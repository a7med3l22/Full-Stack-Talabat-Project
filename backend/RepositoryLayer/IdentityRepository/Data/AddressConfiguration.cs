using CoreLayer.models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IdentityRepository.Data
{
	public class AddressConfiguration : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.ToTable("Addresses"); // Change the table name to "Addresses"
			builder.HasKey(a => a.Id); // Set the primary key
			builder.Property(a => a.Street).IsRequired().HasMaxLength(100);
			builder.Property(a => a.City).IsRequired().HasMaxLength(50);
			builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
			builder.Property(a => a.LastName).IsRequired().HasMaxLength(20);
			builder.Property(a => a.Country).IsRequired().HasMaxLength(50);

			builder.HasOne(a => a.ApplicationUser).WithOne(u => u.Address).HasForeignKey<Address>(a => a.ApplicationUser_Id); // One-to-One must HasForeignKey<Address> determine address
			


		}
	}
}
