using HR.LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HR.LeaveManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }

            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new ErrorDetails { ErrorType = "Failure", ErrorMessage = exception.Message });

            switch (exception)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Message);
                    break;

                default:
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }

        public class ErrorDetails
        {
            public string ErrorType { get; set; }

            public string ErrorMessage { get; set; }
        }
    }
}