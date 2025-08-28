using AutomobileInsuranceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomobileInsuranceSystem.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<OptionalProduct> OptionalProducts { get; set; }
        public DbSet<ProposalOptionalProduct> ProposalOptionalProducts { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<EmailReminder> EmailReminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            // Vehicles
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Proposals
            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.User)
                .WithMany(u => u.Proposals)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Vehicle)
                .WithMany(v => v.Proposals)
                .HasForeignKey(p => p.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Policy)
                .WithMany(po => po.Proposals)
                .HasForeignKey(p => p.PolicyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proposal>()
                .HasOne(p => p.Officer)
                .WithMany()
                .HasForeignKey(p => p.OfficerId)
                .OnDelete(DeleteBehavior.NoAction);



            // ProposalOptionalProduct (Many-to-Many)
            modelBuilder.Entity<ProposalOptionalProduct>()
                .HasKey(pop => new { pop.ProposalId, pop.OptionalProductId });

            modelBuilder.Entity<ProposalOptionalProduct>()
                .HasOne(pop => pop.Proposal)
                .WithMany(p => p.ProposalOptionalProducts)
                .HasForeignKey(pop => pop.ProposalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProposalOptionalProduct>()
                .HasOne(pop => pop.OptionalProduct)
                .WithMany(op => op.ProposalOptionalProducts)
                .HasForeignKey(pop => pop.OptionalProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quote
            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Proposal)
                .WithMany(p => p.Quotes)
                .HasForeignKey(q => q.ProposalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Quote)
                .WithMany(q => q.Payments)
                .HasForeignKey(p => p.QuoteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Document
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Proposal)
                .WithMany(p => p.Documents)
                .HasForeignKey(d => d.ProposalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // EmailReminder
            modelBuilder.Entity<EmailReminder>()
                .HasKey(er => er.ReminderId);

            modelBuilder.Entity<EmailReminder>()
                .HasOne(er => er.Proposal)
                .WithMany(p => p.EmailReminders)
                .HasForeignKey(er => er.ProposalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
