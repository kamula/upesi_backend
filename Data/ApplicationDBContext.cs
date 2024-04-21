using System;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, ApplicationRole, Guid>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<FundsTransfer> FundsTransfers { get; set; }
        public DbSet<AtmWithdraw> AtmWithdraws { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-one relationship between User and Account
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId);

            // Configure the primary key for Account
            modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);

            // Configure the relationship between Account and FundsTransfer for SourceAccount
            modelBuilder.Entity<FundsTransfer>()
                .HasOne(ft => ft.SourceAccount)
                .WithMany(a => a.FundsTransfers)
                .HasForeignKey(ft => ft.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure the relationship between Account and FundsTransfer for DestinationAccount
            modelBuilder.Entity<FundsTransfer>()
                .HasOne(ft => ft.DestinationAccount)
                .WithMany() // No navigation property back to FundsTransfers in Account
                .HasForeignKey(ft => ft.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Seed user roles
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}
