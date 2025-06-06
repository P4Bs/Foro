using ForoWebApp.Constants;
using ForoWebApp.Database;
using ForoWebApp.Helpers.Passwords;
using ForoWebApp.Managers;
using ForoWebApp.Models.Domain;
using ForoWebApp.Models.Settings;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Add Configuration
builder.Configuration.Sources.Clear();
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", optional: false)
    .AddUserSecrets(Assembly.GetEntryAssembly());
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, UserConstants.AdminRole));

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

#region Mongo Database Configuration
var dbConfig = new DatabaseConfiguration(
    Environment.GetEnvironmentVariable("DB__CONNECTION__STRING"),
    Environment.GetEnvironmentVariable("DB__NAME")
);
builder.Services.AddSingleton(dbConfig);
builder.Services.AddScoped<DbContext>();
builder.Services.AddScoped<UnitOfWork>();
#endregion

#region Entity Services
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<ThreadService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<UserService>();
#endregion

#region Managers and Helpers
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<CredentialsManager>();
builder.Services.AddSingleton<IPasswordHelper, PasswordHelper>();
#endregion

#region Authentication
var signingKey = Environment.GetEnvironmentVariable("SECRET__KEY");
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
    options.AccessDeniedPath = "/User/AccessDenied";
})
.AddJwtBearer(configuration =>
{
    configuration.RequireHttpsMetadata = false;
    configuration.SaveToken = true;
    configuration.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey)),
    };
});
#endregion

#region MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
#endregion

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();
