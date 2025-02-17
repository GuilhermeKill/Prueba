using Microsoft.EntityFrameworkCore;
using Prueba.DB.Models;

namespace  Prueba.DB
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<CarModel> Cars { get; set; }
        public DbSet<CountryModel> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.Property(e => e.brand).HasMaxLength(200).IsRequired();

                entity.Property(e => e.code_vin).HasMaxLength(17).IsRequired();

                entity.Property(e => e.model).HasMaxLength(200).IsRequired();

                entity.Property(e => e.year).IsRequired();

                entity.Property(e => e.patent).HasMaxLength(200).IsRequired();

                entity.Property(e => e.country).HasMaxLength(100).IsRequired();



                entity.Property(e => e.patent).HasMaxLength(8);

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<CountryModel>(entity =>
           {
               entity.HasKey(e => e.Id);

               entity.Property(e => e.Name).HasMaxLength(200).IsRequired();

           });

            OnModelCreatingPartial(modelBuilder);
        }        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
