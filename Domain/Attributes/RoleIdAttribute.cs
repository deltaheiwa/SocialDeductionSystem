using SocialDeductionSystem.Domain.ValueObjects;

namespace SocialDeductionSystem.Domain.Attributes;


[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class RoleIdAttribute : Attribute
{
    public RoleId RoleId { get; }

    /// <summary>
    /// Associates a class implementing IRole with its unique string identifier.
    /// </summary>
    /// <param name="roleId">The unique string identifier.</param>
    public RoleIdAttribute(RoleId roleId)
    {
        RoleId = roleId;
    }
}