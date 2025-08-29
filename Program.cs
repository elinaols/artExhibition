// Using statement som anv�nder cookies f�r att spara och veta vem den inloggade anv�ndaren �r (f�r autentisering)
using Microsoft.AspNetCore.Authentication.Cookies;
// Skapar en builder f�r att kunna konfigurera appen
var builder = WebApplication.CreateBuilder(args);

// Adderar tj�nster f�r controller med vyer
builder.Services.AddControllersWithViews();

// Konfigurerar autentisering med cookies f�r inloggning. LoginPath f�rklarar var inloggningen sker
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/User/Index");

// Bygger och konfigurerar applikationen
var app = builder.Build();

// Konfigurera HTTP-f�rfr�gningspipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
// Aktiverar statiska filer, ex. CSS eller JS
app.UseStaticFiles();

// Aktiverar routing som m�jligg�r koppling av URL:er till r�tt controller och action
app.UseRouting();

// Aktiverar login-funktion (ska ligga f�re UseAuthorization). Kontrollerar inlogg med cookies
app.UseAuthentication();

// Anv�nds f�r att kontrollera anv�ndarr�ttigheter - best�mmer vad anv�ndaren har r�tt att g�ra
app.UseAuthorization();

// Standardrutt som anv�nds om ingen specifik rutt matchar, dirigeras d� till home/index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Lyssnar efter inkommande f�rfr�gningar
app.Run();