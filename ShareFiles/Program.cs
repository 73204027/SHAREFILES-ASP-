using ShareFiles.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IISServerOptions>((options) => 
{
    options.MaxRequestBodySize = int.MaxValue;
});
builder.WebHost.ConfigureKestrel(serverOptions => 
{
    serverOptions.Limits.MaxRequestBodySize = int.MaxValue;
});
builder.Services.AddScoped<UploadHandlerService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapControllers();



app.Run();
