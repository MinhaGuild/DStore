using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ChangePassword> ChangePassword { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(c => c.Updated).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Role>().Property(c => c.Updated).HasDefaultValueSql("GETDATE()");
        }
    }
}
