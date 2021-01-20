using Microsoft.EntityFrameworkCore;
using ZooService.Models;

namespace ZooService
{
    /// <summary>
    /// Zoo Context Class
    /// </summary>
    public class ZooContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZooContext"/> class.
        /// </summary>
        public ZooContext():base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ZooContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ZooContext(DbContextOptions<ZooContext> options): base(options)
        {
        }

        /// <summary>
        /// Gets or sets the animal.
        /// </summary>
        /// <value>
        /// The animal.
        /// </value>
        public virtual DbSet<Animal> Animal { get; set; }
        /// <summary>
        /// Gets or sets the type of the animal.
        /// </summary>
        /// <value>
        /// The type of the animal.
        /// </value>
        public virtual DbSet<AnimalType> AnimalType { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd()
                    .HasComment("Primary key animal table");

                entity.ToTable("Animal");
                entity.HasComment("Store data information about animals");

                entity.HasIndex(e => e.IdAnimalType)
                    .HasName("ANIMAL_ANIMAL_TYPE_FK");

                entity.Property(e => e.IdAnimalType)
                    .IsRequired()
                    .HasColumnName("Id_AnimalType")
                    .HasComment("Foreign Key for table animal type.");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("Code")
                    .HasMaxLength(16)
                    .HasComment("Code for the animal register.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(64)
                    .HasComment("Code for the animal register.");

                entity.Property(e => e.DateAdmission)
                    .IsRequired()
                    .HasColumnName("DateAdmission")
                    .HasColumnType("timestamp")
                    .HasComment("Date for the register was entered the zoo.");

                entity.Property(e => e.Weight)
                   .IsRequired()
                   .HasColumnName("Weight")
                   .HasColumnType("numeric(8,2)")
                   .HasComment("Weight for the animal register.");

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasColumnName("DateCreated")
                    .HasColumnType("timestamp")
                    .HasComment("Date for the register was created in database.");

                entity.Property(e => e.DateUpdated)
                    .HasColumnName("DateUpdated")
                    .HasColumnType("timestamp")
                    .HasComment("Date for the register was updated in database.");

                entity.Property(e => e.UserCreated)
                    .IsRequired()
                    .HasColumnName("UserCreated")
                    .HasColumnType("uuid")
                    .HasComment("User that to register in database.");

                entity.Property(e => e.UserUpdated)
                    .HasColumnName("UserUpdated")
                    .HasColumnType("uuid")
                    .HasComment("User that to update in database.");

                entity.HasOne(d => d.AnimalType)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.IdAnimalType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ANIMAL_ANIMAL_TYPE");
            });

            modelBuilder.Entity<AnimalType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd()
                    .HasComment("Primary key animal type table");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("Code")
                    .HasMaxLength(16)
                    .HasComment("Code for the animal register.");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(32)
                    .HasComment("Code for the animal register.");

                #region Seed Data
                entity.HasData(new AnimalType()
                {
                    Id = 1,
                    Code = "LN",
                    Name = "León"
                },
                new AnimalType()
                {
                    Id = 2,
                    Code = "TG",
                    Name = "Tigre"
                },
                new AnimalType()
                {
                    Id = 3,
                    Code = "ELF",
                    Name = "Elefante"
                });
                #endregion
            });
        }
    }
}
