using CRM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infraestructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<SalesOpportunity> SalesOpportunities { get; set; }

        public DbSet<SupportCase> SupportCases { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Autoincrement for Guids

            modelBuilder.Entity<Activity>()
                .Property(a => a.IdActivity)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Contact>()
                .Property(c => c.IdContact)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .Property(c => c.IdCustomer)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Invoice>()
                .Property(i => i.IdInvoice)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(o => o.IdOrder)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .Property(p => p.IdProduct)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SalesOpportunity>()
                .Property(so => so.IdSalesOpportunity)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SupportCase>()
                .Property(sc => sc.IdSupportCase)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(u => u.IdUser)
                .ValueGeneratedOnAdd();

            // Foreing Key Constraints
            // Activity to Customer
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(a => a.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            // Activity to Contact
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Contact)
                .WithMany()
                .HasForeignKey(a => a.IdContact)
                .OnDelete(DeleteBehavior.Restrict);

            // Activity to User (CreatedBy)
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.CreatedByUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Activity to User (UpdatedBy)
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.UpdatedByUser)
                .WithMany()
                .HasForeignKey(a => a.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Contact to Customer
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            // Contact to User (CreatedBy)
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Contact to User (UpdatedBy)
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Customer to User (CreatedBy)
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer to User (UpdatedBy)
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Customer to SalesOpportunities
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.SalesOpportunities)
                .WithOne(so => so.Customer)
                .HasForeignKey(so => so.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice to Order
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Order)
                .WithMany()
                .HasForeignKey(i => i.IdOrder)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice to User (CreatedBy)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.CreatedByUser)
                .WithMany()
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice to User (UpdatedBy)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.UpdatedByUser)
                .WithMany()
                .HasForeignKey(i => i.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Order to Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            // Order to User (CreatedBy)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.CreatedByUser)
                .WithMany()
                .HasForeignKey(o => o.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Order to User (UpdatedBy)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.UpdatedByUser)
                .WithMany()
                .HasForeignKey(o => o.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Product to User (CreatedBy)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Product to User (UpdatedBy)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // SalesOpportunity to Customer
            modelBuilder.Entity<SalesOpportunity>()
                .HasOne(so => so.Customer)
                .WithMany(c => c.SalesOpportunities)
                .HasForeignKey(so => so.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            // SalesOpportunity to User (CreatedBy)
            modelBuilder.Entity<SalesOpportunity>()
                .HasOne(so => so.CreatedByUser)
                .WithMany()
                .HasForeignKey(so => so.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // SalesOpportunity to User (UpdatedBy)
            modelBuilder.Entity<SalesOpportunity>()
                .HasOne(so => so.UpdatedByUser)
                .WithMany()
                .HasForeignKey(so => so.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // SupportCase to Customer
            modelBuilder.Entity<SupportCase>()
                .HasOne(sc => sc.Customer)
                .WithMany(c => c.SupportCases)
                .HasForeignKey(sc => sc.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            // SupportCase to Contact
            modelBuilder.Entity<SupportCase>()
                .HasOne(sc => sc.Contact)
                .WithMany()
                .HasForeignKey(sc => sc.IdContact)
                .OnDelete(DeleteBehavior.Restrict);

            // SupportCase to User (CreatedBy)
            modelBuilder.Entity<SupportCase>()
                .HasOne(sc => sc.CreatedByUser)
                .WithMany()
                .HasForeignKey(sc => sc.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // SupportCase to User (UpdatedBy)
            modelBuilder.Entity<SupportCase>()
                .HasOne(sc => sc.UpdatedByUser)
                .WithMany()
                .HasForeignKey(sc => sc.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // User to User (CreatedBy)
            modelBuilder.Entity<User>()
                .HasOne(u => u.CreatedByUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // User to User (UpdatedBy)
            modelBuilder.Entity<User>()
                .HasOne(u => u.UpdatedByUser)
                .WithMany()
                .HasForeignKey(u => u.UpdatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
