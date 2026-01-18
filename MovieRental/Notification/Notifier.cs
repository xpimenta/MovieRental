namespace MovieRental.Notification;

public class Notifier : INotifier
{
    private List<Notification> _notifications;

    public Notifier()
    {
        _notifications = new List<Notification>();
    }
    public void HandleNotification(Notification notification)
    {
        _notifications.Add(notification);
    }
    public bool hasNotification()
    {
        return _notifications.Any();
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }
}