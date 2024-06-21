using Backend_TimTour.Configuration;
using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.LoginInterfaces;
using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Interfaces.RatingInterfaces;
using Backend_TimTour.Interfaces.RecommandationInterfaces;
using Backend_TimTour.Interfaces.ReservationInterfaces;
using Backend_TimTour.Interfaces.UserInterfaces;
using Backend_TimTour.Parsers;
using Backend_TimTour.Repositories;
using Backend_TimTour.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLog4Net();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding MongoDB configuration
var mongoDbSettings = builder.Configuration.GetSection("MongoDB");

//Rest of dependencies injections
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddSingleton<IMuseumRepository, MuseumRepository>();
builder.Services.AddSingleton<IBarRepository, BarRepository>();
builder.Services.AddSingleton<IUserRegisterService,UserRegisterService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddSingleton<IRatingService, RatingService>();
builder.Services.AddSingleton<IRecommandationService, RecommandationService>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
builder.Services.AddSingleton<IReservationService, ReservationService>();
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IBarParser, BarParser>();
builder.Services.AddSingleton<IMuseumParser, MuseumParser>();
builder.Services.AddSingleton<IRestaurantParser, RestaurantParser>();
builder.Services.AddSingleton<IEventParser, EventParser>();
builder.Services.AddSingleton<IUserPrefferenceParser, UserPrefferenceParser>();
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings["ConnectionString"]));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

// Add authentication service
builder.Services.AddAuthentication(options =>
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TouristAuth", policy => policy.RequireClaim(ClaimTypes.Actor, "Tourist"));
    options.AddPolicy("LocationAuth", policy => policy.RequireClaim(ClaimTypes.Actor, "Location"));
    options.AddPolicy("AdminAuth", policy => policy.RequireClaim(ClaimTypes.Actor, "Administrator"));
});
var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
