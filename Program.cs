using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TPFINALFINANZAS.Data;
using TPFINALFINANZAS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// agregar controladores con vistas
builder.Services.AddControllersWithViews();

// configurar EF Core con LocalDB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// inyeccion de dependencias de repositorios
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IGastoRepositorio, GastoRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// automapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// crear base de datos si no existe  esto evita tener que correr migraciones manualmente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
