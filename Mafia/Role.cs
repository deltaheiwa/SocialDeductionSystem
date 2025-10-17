namespace Mafia;

public abstract class Role
{
    /// <summary>
    /// The name of the role.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// A description of the role's goal.
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// The team the role belongs to.
    /// </summary>
    public abstract Faction Faction { get; }
}