using FluentValidation;
using FluentValidation.Results;
using SaturnApi.Business.ErrorNotifications;
using SaturnApi.Business.Interfaces;
using SaturnApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaturnApi.Api.Services
{
     public abstract class BaseService
    {
        private readonly IErrorNotifier _errorNotifier;

        public BaseService(IErrorNotifier errorNotifier)
        {
            _errorNotifier = errorNotifier;
        }

        protected void NotifyError(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NotifyError(error.ErrorMessage);
            }
        }

        protected void NotifyError(string message)
        {
            _errorNotifier.Handle(new ErrorNotification(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
            {
                return true;
            }

            NotifyError(validator);

            return false;
        }
    }
}
