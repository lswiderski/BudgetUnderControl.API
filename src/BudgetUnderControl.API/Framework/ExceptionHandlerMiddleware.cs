using BudgetUnderControl.Shared.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BudgetUnderControl.API.Framework
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            this.logger.LogError(exception, string.Format("{0} Request: {2}{3} | StackTrace: {1}", exception.Message, exception.StackTrace, context.Request.Path.ToString(), context.Request.QueryString.ToString()));
            var errorCode = "error";

            context.Response.ContentType = "application/json";

            if (exception is ValidationCommandException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var validationErrorResponse = new { code = errorCode, errorType = "Validation", message = "Validation Error", errors = ((ValidationCommandException)exception).Errors };
                await context.Response.WriteAsJsonAsync(validationErrorResponse);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { code = errorCode, message = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
