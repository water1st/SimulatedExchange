using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SimulatedExchange.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory loggerFactory;

        public ExceptionFilter(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var logger = loggerFactory.CreateLogger(exception.TargetSite.ReflectedType);
            logger.LogError(exception, exception.Message);

            var result = new ContentResult();
            result.Content = exception.Message;
            result.StatusCode = 500;
            result.ContentType = "text/plain";

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
