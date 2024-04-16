using Courier.DataAccess.Data;
using Courier.Domain.Repository;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.Email;
using Courier.Domain.Services.OfferService;
using Courier.Domain.Services.OrderService;
using Courier.Domain.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddValidatorsFromAssemblyContaining<InquiryValidator>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// JWT
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "role"
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PostInquiry", policy =>
                      policy.RequireClaim("permissions", "post:inquiry"));
    options.AddPolicy("PostOrder", policy =>
                      policy.RequireClaim("permissions", "post:order"));
    options.AddPolicy("GetOrder", policy =>
                      policy.RequireClaim("permissions", "get:order"));
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Documentation",
        Version = "v1.0",
        Description = ""
    });
    options.ResolveConflictingActions(x => x.First());
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        BearerFormat = "JWT",
        Flows = new OpenApiOAuthFlows
        {
            ClientCredentials = new OpenApiOAuthFlow()
            {
                TokenUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/oauth/token"),
            }          
        }
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
          {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
              },
              new[] { "" }
          }
      });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(settings =>
{
    settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
    settings.UseRequestInterceptor("(req) => { if (req.url.endsWith('oauth/token') && req.body) req.body += '&audience=" + builder.Configuration["Auth0:Audience"] + "'; return req; }");
    settings.OAuthUsePkce();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
