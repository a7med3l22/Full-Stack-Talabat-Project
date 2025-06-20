using Talabat_APIs.CustomMiddleWares;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.GenericRepository.Data;
using RepositoryLayer.GenericRepository.Data.Seeddata;
using RepositoryLayer.GenericRepository;
using RepositoryLayer.IdentityRepository;

namespace Talabat_APIs.TranslatedCodeFromProgram
{
	public static class TranslatedCustomApp
	{
		public static async Task<WebApplication> TranslatedApp(this WebApplication app)
		{

			//auto update DataBase
			using (var scope = app.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
				var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("TranslatedCustomApp");
				var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				try
				{
					await context.Database.MigrateAsync();
					await AddSeedToDB.AddJsonFilesToDB(context);
					await identityContext.Database.MigrateAsync();
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "? An error occurred while migrating the database.");
				}
			}


			app.UseMiddleware<HandleErrorMiddleWare>(); //My Custom MiddleWare 

			


			app.UseStatusCodePagesWithReExecute("/error/{0}"); 

			return app;
		}
	}
}
