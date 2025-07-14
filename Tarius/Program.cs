using Tarius.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de la autenticación basada en cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/AccessDenied";
        options.Cookie.Name = "TariusAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
    });

// Aquí agregamos soporte para controladores de tipo API
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


// También seguimos soportando vistas
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true; // Opcional, para que el JSON sea legible
    });

var app = builder.Build();

// Muestra detalles de errores solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Mapear rutas para controladores MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapear rutas para controladores de API (como ApiRecetasController)
app.MapControllers(); // <-- esto habilita la API

app.Run();
