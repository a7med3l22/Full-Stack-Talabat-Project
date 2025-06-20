using CoreLayer.models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.GenericRepository.Configrations
{
	public class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(o => o.orderState).HasConversion(
				o => o.ToString(), //save to Db
				o=> Enum.Parse<OrderState>(o) //return from Db
				);
			builder.OwnsOne(o => o.shippingAddress);
			builder.HasOne(o=>o.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
			builder.HasMany(o => o.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade).IsRequired();//.IsRequired() //to ensure that forigenKey cannot be null
			builder.Property(o => o.SubTotal).HasPrecision(12, 2);

		}
	}
}
