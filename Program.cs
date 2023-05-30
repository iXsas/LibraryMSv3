// Import necessary namespaces
using LibraryMSv3.Data;
using LibraryMSv3.Mapper;
using LibraryMSv3.Repositories;
using LibraryMSv3.Repositories.Interfaces;
using LibraryMSv3.Services.Interfaces;
using LibraryMSv3.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

// Create builder for the application
var builder = WebApplication.CreateBuilder(args);

// Read configuration file
var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json");
IConfiguration config = configBuilder.Build();

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperLMS));

// Set up Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
SetUpSwagger(builder.Services);

// Set up JWT authentication

SetUpAuthentication(builder.Services);

// Add necessary services and repositories
builder.Services.AddDbContext<ApplicationDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();


// Register DbContext
//builder.Services.AddScoped<IDbContext, UserService>();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();


builder.Services.AddHttpContextAccessor();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

//Function to set up Swagger with JWT authentication
void SetUpSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new string[]{}
            }
        });
    });
}

// Function to set up JWT authentication
void SetUpAuthentication(IServiceCollection services)
{
    _ = services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["JWT:ValidIssuer"],
            ValidAudience = config["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: config["JWT:Token"]))
        };
    });
    services.AddAuthorization(options =>
    {
        options.AddPolicy("Special", policy => policy.RequireClaim("Special"));
        options.AddPolicy("Role", policy => policy.RequireClaim("Role"));
    });
}
