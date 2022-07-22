using Microsoft.EntityFrameworkCore;
using Movie.Models;

namespace Movie.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public MovieContext(DbContextOptions<MovieContext> dbContext)
            : base(dbContext)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Movie.Models.Film
            modelBuilder.Entity<Film>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Film>().Property(x => x.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Film>().Property(x => x.Description).IsRequired().HasColumnType("varchar").HasMaxLength(800);
            modelBuilder.Entity<Film>().Property(x => x.Evaluation).IsRequired().HasColumnType("float").HasMaxLength(1);
            modelBuilder.Entity<Film>().Property(x => x.PosterPath).IsRequired().HasColumnType("varchar").HasMaxLength(300);

            //Movie.Models.Serial
            modelBuilder.Entity<Serial>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Serial>().Property(x => x.Name).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Serial>().Property(x => x.Season).IsRequired().HasColumnType("int");
            modelBuilder.Entity<Serial>().Property(x => x.Episode).IsRequired().HasColumnType("int");
            modelBuilder.Entity<Serial>().Property(x => x.Completed).IsRequired().HasColumnType("bool").HasDefaultValue(false);
            modelBuilder.Entity<Serial>().Property(x => x.Description).IsRequired().HasColumnType("varchar").HasMaxLength(800);
            modelBuilder.Entity<Serial>().Property(x => x.Evaluation).IsRequired().HasColumnType("float").HasMaxLength(1);
            modelBuilder.Entity<Serial>().Property(x => x.PosterPath).IsRequired().HasColumnType("varchar").HasMaxLength(300);

            //Movie.Models.User
            modelBuilder.Entity<Admin>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Admin>().Property(x => x.Login).IsRequired().HasColumnType("varchar").HasMaxLength(100);
            modelBuilder.Entity<Admin>().Property(x => x.Password).IsRequired().HasColumnType("varchar").HasMaxLength(100);


            base.OnModelCreating(modelBuilder);
        }
    }
}
