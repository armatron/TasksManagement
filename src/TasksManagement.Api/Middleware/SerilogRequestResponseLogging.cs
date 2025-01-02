using Serilog;
using System.Text.RegularExpressions;

namespace TasksManagement.Api.Middleware
{
    public class SerilogRequestResponseLogging
    {
        private readonly RequestDelegate _next;

        public SerilogRequestResponseLogging(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Allow multiple reading of request body
            context.Request.EnableBuffering();

            // Create a new rewindable memory stream for the response which downstream middleware will use
            context.Response.Body = new MemoryStream();

            // Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);
        }

        public static async void EnrichDiagnosticContext(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            Stream requestBodyStream = httpContext.Request.Body;
            requestBodyStream.Position = 0;
            string requestBodyAsText = await new StreamReader(requestBodyStream).ReadToEndAsync();
            requestBodyStream.Position = 0;
            diagnosticContext.Set("RequestBody", Regex.Replace(requestBodyAsText, @"\s+", " ").Trim()); // remove \n

            Stream responseBodyStream = httpContext.Response.Body;
            responseBodyStream.Position = 0;
            string responseBodyAsText = await new StreamReader(responseBodyStream).ReadToEndAsync();
            responseBodyStream.Position = 0;
            diagnosticContext.Set("ResponseBody", Regex.Replace(responseBodyAsText, @"\s+", " ").Trim()); // remove \n
        }
    }
}
