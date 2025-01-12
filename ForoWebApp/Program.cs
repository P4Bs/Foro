using ForoWebApp.Database;
using ForoWebApp.Models.Settings;
using ForoWebApp.Services;
using ForoWebApp.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Mongo Database Configuration
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<DbContext>();
builder.Services.AddScoped<UnitOfWork>();
#endregion

builder.Services.AddSingleton<AuthenticationHelper>();

#region Entity Repositories Registration
//TODO: REGISTRATE REPOSITORIES
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<ThreadService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<UserService>();
#endregion

builder.Services.AddAuthentication(configuration =>
{
    configuration.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configuration.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(configuration =>
{
    configuration.RequireHttpsMetadata = false;
    configuration.SaveToken = true;
    configuration.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Secret").ToString())),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.MapControllers();

app.Run();
