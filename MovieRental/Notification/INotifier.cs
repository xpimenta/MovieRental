namespace MovieRental.Notification;

public interface INotifier
{
    bool hasNotification();
    List<Notification> GetNotifications();
    void HandleNotification(Notification notification);
}

