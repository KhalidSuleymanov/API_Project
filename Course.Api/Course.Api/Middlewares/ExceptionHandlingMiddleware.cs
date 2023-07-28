using Course.Service.Exceptions;

namespace Course.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                List<RestExceptionErrorItem> errors = new List<RestExceptionErrorItem>();
                string message = e.Message;
                switch (e)
                {
                    case RestException re:
                        response.StatusCode = (int)re.Code;
                        message = re.Message;
                        errors = re.Errors;
                        break;
                    default:
                        response.StatusCode = 500;
                        break;
                }
                await response.WriteAsJsonAsync(new { message, errors });
            }
        }
    }
}

