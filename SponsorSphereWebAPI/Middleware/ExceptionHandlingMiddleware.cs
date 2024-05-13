using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphereWebAPI.Filters;
using System.Net;

namespace SponsorSphereWebAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(ILoggerFactory loggerFactory, RequestDelegate next)
        {
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error");

                context.Response.StatusCode = ex switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    DbUpdateException => (int)HttpStatusCode.BadRequest,
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

            await CreateExceptionResponseAsync(context, ex);
            }
        }

        private static Task CreateExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorResponse()
            {
                StatusCode = context.Response.StatusCode,
                StatusPhrase = ex.Message,
                Timestamp = DateTime.UtcNow
            }.ToString());
        }
    }
}
