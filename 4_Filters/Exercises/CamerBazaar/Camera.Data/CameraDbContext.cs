﻿
namespace Camera.Data
{
    using Camera.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CameraDbContext : IdentityDbContext<User>
    {
        public CameraDbContext(DbContextOptions<CameraDbContext> options)
            : base(options)
        {
        }

        public DbSet<Camera> Cameras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<User>()
                .HasMany(u => u.Cameras)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}
