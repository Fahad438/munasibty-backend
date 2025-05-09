﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using System.Text;
using TestApi.Data;
using Zafaty.Server.Helpers;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;
using Zafaty.Server.Repositories;
using Zafaty.Server.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// قراءة القيم من Environment Variables
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                    ?? builder.Configuration.GetConnectionString("DefaultConnection");

var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key") ?? "fallback-key";
var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer") ?? "zafaty";
var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience") ?? "zafatyUsers";

// تسجيل الـ Repositories والخدمات
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IGuestsRepository, GuestRepository>();
builder.Services.AddScoped<IGuestsService, GuestService>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IBudgetService, BudgetService>();

// تسجيل خدمة المصادقة
builder.Services.AddScoped<IAuthService, AuthService>();

// إعداد DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// إعداد JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// إضافة التصاريح (Authorization)
builder.Services.AddAuthorization();

// إعداد CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// إعداد Rate Limiting
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddPolicy("UserRateLimit", httpContext =>
    {
        var userId = httpContext.User.FindFirst("id")?.Value ?? "anonymous";

        return RateLimitPartition.GetFixedWindowLimiter(userId, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 50,
            Window = TimeSpan.FromSeconds(60),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 2
        });
    });
});

// إضافة الخدمات للـ Controllers
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// استخدام CORS
app.UseCors("AllowAll");

// تمكين Swagger في بيئة التطوير
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();
