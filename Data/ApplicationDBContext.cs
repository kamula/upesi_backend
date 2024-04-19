using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, ApplicationRole, Guid>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        // public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FundsTransfer> FundsTransfers { get; set; }
        public DbSet<AtmWithdraw> AtmWithdraws { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for Account
            modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);

            // Configure the relationship between Account and FundsTransfer for SourceAccount
            modelBuilder.Entity<FundsTransfer>()
                .HasOne(ft => ft.SourceAccount)
                .WithMany(a => a.FundsTransfers)
                .HasForeignKey(ft => ft.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict); // This prevents cascade delete

            // Configure the relationship between Account and FundsTransfer for DestinationAccount
            modelBuilder.Entity<FundsTransfer>()
                .HasOne(ft => ft.DestinationAccount)
                .WithMany() // No navigation property back to FundsTransfer in Account
                .HasForeignKey(ft => ft.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict); // This prevents cascade delete

            // Setting up the relationship and foreign key for User in Account
            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes all accounts if the user is deleted

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