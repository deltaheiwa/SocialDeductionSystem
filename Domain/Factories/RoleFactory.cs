using System.Reflection;
using Microsoft.Extensions.Logging;
using SocialDeductionSystem.Domain.Attributes;
using SocialDeductionSystem.Domain.Interfaces;
using SocialDeductionSystem.Domain.Interfaces.External.Factories;
using SocialDeductionSystem.Domain.ValueObjects.Ids;
using SocialDeductionSystem.Domain.ValueObjects.Ids.Comparers;

namespace SocialDeductionSystem.Domain.Factories;

public class RoleFactory : IRoleFactory
{
    private readonly ILogger<RoleFactory> _logger;
    
    public IRole CreateRole(string roleId, ISdgVariationRuleset variationInfo)
    {
        throw new NotImplementedException();
    }
    
    private (IReadOnlyDictionary<RoleId, Type> BaseRoles, IReadOnlyDictionary<string, IReadOnlyDictionary<string, Type>> Adapters) ScanForRolesAndAdapters()
    {
        var baseRoles = new Dictionary<RoleId, Type>(RoleIdOrdinalIgnoreCaseComparer.Instance);
        var adapters = new Dictionary<string, Dictionary<string, Type>>(StringComparer.OrdinalIgnoreCase);
        
        var assembliesToScan = AppDomain.CurrentDomain.GetAssemblies()
            .Where(asm => asm.FullName != null &&
                         (asm.FullName.StartsWith("SocialDeductionSystem.Domain.Core") ||
                          asm.FullName.StartsWith("SocialDeductionSystem.Domain.Variations.")))
            .Distinct()
            .ToList();

        _logger.LogDebug("Scanning {Count} assemblies for roles and adapters.", assembliesToScan.Count);

        foreach (var assembly in assembliesToScan)
        {
            try
            {
                var roleTypes = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && typeof(IRole).IsAssignableFrom(t));

                foreach (var type in roleTypes)
                {
                    var roleIdAttr = type.GetCustomAttribute<RoleIdAttribute>();
                    if (roleIdAttr != null)
                    {
                        if (!baseRoles.TryAdd(roleIdAttr.RoleId, type))
                        {
                            _logger.LogWarning("Duplicate RoleId found: '{RoleId}' mapping to {TypeName} conflicts with existing {ExistingTypeName}",
                                roleIdAttr.RoleId, type.FullName, baseRoles[roleIdAttr.RoleId].FullName);
                        } else {
                             _logger.LogTrace("Registered Base Role: ID='{RoleId}', Type='{TypeName}'", roleIdAttr.RoleId, type.FullName);
                        }
                    }
                    
                    var adapterAttr = type.GetCustomAttribute<RoleAdapterAttribute>();
                    if (adapterAttr != null)
                    {
                         if (!adapters.ContainsKey(adapterAttr.TargetVariationId))
                         {
                             adapters[adapterAttr.TargetVariationId] = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
                         }

                         if (!adapters[adapterAttr.TargetVariationId].TryAdd(adapterAttr.BaseRoleId, type))
                         {
                             _logger.LogWarning("Duplicate Adapter found for Variation='{VariationId}', BaseRole='{BaseRoleId}' mapping to {TypeName} conflicts with existing {ExistingTypeName}",
                                 adapterAttr.TargetVariationId, adapterAttr.BaseRoleId, type.FullName, adapters[adapterAttr.TargetVariationId][adapterAttr.BaseRoleId].FullName);
                         } else {
                              _logger.LogTrace("Registered Adapter: Variation='{VariationId}', BaseRole='{BaseRoleId}', Type='{TypeName}'",
                                     adapterAttr.TargetVariationId, adapterAttr.BaseRoleId, type.FullName);
                         }
                    }
                    
                    if (roleIdAttr == null && adapterAttr == null)
                    {
                         _logger.LogTrace("Type '{TypeName}' implements IRole but has no RoleId or RoleAdapter attribute.", type.FullName);
                    }
                }
            }
            catch (Exception ex) {

                 _logger.LogError(ex, "Error scanning assembly {AssemblyName} for roles/adapters.", assembly.FullName);
            }
        }
        
        var readOnlyAdapters = adapters.ToDictionary(
                kvp => kvp.Key,
                kvp => (IReadOnlyDictionary<string, Type>)kvp.Value.AsReadOnly(),
                StringComparer.OrdinalIgnoreCase
            );
        return (baseRoles.AsReadOnly(), readOnlyAdapters.AsReadOnly());
    }
}