using Microsoft.EntityFrameworkCore;
using PetShop.Model;

namespace PetShop.Repository
{
    public class PetDbContext : DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext> options) : base(options)
        { }

        public DbSet<Housing> Housings { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetOwner>().ToTable("PetOwner");

            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Housing>(entity =>
            {
                entity.ToTable("Housing");

                entity.HasOne(d => d.IdPetNavigation)
                    .WithMany(p => p.Housings)
                    .HasForeignKey(d => d.IdPet)
                    .HasConstraintName("FK_Housing_Pet");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.HealthCondition).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProfilePhotoFileName).HasMaxLength(100);

                entity.Property(e => e.ReasonForHospitalization).HasMaxLength(100);

                entity.HasOne(d => d.IdPetOwnerNavigation)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.IdPetOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_PetOwner");
            });

            modelBuilder.Entity<PetOwner>(entity =>
            {
                entity.ToTable("PetOwner");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });
        }

    }
}
