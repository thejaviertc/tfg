using TfgTemporalName.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();

builder.Services.AddDbContext<TfgTemporalNameContext>();

// TODO: Disable on release
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
