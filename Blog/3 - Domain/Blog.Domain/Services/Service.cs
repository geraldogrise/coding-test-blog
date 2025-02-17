using Blog.Domain.Notifications;
using FluentValidation.Results;
using MediatR;


namespace Blog.Domain.Services
{
    public abstract class Service
    {
        private readonly IMediator _mediator;

        protected Service(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected void RaiseError(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                _mediator.Publish(new DomainNotification(NotificationType.Error, error.PropertyName, error.ErrorMessage));
        }

        protected void RaiseError(string message)
        {
            _mediator.Publish(new DomainNotification(NotificationType.Error, GetType().Name, message));
        }
    }
}
