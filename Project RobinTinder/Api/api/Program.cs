using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using api_test.Data;
using api_test.Models;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<api_testContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("api_testContext")));

// Add services to the container.
//builder.Services.Configure<FormOptions>(options => {
//    options.ValueCountLimit = int.MaxValue;
//    options.MultipartBodyLengthLimit = int.MaxValue;   
//    options.MemoryBufferThreshold = int.MaxValue;
//});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Resources")),
//    RequestPath = new PathString("/Resources")
//});
app.UseAuthorization();

app.MapControllers();

app.Run();
