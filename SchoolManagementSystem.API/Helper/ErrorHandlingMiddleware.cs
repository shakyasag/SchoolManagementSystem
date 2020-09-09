using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolManagementSystem.Application.Common.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolManagementSystem.API.Helper
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            object errors = null;
            switch (ex)
            {
                case RestException re:
                    logger.LogError(ex, "REST ERROR");
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case ValidateException ValidateException:
                    code = HttpStatusCode.BadRequest;
                    errors = JsonSerializer.Serialize(ValidateException.Failures);
                    break;

                case Exception e:
                    logger.LogError(ex, "SERVER ERROR");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)code;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (errors != null)
            {
                var result = JsonSerializer.Serialize(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }


    }



    public static class ErrorhandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
