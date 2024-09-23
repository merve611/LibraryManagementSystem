using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    // Giri� - ��k�� - Eri�im reddi durumlar�nda
    options.LoginPath = new PathString("/");        //giri� yapt�ktan sonra anasayfaya
    options.LogoutPath = new PathString("/");       //��k�� yap�ld�kt�ktan sonra anasayfaya
    options.AccessDeniedPath = new PathString("/");     //eri�im engellenirse anasayfaya g�nder

});

var app = builder.Build();

app.UseAuthentication();
app.UseStaticFiles();       

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}"
    );

app.Run();
