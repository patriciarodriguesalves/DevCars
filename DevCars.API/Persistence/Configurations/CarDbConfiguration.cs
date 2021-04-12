using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            //Primary Key
            builder
                .HasKey(c => c.Id);

           builder
                .Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Marca")
                .HasDefaultValue("Genérico")
                .HasColumnType("VARCHAR(100)");

            builder
                .Property(c => c.ProductionDate)
                .HasDefaultValueSql("getDate()");

            //Nomencladura de tabela - Opcional
            builder
                .ToTable("tb_Car");
        }
    }
}
