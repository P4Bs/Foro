using ForoWebApp.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Mongo Database Configuration
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
	var dbSettings = serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value;
	return new MongoClient(dbSettings.ConnectionString);
});
#endregion

#region Entity Repositories Registration
//TODO: REGISTRATE REPOSITORIES
//builder.Services.AddScoped();

#endregion


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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllers();

app.Run();
