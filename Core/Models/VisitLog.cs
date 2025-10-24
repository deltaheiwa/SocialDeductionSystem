using Core.Enums;

namespace Core.Models;

public record VisitLog(Player Visitor, Player Target, int NightNumber, VisitType VisitType);