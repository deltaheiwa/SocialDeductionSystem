namespace SocialDeductionSystem.Domain.ValueObjects.Comparers;

public class RoleIdOrdinalIgnoreCaseComparer : IEqualityComparer<RoleId>
{
    public static RoleIdOrdinalIgnoreCaseComparer Instance { get; } = new();
    
    public bool Equals(RoleId x, RoleId y)
    {
        return string.Equals(x.Value, y.Value, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(RoleId obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Value, StringComparer.OrdinalIgnoreCase);
        return hashCode.ToHashCode();
    }
}
