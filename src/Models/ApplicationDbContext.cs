using Microsoft.EntityFrameworkCore;

namespace ConectaTfg.Models;

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
		modelBuilder
			.Entity<Topic>()
			.HasOne(t => t.User)
			.WithMany(u => u.Topics)
			.HasForeignKey(t => t.UserId)
			.IsRequired();

		modelBuilder
			.Entity<Topic>()
			.HasOne(t => t.UserRequestered)
			.WithMany(u => u.RequestedTopics)
			.HasForeignKey(t => t.UserIdRequested);

		modelBuilder
			.Entity<Idea>()
			.HasOne(i => i.User)
			.WithMany(u => u.Ideas)
			.HasForeignKey(i => i.UserId)
			.IsRequired();

		modelBuilder
			.Entity<Idea>()
			.HasOne(i => i.UserRequestered)
			.WithMany(u => u.RequestedIdeas)
			.HasForeignKey(i => i.UserIdRequested);
	}
}
