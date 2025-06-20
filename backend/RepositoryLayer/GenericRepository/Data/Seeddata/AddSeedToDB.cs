using CoreLayer.models.Order;
using CoreLayer.models.ProductModels;
using RepositoryLayer.GenericRepository.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer.GenericRepository.Data.Seeddata
{
	public static class AddSeedToDB
	{
		public async static Task AddJsonFilesToDB(ApplicationContext context)
		{
			
			var product = File.ReadAllText("../RepositoryLayer/GenericRepository/Data/Seeddata/Products.json");
			var category = File.ReadAllText("../RepositoryLayer/GenericRepository/Data/Seeddata/Categories.json");
			var brand = File.ReadAllText("../RepositoryLayer/GenericRepository/Data/Seeddata/Brands.json");
			var DeliveryMethodJson = File.ReadAllText("../RepositoryLayer/GenericRepository/Data/Seeddata/DeliveryMethod.json");

			if(!context.DeliveryMethods.Any())
			{
				var DeliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodJson);
				if (DeliveryMethod?.Any()==true)
				{
					await context.DeliveryMethods.AddRangeAsync(DeliveryMethod);
				}
			}

			if (!context.Products.Any())
			{
				var productList = JsonSerializer.Deserialize<List<Product>>(product);
				if (productList?.Any()==true)
				{
					await context.Products.AddRangeAsync(productList);
				}
			}


			if (!context.Products.Any())
			{
				var categoryList = JsonSerializer.Deserialize<List<Category>>(category);
				if (categoryList?.Any() == true)
				{
					await context.Categories.AddRangeAsync(categoryList);
				}
			}

			if (!context.Products.Any())
			{
				var brandList = JsonSerializer.Deserialize<List<Brand>>(brand);
				if (brandList?.Any() == true)
				{
					await context.Brands.AddRangeAsync(brandList);
				}
			}
			await context.SaveChangesAsync();
		}
	}
}
