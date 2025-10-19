using Core.Models;

namespace Core.Events;

public class AttackEvent(Player attacker, Player target) : GameEventBase
{
    public Player Attacker { get; } = attacker;
    public Player Target { get; } = target;
    public bool IsStopped { get; set; } = false;
    public string FailureMessage { get; set; } = "Your target was immune!";
}