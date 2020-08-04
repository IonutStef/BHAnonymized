using Anonymized.Assessment.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Anonymized.Assessment.Api.Middlewares
{
    /// <summary>
    /// Filters to handle exceptions.
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        /// <summary>
        /// Initialize the <see cref="ApiExceptionFilter"/> to handle <see cref="Exception"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Exception handler.
        /// </summary>
        /// <param name="context">The <see cref="ExceptionContext"/> containing details about the exception.</param>
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundException e:
                    {
                        context.ExceptionHandled = true;
                        context.ModelState.AddModelError(e.KeyName, e.Message);
                        context.Result = new NotFoundObjectResult(new SerializableError(context.ModelState));

                        _logger.LogError($"Caught {typeof(NotFoundObjectResult)} with message: {e.Message} \n {context.ModelState}.");
                        break;
                    }
                case BadRequestException e:
                    {
                        context.ExceptionHandled = true;
                        context.ModelState.AddModelError(e.KeyName, e.Message);
                        context.Result = new BadRequestObjectResult(context.ModelState);

                        _logger.LogError($"Caught {typeof(BadRequestException)} with message: {e.Message} \n {context.ModelState}.");
                        break;
                    }
                case Exception e:
                    {
                        context.ExceptionHandled = true;
                        context.ModelState.AddModelError(string.Empty, e.Message);
                        context.Result = new UnprocessableEntityObjectResult(new SerializableError(context.ModelState));

                        _logger.LogError($"Caught {typeof(Exception)} with message: {e.Message} \n {context.ModelState}.");
                        break;
                    }
            }
        }
    }
}
