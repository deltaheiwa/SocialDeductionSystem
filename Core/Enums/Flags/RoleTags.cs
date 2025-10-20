namespace Core.Enums.Flags;

/// <summary>
/// Logical tags that might affect the setup generation or gameplay mechanics of roles.
/// </summary>
[Flags]
public enum RoleTags
{
    None = 0,
    Unique = 1 << 0,
}