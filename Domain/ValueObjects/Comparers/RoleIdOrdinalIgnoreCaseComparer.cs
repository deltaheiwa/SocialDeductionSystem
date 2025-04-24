namespace SocialDeductionSystem.Domain.ValueObjects.Ids.Comparers;

public class RoleIdOrdinalIgnoreCaseComparer : IEqualityComparer<RoleId>
{
    public static RoleIdOrdinalIgnoreCaseComparer Instance { get; } = new RoleIdOrdinalIgnoreCaseComparer();
    
    public bool Equals(RoleId x, RoleId y)
    {
        return string.Equals(x.Value, y.Value, StringComparison.OrdinalIgnoreCase) && string.Equals(x.roleId, y.roleId, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(RoleId obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Value, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(obj.roleId, StringComparer.OrdinalIgnoreCase);
        return hashCode.ToHashCode();
    }
}
