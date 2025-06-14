namespace SocialDeductionSystem.Domain.Notifications.Audiences;

public abstract class Audience;

public class PublicAudience : Audience { }
public class PlayerAudience : Audience { public string PlayerId { get; set; } }
public class TeamAudience : Audience { public string TeamId { get; set; } }