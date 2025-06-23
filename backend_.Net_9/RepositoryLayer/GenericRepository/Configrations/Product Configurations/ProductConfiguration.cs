﻿using CoreLayer.models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.GenericRepository.Configrations
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(p => p.Name)
				.IsRequired();
				

			builder.Property(p => p.Description).IsRequired();

			builder.Property(p => p.Price)
				.HasPrecision(18, 2);

			builder.Property(p => p.PictureUrl)
				.IsRequired();

			builder.HasOne(p=>p.Category).WithMany()
				.HasForeignKey(p => p.CategoryId);

			builder.HasOne(p => p.Brand).WithMany()
				.HasForeignKey(p => p.BrandId);
		}
	}
}
