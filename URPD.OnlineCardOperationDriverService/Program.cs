using Microsoft.Extensions.Hosting.WindowsServices;
using System.Text.Json;
using System.Reflection;
using Microsoft.OpenApi.Models;
using URPD.OnlineCardOperationDriverService.DataTypes;
using URPD.OnlineCardOperationDriverService.Services;


var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService()
                                     ? AppContext.BaseDirectory : default
};
var builder = WebApplication.CreateBuilder(options);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "URPD.OnlineCardOperationDriverService",
        Description = "Web-сервис дл€ взаимодействи€ с IZProtokolDriver"
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
    options.EnableAnnotations();
});

builder.Host.UseWindowsService();
builder.Services.AddHostedService<DriverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

DriverService ds = new DriverService();

app.MapGet("/status", () => Results.Json(new ResultDescription { ResultCode = 0, ResultCodeDescription = "—ервис запущен." }))
    .WithName("Status");

app.MapPost("/init", () =>
{
    ds.Init();
    return Results.Json(new ResultDescription { ResultCode = ds.ResultCode, ResultCodeDescription = ds.ResultCodeDescription ?? ""});
}).WithName("Init");

app.MapPost("/showproperties", async () =>
{
    await ds.ShowProperties();
}).WithName("ShowProperties");

app.MapPost("/getsmartcardnativeid", async () =>
{
    await ds.GetSmartCardNativeId();
    return Results.Json(new ResultDescription { ResultCode = ds.ResultCode, ResultCodeDescription = ds.ResultCodeDescription, SmartCard = new SmartCard { SmartCardNativeId = ds.SmartCardNativeId} });
}).WithName("GetSmartCardNativeId");

app.MapPost("/ticketsale", async (TicketSale ticketSale) =>
{
    await ds.TicketSale(ticketSale);
    return Results.Json(new ResultDescription { ResultCode = ds.ResultCode, ResultCodeDescription = ds.ResultCodeDescription});
}).WithName("TicketSale");

app.Run();
