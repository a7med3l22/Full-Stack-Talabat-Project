﻿using CoreLayer.models.Order;
using CoreLayer.models.ProductModels;
using RepositoryLayer.GenericRepository;
using System.Text.Json;

namespace Talabat_APIs.Seeddata
{
	public static class AddSeedToDB
	{
		public async static Task AddJsonFilesToDB(ApplicationContext context)
		{
			
			var product = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Seeddata", "Products.json"));
			var category = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Seeddata", "Categories.json"));
			var brand = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Seeddata", "Brands.json"));
			var DeliveryMethodJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Seeddata", "DeliveryMethod.json"));

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
