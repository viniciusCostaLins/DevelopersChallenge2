using FluentValidation;
using FluentValidation.Results;
using Nibo.Domain.Entities.Abstract;
using Nibo.Domain.Interfaces.Services;

namespace Nibo.Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void DoNotify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                DoNotify(error.ErrorMessage);
            }
        }

        protected void DoNotify(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if(validator.IsValid) return true;

            DoNotify(validator);

            return false;
        }
    }
}