using CollegeAis.Data.Db;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using CollegeAis.Web.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddRazorPages()
    .AddDataAnnotationsLocalization();

builder.Services.AddSingleton<IValidationAttributeAdapterProvider, RuValidationAttributeAdapterProvider>();

builder.Services.AddDbContext<CollegeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("ru-RU") };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru-RU"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


// Автоматически применяем миграции при старте (удобно для разработки)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
    db.Database.Migrate();
}

app.MapRazorPages();

app.Run();