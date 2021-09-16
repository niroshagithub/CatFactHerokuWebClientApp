using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CatFactHerokuWebClientApp.Web.ExceptionHandlers
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate request,
            ILogger<ExceptionHandler> logger)
        {
            _request = request;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request.Invoke(context);
            }
            catch (Exception ex)
            {
                var code = DateTime.Now.Ticks.ToString();
                _logger.LogError(ex, $"Unhandled Exception (REF: {code}).");

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var payload = new
                    {Message = $"Unhandled Exception. Please contact system administrator. Regerence No.: {code}."};
                await response.WriteAsync(JsonConvert.SerializeObject(payload));
            }
        }
    }
}
