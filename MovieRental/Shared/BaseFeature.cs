using FluentValidation;
using FluentValidation.Results;
using MovieRental.Notification;

namespace MovieRental.Shared;

public abstract class BaseFeature
{
    private readonly INotifier _notifier;

    public BaseFeature(INotifier notifier)
    {
        _notifier = notifier;
    }
    
    protected void Notify(ValidationResult validation)
    {
        foreach (var validationResult in validation.Errors)
        {
            Notify(validationResult.ErrorMessage);
        }
    }
    
    protected void Notify(string message)
    {
        _notifier.HandleNotification(new Notification.Notification(message));
    }
    protected bool ExecuteValidation<TV, TE>(TV validation, TE entity)
        where TV : AbstractValidator<TE>
        where TE : Entity
    {
        var validationResult = validation.Validate(entity);
        if (validationResult.IsValid) return true;
        
        Notify(validationResult);
        
        return false;
    }

}