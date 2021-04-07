using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace petshop.ModelsEF
{
    public partial class DBPetContext : DbContext
    {
        public DBPetContext()
        {
        }

        public DBPetContext(DbContextOptions<DBPetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Housing> Housings { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<PetOwner> PetOwners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=cloud129.p80.com.br;Initial Catalog=DBPet;Persist Security Info=True;User ID=pet;Password=@pet123");
            }
        }

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
