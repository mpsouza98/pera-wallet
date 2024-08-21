using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PeraInvest.Domain.CarteiraAggregate.Exceptions;

namespace PeraInvest.API.Controllers.ExceptionFilters {
    public class ExceptionFilter : IExceptionFilter {
        public void OnException(ExceptionContext context) {
            var statusCode = context.Exception switch {
                EntityAlreadyExistsException => StatusCodes.Status409Conflict,

                EntityNotFoundException => StatusCodes.Status404NotFound,

                BadRequestException => StatusCodes.Status400BadRequest,

                _ => StatusCodes.Status500InternalServerError
            };

            var message = statusCode switch {
                StatusCodes.Status500InternalServerError => "Internal Server Error",

                _ => context.Exception.Message
            };

            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new ExceptionResponse(message));
        }
    }

    public record ExceptionResponse(string Message);
}
