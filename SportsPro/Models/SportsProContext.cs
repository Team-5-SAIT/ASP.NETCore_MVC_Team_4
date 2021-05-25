using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SportsPro.Models
{
    public partial class SportsProContext : DbContext
    {
        public SportsProContext()
        {
        }

        public SportsProContext(DbContextOptions<SportsProContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Incidents> Incidents { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Registrations> Registrations { get; set; }
        public virtual DbSet<Technicians> Technicians { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SportsPro;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.HasIndex(e => e.CountryId);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.CountryId)
                    .IsRequired()
                    .HasColumnName("CountryID");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.PostalCode).IsRequired();

                entity.Property(e => e.State).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CountryId);
            });

            modelBuilder.Entity<Incidents>(entity =>
            {
                entity.HasKey(e => e.IncidentId);

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.TechnicianId);

                entity.Property(e => e.IncidentId).HasColumnName("IncidentID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.Technician)
                    .WithMany(p => p.Incidents)
                    .HasForeignKey(d => d.TechnicianId);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ProductCode).IsRequired();

                entity.Property(e => e.YearlyPrice).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Registrations>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductName).IsRequired();
            });

            modelBuilder.Entity<Technicians>(entity =>
            {
                entity.HasKey(e => e.TechnicianId);

                entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Phone).IsRequired();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Role).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
