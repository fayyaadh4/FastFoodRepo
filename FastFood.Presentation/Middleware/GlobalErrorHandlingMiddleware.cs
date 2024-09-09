using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace FastFood.Middleware
{
    // done for all requests
    public class GlobalErrorHandlingMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, 
            ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //request coming in
                _logger.LogInformation("A request coming in");
                await _next(context);
                // response
                _logger.LogInformation("A request has completed");

            }
            catch (Exception e)
            {
                // log error so dev can know stack trace
                _logger.LogError(e, e.Message);
                var details = new ProblemDetails()
                {
                    Detail = "Internal Server Error",
                    Instance = "Error",
                    Status = 500,
                    Title = "Server Error",
                    Type = "Error"
                };

                var response = JsonSerializer.Serialize(details);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response)
;            }
        }
    }
}
