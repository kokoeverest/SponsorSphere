using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Globalization;

namespace SponsorSphereWebAPI.Filters
{
    public class DurationLogActionFilter : IActionFilter
    {
        public Stopwatch? StopWatch {  get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string format = "yyyyMMdd";
            Console.WriteLine($">>>>> Action: {context.ActionDescriptor.DisplayName} ({StopWatch?.ElapsedMilliseconds}ms) at {DateTime.UtcNow.ToString(format, CultureInfo.InvariantCulture)}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            StopWatch = Stopwatch.StartNew();
        }
    }
}
