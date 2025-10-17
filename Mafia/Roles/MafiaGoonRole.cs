namespace Mafia.Roles;

public class MafiaGoonRole : Role
{
    public override string Name => "Mafia";
    public override string Description => "You are a goon of the Mafia, just another evildoer keen to see the Mafia succeed.";
    public override Faction Faction => Faction.Mafia;
}