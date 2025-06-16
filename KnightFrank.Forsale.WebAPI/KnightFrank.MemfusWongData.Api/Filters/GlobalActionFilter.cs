using Microsoft.AspNetCore.Mvc.Filters;

namespace KnightFrank.MemfusWongData.Api.Filters
{
    /// <summary>
    /// This represents the filter attribute entity for global actions.
    /// </summary>
    public class GlobalActionFilter : ActionFilterAttribute
    {
        //private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalActionFilter"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILoggerFactory"/> instance.</param>
        //public GlobalActionFilter(ILoggerFactory logger)
        //{
        //    if (logger == null)
        //    {
        //        throw new ArgumentNullException(nameof(logger));
        //    }

        //    _logger = logger.CreateLogger("Global Action Filter");
        //}
        public GlobalActionFilter()
        {
        }

        /// <summary>
        /// Called while an action is being executed.
        /// </summary>
        /// <param name="context"><see cref="ActionExecutingContext"/> instance.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //   _logger.LogInformation("Global Action Filter - OnActionExecuting");
        }
    }
}
