using Microsoft.EntityFrameworkCore;

namespace TfgTemporalName.Models;

public class ApplicationDbContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public DbSet<Topic> Topics { get; set; }

	public DbSet<Idea> Ideas { get; set; }

	private readonly IConfiguration _configuration;

	public ApplicationDbContext(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseMySQL(_configuration["MySQLConnectionString"]!).UseSnakeCaseNamingConvention();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>().HasMany(u => u.Topics).WithOne().HasForeignKey(t => t.UserId).IsRequired();
		modelBuilder.Entity<User>().HasMany(u => u.Ideas).WithOne().HasForeignKey(t => t.UserId).IsRequired();
	}
}
