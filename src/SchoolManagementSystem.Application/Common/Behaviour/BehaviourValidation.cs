﻿
using FluentValidation;
using MediatR;
using SchoolManagementSystem.Application.Common.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common.Behaviour
{
    public class BehaviourValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public BehaviourValidation(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidateException(failures);
            }
            return await next();
        }
    }
}
