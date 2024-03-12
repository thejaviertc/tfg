using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TfgTemporalName.Models;
using TfgTemporalName.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>();

builder
	.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
			ValidateIssuerSigningKey = true
		};
	});

builder.Services.AddAuthorization();

// TODO: Disable on release
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();
else
{
	app.UseHttpsRedirection();
	app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
