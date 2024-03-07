using Microsoft.EntityFrameworkCore;

namespace TfgTemporalName.Models;

public class TfgTemporalNameContext : DbContext
{
	public DbSet<User> Users { get; set; }

	private readonly IConfiguration _configuration;

	public TfgTemporalNameContext(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseMySQL(_configuration["MySQLConnectionString"]!).UseSnakeCaseNamingConvention();
	}
}
