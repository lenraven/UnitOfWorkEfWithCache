using Microsoft.EntityFrameworkCore;

using UnitOfWorkSample.Interface;
using UnitOfWorkSample.Model;

namespace UnitOfWorkSample.Implementation.Sql.Context
{
    public class DemoDbContext : DbContext, IUnitOfWork, ICacheUnitOfWork
    {
        public DbSet<Locale> Locales => Set<Locale>();
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Partner> Partners => Set<Partner>();

        public DemoDbContext()
        {
        }

        public DemoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=UnitOfWorkSample;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Locale>().HasKey(p => p.Id);
            modelBuilder.Entity<Locale>().Property(p => p.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .IsUnicode(false);
            modelBuilder.Entity<Locale>().HasData(
                new Locale {Id = "hu-HU"},
                new Locale {Id = "en-GB"},
                new Locale {Id = "de-DE"});

            modelBuilder.Entity<Partner>().HasKey(p => p.Id);
            modelBuilder.Entity<Partner>().Property(p => p.Name).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<Partner>().HasMany(p => p.Organizations).WithOne(p => p.Partner).IsRequired();

            modelBuilder.Entity<Partner.LocaleConnection>().HasOne(p => p.Partner).WithMany(p => p.Locales).HasForeignKey("PartnerId").IsRequired();
            modelBuilder.Entity<Partner.LocaleConnection>().HasOne(p => p.Locale).WithMany().HasForeignKey("LocaleId").IsRequired();
            modelBuilder.Entity<Partner.LocaleConnection>().Property<string>("LocaleId")
                .HasMaxLength(5)
                .IsFixedLength()
                .IsUnicode(false);
            modelBuilder.Entity<Partner.LocaleConnection>().ToTable("Locales", "Partner").HasKey("PartnerId", "LocaleId");

            modelBuilder.Entity<Organization>().HasKey(p => p.Id);
            modelBuilder.Entity<Organization>().Property(p => p.Name).HasMaxLength(250).IsRequired();
        }

        void ICacheUnitOfWork.Attach<TEntity>(TEntity entity)
            where TEntity : class
        {
            Attach(entity);
        }
    }
}
