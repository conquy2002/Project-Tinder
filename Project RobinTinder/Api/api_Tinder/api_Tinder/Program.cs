using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using api_Tinder.Models;
using api_Tinder.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<api_TinderContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("api_TinderContext")));




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                      builder =>
                      {
                          builder.WithOrigins("http://127.0.0.1:5500/website/index.html",
                                              "http://127.0.0.1:5500/")
                                .WithMethods("PUT", "DELETE", "GET");
                      })
     ;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireCors("MyPolicy"); ;

app.Run();
