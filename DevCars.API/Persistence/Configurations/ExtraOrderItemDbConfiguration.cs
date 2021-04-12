﻿using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class ExtraOrderItemDbConfiguration : IEntityTypeConfiguration<ExtraOrderItem>
    {
        public void Configure(EntityTypeBuilder<ExtraOrderItem> builder)
        {
            //Primary key
            builder
                .HasKey(e => e.Id);

            //Nomencladura de tabela - Opcional
            builder
                .ToTable("tb_ExtraOrderItem");
        }
    }
}
