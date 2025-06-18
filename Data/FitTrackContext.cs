using Microsoft.EntityFrameworkCore;

using Backend.Models;

namespace Backend.Data
{
    public class FitTrackContext : DbContext
    {
        public FitTrackContext(DbContextOptions<FitTrackContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Routine> Routines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Activity>().ToTable("activities");
            modelBuilder.Entity<Routine>().ToTable("routine");

            // Configure Activity entity - map UserId property to user_id column
            modelBuilder.Entity<Activity>()
                .Property(a => a.UserId)
                .HasColumnName("user_id");

            // Configure enum to be stored as string in database
            modelBuilder.Entity<Routine>()
                .Property(r => r.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Routine>()
                .HasOne(r => r.User)
                .WithMany(u => u.Routines)
                .HasForeignKey(r => r.user_id);

            modelBuilder.Entity<Routine>()
                .HasOne(r => r.Activity)
                .WithMany(a => a.Routines)
                .HasForeignKey(r => r.activity_id);

            // Configure User-Activity relationship
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(a => a.UserId);
        }
    }
}