namespace SocialDeductionSystem.Domain.ValueObjects.Ids;

public readonly record struct GameId(Guid value) { public GameId() : this(Guid.NewGuid()) { } }

public readonly record struct PlayerId(Guid value) { public PlayerId() : this(Guid.NewGuid()) { } }

public readonly record struct RoleId(string roleId)
{
    public string Value { get; init; } = ValidateRoleId(roleId);
    private static string ValidateRoleId(string roleId) { 
        if (string.IsNullOrWhiteSpace(roleId))
            throw new ArgumentException("RoleId cannot be null or whitespace.", nameof(roleId));
        // Todo: Add length checks, allowed characters, toLower normalization?
        roleId = roleId.Trim();
        return roleId;
    }
    
    public static implicit operator string(RoleId roleId) => roleId.Value;
}

public readonly record struct TeamId(string teamId)
{
    public string Value { get; init; } = ValidateTeamId(teamId);

    private static string ValidateTeamId(string teamId)
    {
        if (string.IsNullOrWhiteSpace(teamId))
            throw new ArgumentException("TeamId cannot be null or whitespace.", nameof(teamId));

        teamId = teamId.Trim();
        return teamId;
    }

    public static implicit operator string(TeamId teamId) => teamId.Value;
}

