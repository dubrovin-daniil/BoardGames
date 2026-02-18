using BoardGames.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BoardGames
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Session> Sessions { get; set; }

        private readonly string _connectionString;

        public AppDbContext()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json")
                .Build();

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>(b =>
            {
                b.Property(g => g.Title).HasColumnType("NVARCHAR").HasMaxLength(100).IsRequired();
                b.HasIndex(g => g.Title).IsUnique();
                b.ToTable(g =>
                {
                    g.HasCheckConstraint("CK_Game_MinPlayers", "MinPlayers > 0");
                    g.HasCheckConstraint("CK_Game_MaxPlayers", "MaxPlayers > 0");
                });
            });

            modelBuilder.Entity<Member>().Property(m => m.JoinDate).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Session>().Property(s => s.Date).HasColumnType("DATETIME").HasDefaultValueSql("GETDATE()");
        }
    }
}
