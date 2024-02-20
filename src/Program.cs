var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
