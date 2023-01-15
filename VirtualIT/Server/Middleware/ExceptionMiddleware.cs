using System.Net;
using VirtualIT.Domain.Exceptions;

namespace VirtualIT.Server.Middleware {
    public class ExceptionMiddleware {
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next) {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            try {
                await next(httpContext);
            } catch (Exception ex) {
                logger.LogError(ex, "Something went wrong");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            ErrorDetails error = exception switch {
                EntityNotFoundException ex => new ErrorDetails(ex.Message, HttpStatusCode.NotFound),
                EntityAlreadyExistsException ex => new ErrorDetails(ex.Message, HttpStatusCode.Conflict),
                ServerDoesNotHaveEnoughResourcesException ex => new ErrorDetails(ex.Message, HttpStatusCode.Conflict),
                ArgumentException ex => new ErrorDetails(ex.Message, HttpStatusCode.NotFound),
                ApplicationException ex => new ErrorDetails(ex.Message),
                _ => new ErrorDetails(exception.Message)
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.StatusCode;
            await context.Response.WriteAsync(error.ToString());
        }
    }
}
