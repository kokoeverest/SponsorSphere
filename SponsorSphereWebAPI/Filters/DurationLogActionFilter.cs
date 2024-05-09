using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace SponsorSphereWebAPI.Filters
{
    public class DurationLogActionFilter : IActionFilter
    {
        public Stopwatch? StopWatch {  get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($">>>>> Action: {context.ActionDescriptor.DisplayName} ({StopWatch?.ElapsedMilliseconds}ms) at {DateTime.UtcNow:f}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            StopWatch = Stopwatch.StartNew();
        }
    }
}
