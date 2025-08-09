var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<fileUploadHandler_service>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapUploadEndpoints();



app.Run();
