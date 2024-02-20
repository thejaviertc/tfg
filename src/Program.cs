var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middlewares
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();




app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
