using Microsoft.AspNetCore.Mvc.Filters;
using Blog.CrossCutting.Log;

namespace Blog.Endpoints.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly Blog.CrossCutting.Log.ILogger _logger;

        public ExceptionFilter(Blog.CrossCutting.Log.ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                _logger.writeLog (context.Exception);
            }
        }
    }
}
