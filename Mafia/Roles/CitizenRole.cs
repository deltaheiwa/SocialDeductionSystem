namespace Mafia.Roles;

public class CitizenRole : Role
{
    public override string Name => "Citizen";
    public override string Description => "You are just a normal person minding your own business.";
    public override Faction Faction => Faction.Town;
}