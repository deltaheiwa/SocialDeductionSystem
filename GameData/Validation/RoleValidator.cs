using Core.Enums;

namespace GameData.Validation;

public static class RoleValidator
{
    private static readonly Dictionary<Faction, HashSet<Subalignment>> AllowedSubalignments = new()
    {
        [Faction.Town] = new HashSet<Subalignment>
        {
            Subalignment.Standard,
            Subalignment.Investigative,
            Subalignment.Power,
            Subalignment.Protective,
            Subalignment.Support,
            Subalignment.Killing,
            Subalignment.Citizen
        },
        [Faction.Mafia] = new HashSet<Subalignment>
        {
            Subalignment.Standard,
            Subalignment.Deception,
            Subalignment.Tactical,
            Subalignment.Support,
            Subalignment.Killing
        },
        [Faction.Neutral] = new HashSet<Subalignment>
        {
            Subalignment.Standard,
            Subalignment.Apocalypse,
            Subalignment.Evil,
            Subalignment.Killing
        },
    };

    /// <summary>
    /// Checks if a role's sub-alignment is valid for its faction.
    /// Throws an exception if the combination is invalid.
    /// </summary>
    public static void Validate(Faction faction, Subalignment subalignment) // <-- Updated signature
    {
        if (!AllowedSubalignments.ContainsKey(faction))
            return;

        var allowed = AllowedSubalignments[faction];

        if (!allowed.Contains(subalignment))
        {
            throw new ArgumentException($"Role validation failed: The sub-alignment '{subalignment}' is not allowed for the faction '{faction}'.");
        }
    }
}