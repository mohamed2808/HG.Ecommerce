using HG.Ecommerce.APIs.Controllers.Base;
using HG.Ecommerce.Application.DependancyInjection;
using HG.Ecommerce.Infrastruction.Presistance.Dependency_Injection;
using HG.Ecommerce.Presentation.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(BaseAPIController).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.GetConnectionString(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.Run();
