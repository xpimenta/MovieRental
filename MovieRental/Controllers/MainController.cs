using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Notification;

namespace MovieRental.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    public MainController(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected bool ValidOperation()
    {
        return !_notifier.hasNotification();
    }

    protected void NotifyError(string message)
    {
        _notifier.HandleNotification(new Notification.Notification(message));
    }

    protected IActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
    {
        if (ValidOperation())
        {
            return new ObjectResult(result)
            {
                StatusCode = (int)statusCode
            };
        }

        return BadRequest(new
        {
            errors = _notifier.GetNotifications().Select(n => n.Message)
        });
    }
}