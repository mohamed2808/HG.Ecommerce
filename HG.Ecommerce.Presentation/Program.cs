using HG.Ecommerce.APIs.Controllers.Base;
using HG.Ecommerce.Application.DependancyInjection;
using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Core.Entites;
using HG.Ecommerce.Core.Entites.JWTSetting;
using HG.Ecommerce.Infrastruction.Presistance.Data.DbContextFile;
using HG.Ecommerce.Infrastruction.Presistance.Dependency_Injection;
using HG.Ecommerce.Infrastruction.Services;
using HG.Ecommerce.Presentation.Extenstions;
using HG.Ecommerce.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(BaseAPIController).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.GetConnectionString(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<EcommerceDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        b => b.WithOrigins("http://localhost:4200", "https://hgecommerce.runasp.net", "https://hg-ecommerce-angular-5s1d.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod());
});



var app = builder.Build();
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self'; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data:; " +
        "connect-src 'self' http://localhost:4200 https://hgecommerce.runasp.net; " +
        "frame-ancestors 'self';");

    await next();
});
app.UseCors("AllowFrontend");
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.Run();
