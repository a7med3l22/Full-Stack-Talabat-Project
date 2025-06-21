using Microsoft.EntityFrameworkCore;
using RepositoryLayer.GenericRepository;
using RepositoryLayer.IdentityRepository;
using Talabat_APIs.TranslatedCodeFromProgram;

namespace Talabat_APIs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
             //when make publish it will listen on 7039 Https
			builder.WebHost.ConfigureKestrel(options =>
			{
				options.ListenLocalhost(7039, listenOptions =>
				{
					listenOptions.UseHttps(); // استخدام شهادة dev-certs
				});
			});

			// Add services to the container.
			builder.Services.AddCors(
                opt=>
                {
                    opt.AddPolicy("AllowAngular", policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["AngularLocalHost"]!);
                    });
				}
                
                
                );
  

            builder.Services.AddControllers().AddNewtonsoftJson(
                opt=>opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );


			builder.Services.AddDbContext<ApplicationContext>(options =>
			   options/*.UseLazyLoadingProxies()*/.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));


			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.TranslatedServer(builder.Configuration); // My Own Extension Method

			var app = builder.Build();
            app.UseCors("AllowAngular");

            await app.TranslatedApp(); // My Own Extension Method
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

          //  app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
