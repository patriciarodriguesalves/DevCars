using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //Primary key
            builder
             .HasKey(c => c.Id);

            builder
              .HasMany(c => c.Orders)
              .WithOne(o => o.Customer)
              .HasForeignKey(o => o.IdCustomer)
              .OnDelete(DeleteBehavior.Restrict); //Caso não especifique utiliza o Cascade por padrão

            //Nomencladura de tabela - Opcional
            builder
             .ToTable("tb_Customer");
        }
    }
}
