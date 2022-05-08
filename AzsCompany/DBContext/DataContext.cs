using AzsCompany.Domain;
using Microsoft.EntityFrameworkCore;

namespace AzsCompany.DBContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<Azs> Azs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<NetAZS> NetAzses { get; set; }
        // public DbSet<NumberPhone> NumberPhones { get; set; }
        public DbSet<Oil> Oils { get; set; }
        public DbSet<Payed> Payeds { get; set; }
        public DbSet<CompanyCar> CompanyCars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // payed entity one to many relationship
            builder.Entity<Payed>(x => x.HasKey(p => new {p.CompanyId, p.DriverId, p.NetAzsId, p.OilId, p.CompanyCarId}));

            builder.Entity<Payed>()
                .HasOne(p => p.Company)
                .WithMany(p => p.Payeds)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payed>()
                .HasOne(p => p.Driver)
                .WithMany(p => p.Payeds)
                .HasForeignKey(p => p.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payed>()
                .HasOne(p => p.NetAzs)
                .WithMany(p => p.Payeds)
                .HasForeignKey(p => p.NetAzsId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Payed>()
                .HasOne(p => p.CompanyCar)
                .WithMany(p => p.Payeds)
                .HasForeignKey(p => p.CompanyCarId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Payed>()
                .HasOne(p => p.Oil)
                .WithMany(p => p.Payeds)
                .HasForeignKey(p => p.OilId)
                .OnDelete(DeleteBehavior.Restrict);


            // AZS one to many relationship
            // builder.Entity<Azs>()
            //     .HasOne(a => a.NetAzs)
            //     .WithMany(a => a.Azs)
            //     .HasForeignKey(a => a.NetAZSId);
            
            //CompanyCar one to many relationship
            builder.Entity<CompanyCar>()
                .HasOne(a => a.Company)
                .WithMany(a => a.CompanyCars)
                .HasForeignKey(a => a.CompanyId);

            // Driver one to many relationship
            builder.Entity<Driver>()
                .HasOne(a => a.Company)
                .WithMany(a => a.Drivers)
                .HasForeignKey(a => a.CompanyId);
            
            builder.Entity<NetAZS>()
                .HasOne(a => a.Azs)
                .WithMany(a => a.NetAzses)
                .HasForeignKey(a => a.AzsId);

            // NumberPhone one to many relationship
            // builder.Entity<NumberPhone>()
            //     .HasOne(a => a.Driver)
            //     .WithMany(a => a.Phones)
            //     .HasForeignKey(a => a.DriverId);
        } 
    }
}