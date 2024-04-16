using Courier.DataAccess.Data;
using Courier.Domain.Models;
using Courier.Domain.Repository;
using Courier.Domain.Repository.IRepository;
using Courier.Domain.Services.Email;
using Courier.Domain.Services.OfferService;
using Courier.Domain.Services.OrderService;
using Courier.Domain.Validation;
using Courier.React.Authorization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
#if (RELEASE)
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDBConnection"));
#else
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
#endif
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
        RoleClaimType = "role",
    };
    options.RequireHttpsMetadata = false;
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ChangeStatus", policy => policy.Requirements.Add(new
        HasRoleRequirement(new[] { "Admin", "OfficeWorker", "Courier" }, domain)));
    options.AddPolicy("GetAllInquiries", policy => policy.Requirements.Add(new
        HasRoleRequirement(new[] { "Admin"}, domain)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasRoleHandler>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
