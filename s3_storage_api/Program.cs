using Microsoft.AspNetCore.Http.Features;
using s3_storage_api.Consants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = AppConstants.MaxFileSize;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = AppConstants.MaxFileSize;
});

var app = builder.Build();

var storagePath = Path.Combine(app.Environment.ContentRootPath, AppConstants.StorageDirectory);

if (!Directory.Exists(storagePath))
{
    Directory.CreateDirectory(storagePath);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
