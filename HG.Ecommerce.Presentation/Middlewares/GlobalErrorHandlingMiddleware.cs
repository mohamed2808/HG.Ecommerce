using HG.Ecommerce.Application.Abstraction.Exceptions;
using System.Net;
using System.Text.Json;

namespace HG.Ecommerce.Presentation.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case ForbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                message = exception.Message,
                statusCode = (int)statusCode
            });

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }

    }
}
