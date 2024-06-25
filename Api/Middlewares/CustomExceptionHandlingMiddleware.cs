using System.Net;
using System.Text.Json;
using Core.Exceptions;
using FluentValidation;

namespace Api.Middlewares;

public class CustomExceptionHandlingMiddleware
{
    public CustomExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private readonly RequestDelegate _next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionsAsync(context, exception);
        }
    }

    private Task HandleExceptionsAsync(HttpContext context, Exception exception)
    {
        ExceptionOptions options = ExceptionOptions.GenerateExceptionOptions(exception);
        
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)options.StatusCode;

        return context.Response.WriteAsJsonAsync(options.ResultMessage);
    }
    
    
    private record ExceptionOptions
    {
        private ExceptionOptions(HttpStatusCode statusCode, string resultMessage) 
        {
            StatusCode = statusCode;
            ResultMessage = resultMessage;
        }
        
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string ResultMessage { get; set; }
        
        
        public static ExceptionOptions GenerateExceptionOptions(Exception exception)
        {
            ExceptionOptions helper = SwitchException(exception);
            
            if (helper.ResultMessage.Equals(String.Empty))
            {
                helper.ResultMessage = JsonSerializer.Serialize(new
                {
                    error = exception.Message
                });
            }

            return new ExceptionOptions(helper.StatusCode, helper.ResultMessage);
        }

        private static ExceptionOptions SwitchException(Exception exception)
        {
            switch (exception)
            {
                case ValidationException validationException:
                {
                    HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                    string resultMessage = JsonSerializer.Serialize(validationException.Errors);
                    return new ExceptionOptions(statusCode, resultMessage);
                }
                case DuplicateException duplicateException:
                {
                    HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                    string resultMessage = JsonSerializer.Serialize(new
                    {
                        error = duplicateException.Message
                    });
                    return new ExceptionOptions(statusCode, resultMessage);
                }
                case IncorrectPasswordException passwordException:
                {
                    HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                    string resultMessage = JsonSerializer.Serialize(new
                    {
                        error = passwordException.Message
                    });
                    return new ExceptionOptions(statusCode, resultMessage);
                }
            }
            
            return new ExceptionOptions(HttpStatusCode.InternalServerError, String.Empty);
        }
    }
}