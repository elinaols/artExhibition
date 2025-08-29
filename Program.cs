// Using statement som använder cookies för att spara och veta vem den inloggade användaren är (för autentisering)
using Microsoft.AspNetCore.Authentication.Cookies;
// Skapar en builder för att kunna konfigurera appen
var builder = WebApplication.CreateBuilder(args);

// Adderar tjänster för controller med vyer
builder.Services.AddControllersWithViews();

// Konfigurerar autentisering med cookies för inloggning. LoginPath förklarar var inloggningen sker
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/User/Index");

// Bygger och konfigurerar applikationen
var app = builder.Build();

// Konfigurera HTTP-förfrågningspipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
// Aktiverar statiska filer, ex. CSS eller JS
app.UseStaticFiles();

// Aktiverar routing som möjliggör koppling av URL:er till rätt controller och action
app.UseRouting();

// Aktiverar login-funktion (ska ligga före UseAuthorization). Kontrollerar inlogg med cookies
app.UseAuthentication();

// Används för att kontrollera användarrättigheter - bestämmer vad användaren har rätt att göra
app.UseAuthorization();

// Standardrutt som används om ingen specifik rutt matchar, dirigeras då till home/index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Lyssnar efter inkommande förfrågningar
app.Run();