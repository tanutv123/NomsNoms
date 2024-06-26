using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Net.payOS;
using NomsNoms.Data;
using NomsNoms.Data.Seed;
using NomsNoms.Entities;
using NomsNoms.Extensions;
using NomsNoms.Helpers;
using NomsNoms.Interfaces;
using NomsNoms.Middleware;
using NomsNoms.Services;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
    options.AddPolicy("RequireStaffRole", policy => policy.RequireRole("Staff"));
    options.AddPolicy("RequireCookRole", policy => policy.RequireRole("Cook"));
    options.AddPolicy("RequireAdminManagerRole", policy =>
        policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") || context.User.IsInRole("Manager")));
});
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IMealPlanRepository, MealPlanRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
builder.Services.AddSingleton(payOS);
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
.WithOrigins("http://localhost:4200"));

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedTasteProfile(context);
    await Seed.SeedUser(userManager, roleManager);
    await Seed.SeedSubscription(context);
    await Seed.SeedMealPlanType(context);
    await Seed.SeedMealPlan(context);
    await Seed.SeedMealPlanSubscription(context);
    await Seed.SeedIngredient(context);
    await Seed.SeedCategory(context);
    await Seed.SeedComplexity(context);
    await Seed.SeedRecipe(context);

}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error while seeding data");
}

app.Run();
