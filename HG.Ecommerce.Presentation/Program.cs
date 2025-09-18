using HG.Ecommerce.APIs.Controllers.Base;
using HG.Ecommerce.Application.DependancyInjection;
using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Core.Entites;
using HG.Ecommerce.Core.Entites.JWTSetting;
using HG.Ecommerce.Infrastruction.Presistance.Data.DbContextFile;
using HG.Ecommerce.Infrastruction.Presistance.Dependency_Injection;
using HG.Ecommerce.Infrastruction.Services;
using HG.Ecommerce.Presentation.Middlewares;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(BaseAPIController).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.GetConnectionString(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<EcommerceDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.Run();
