using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL.Data
{
    public sealed class CommentContext : DbContext
    {
        private readonly string _dbPassword;
        private readonly string _dbUsername;

        private readonly string connectionString;

        public CommentContext()
        {
            _dbUsername = Environment.GetEnvironmentVariable("DbUsername", System.EnvironmentVariableTarget.User);
            _dbPassword = Environment.GetEnvironmentVariable("DbPassword", System.EnvironmentVariableTarget.User);

            connectionString = $"Host=localhost;Port=5432;Database=CommentsDb;Username={_dbUsername};Password={_dbPassword}";
        }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: new List<string> { "4060" });
            });

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => entity.HasCheckConstraint("email_constraint", @"""Email"" LIKE '%_@__%.__%'"));
            modelBuilder.Entity<User>(entity => entity.HasCheckConstraint("name_constraint", @"LENGTH(""Username"")>=2"));
            modelBuilder.Entity<User>(entity => entity.HasCheckConstraint("password_constraint", @"LENGTH(""Password"")>=8"));

            modelBuilder.Entity<Comment>(entity => entity.HasCheckConstraint("dogid_constraint", @"""DogID"" >=0"));
            modelBuilder.Entity<Comment>(entity => entity.HasCheckConstraint("text_constraint", @"LENGTH(""Text"") >=1"));
            modelBuilder.Entity<Comment>(entity => entity.HasCheckConstraint("ratingconstraint", @"""Rating"" >= 1 AND ""Rating"" <= 5"));


            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
            modelBuilder.Entity<User>().HasKey(user => user.Email);
            modelBuilder.Entity<User>().Property(user => user.Email).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Password).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Username).IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Password).HasMaxLength(1000);
            modelBuilder.Entity<User>().Property(user => user.AccessLevel).IsRequired();

            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Comment>().HasKey(comment => comment.CommentID);
            modelBuilder.Entity<Comment>().Property(comment => comment.CommentID).HasIdentityOptions(1, 1, 1, 2147483647, false, 1);
            modelBuilder.Entity<Comment>().Property(comment => comment.CommentID).IsRequired();
            modelBuilder.Entity<Comment>().Property(comment => comment.DogID).IsRequired();
            modelBuilder.Entity<Comment>().Property(comment => comment.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(comment => comment.Rating).IsRequired();
            modelBuilder.Entity<Comment>().Property(comment => comment.VoteCounts).HasDefaultValue(0);
            modelBuilder.Entity<Comment>().Property(comment => comment.DateTime).IsRequired();

        }

    }
}
