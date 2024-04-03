using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OblakotekaServer.Domain.Exceptions;

namespace OblakotekaServer.Middlewares
{
    public class DomainExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _request;

        public DomainExceptionHandlerMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request.Invoke(context);
            }
            catch (ProductNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
            catch (NotUniqueValueException ex)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
        }
    }
}