using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Globalization;

namespace SponsorSphereWebAPI.Filters
{
    /// <summary>
    /// Represents an action filter that logs the duration of the action execution.
    /// </summary>
    public class DurationLogActionFilter : IActionFilter
    {
        /// <summary>
        /// Gets or sets the stopwatch used to measure the duration of the action execution.
        /// </summary>
        public Stopwatch? StopWatch { get; set; }

        /// <summary>
        /// Called before the action method is invoked.
        /// Starts the stopwatch to measure the duration of the action execution.
        /// </summary>
        /// <param name="context">The context of the action being executed.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            StopWatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// Called after the action method is invoked.
        /// Logs the duration of the action execution.
        /// </summary>
        /// <param name="context">The context of the action that was executed.</param>
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
