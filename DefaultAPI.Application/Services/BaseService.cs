using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Base;
using DefaultAPI.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace DefaultAPI.Application.Services
{
    public abstract class BaseService
    {
        private readonly INotificationMessageService _notificationMessageService;

        protected BaseService(INotificationMessageService notificationMessageService)
        {
            _notificationMessageService = notificationMessageService;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notificationMessageService.Handle(new NotificationMessage(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : BaseEntity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
