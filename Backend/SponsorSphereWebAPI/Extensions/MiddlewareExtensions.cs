using SponsorSphereWebAPI.Middleware;

namespace SponsorSphereWebAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app) 
            => app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
