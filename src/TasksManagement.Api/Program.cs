using Serilog;
using TasksManagement.Api;
using TasksManagement.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApi();

// Load serilog configuration
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration, "Serilog");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use serilog
app.UseSerilogRequestLogging();
//// Use serilog with middleware for logging requests and resposes (issue with responses in swagger)
//app.UseMiddleware<SerilogRequestResponseLogging>();
//app.UseSerilogRequestLogging(options =>
//{
//    // template for output
//    options.MessageTemplate =
//        "HTTP RequestMethod: {RequestMethod}. RequestPath: {RequestUrl}{RequestPath}. RequestBody: {RequestBody}. ResponseCode: {StatusCode}. RepsonseBody: {ResponseBody} in {Elapsed:0.0000}";
//    options.EnrichDiagnosticContext = SerilogRequestResponseLogging.EnrichDiagnosticContext;
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
