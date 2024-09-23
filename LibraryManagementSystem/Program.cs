using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    // Giriþ - Çýkýþ - Eriþim reddi durumlarýnda
    options.LoginPath = new PathString("/");        //giriþ yaptýktan sonra anasayfaya
    options.LogoutPath = new PathString("/");       //çýkýþ yapýldýktýktan sonra anasayfaya
    options.AccessDeniedPath = new PathString("/");     //eriþim engellenirse anasayfaya gönder

});

var app = builder.Build();

app.UseAuthentication();
app.UseStaticFiles();       

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}"
    );

app.Run();
