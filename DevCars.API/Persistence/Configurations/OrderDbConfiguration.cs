﻿using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class OrderDbConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Primary key
           builder
              .HasKey(o => o.Id);

           builder
                .HasMany(o => o.ExtraItems)
                .WithOne()
                .HasForeignKey(e => e.IdOrder)
                .OnDelete(DeleteBehavior.Restrict);

           builder
                .HasOne(o => o.Car)
                .WithOne()
                .HasForeignKey<Order>(o => o.IdCar)
                .OnDelete(DeleteBehavior.Restrict);//Caso não especifique utiliza o Cascade por padrão

            //Nomencladura de tabela - Opcional
            builder
                .ToTable("tb_Order");
        }
    }
}
