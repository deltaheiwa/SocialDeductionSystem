using SocialDeductionSystem.Domain.Notifications.Audiences;

namespace SocialDeductionSystem.Domain.Interfaces.Notification;

public interface INotification
{
    Audience TargetAudience { get; set; }
    string Message { get; set; }
}