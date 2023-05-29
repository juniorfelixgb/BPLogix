
using BPLogix.BooksCvsGenerator.Infrastructure;
using FastEndpoints.Swagger;
using FastEndpoints;
using System.Text.Json;
using BPLogix.BooksCvsGenerator.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    c.Endpoints.RoutePrefix = "api";
});

app.UseAuthorization();
app.UseRouting();
app.UseOpenApi();
app.UseSwaggerGen();
app.UseSwaggerUi3(c => c.ConfigureDefaults());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();