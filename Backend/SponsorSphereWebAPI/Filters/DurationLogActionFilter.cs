using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Globalization;

namespace SponsorSphereWebAPI.Filters
{
    public class DurationLogActionFilter : IActionFilter
    {
        public Stopwatch? StopWatch {  get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            StopWatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            CultureInfo invariant = CultureInfo.InvariantCulture;
            string format = "hh:mm:ss";
            string controller = context.ActionDescriptor.DisplayName!;
            string action = 
                $"[{DateTime.Now.ToString(format, invariant)} INF] Controller: {controller}, total execution time ({StopWatch!.ElapsedMilliseconds}ms)";

            Console.WriteLine(action);
        }
    }
}
