using CoreLayer.Interfaces;
using CoreLayer.models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.BasketRepository;
using RepositoryLayer.IdentityRepository;
using RepositoryLayer.Implmented_Interfaces;
using RepositoryLayer.UnitOfWork;
using ServiceLayer;
using ServiceLayer.Cache_Service;
using ServiceLayer.Entity_Service;
using ServiceLayer.Order_Service;
using ServiceLayer.Token;
using StackExchange.Redis;
using System.Reflection;
using System.Text;
using Talabat_APIs.CustomMiddleWares;
using Talabat_APIs.HandelErrors;
using Talabat_APIs.Mapping;

namespace Talabat_APIs.TranslatedCodeFromProgram
{
	public static class TranslatedCustomServer
	{
		public static IServiceCollection TranslatedServer(this IServiceCollection server,IConfiguration config)
		{
			
			//server.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			server.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			server.AddTransient<HandleErrorMiddleWare>();
			server.Configure<ApiBehaviorOptions>(
				options =>
				{
					options.InvalidModelStateResponseFactory = actionContext =>
					{
						var errors = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
						var errorResponse = new validationError(errors);
						return new BadRequestObjectResult(errorResponse);

					};
				}

				);

			server.AddSingleton<IConnectionMultiplexer>(
				serverProvider=>
				{
					var connection=serverProvider.GetRequiredService<IConfiguration>()["Redis"];
					return ConnectionMultiplexer.Connect(connection!);
				}

				
				);

			server.AddScoped<IBasketRepository, BasketRepository>();
			server.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			server.AddScoped<IJWTToken, JwtToken>();
			server.AddScoped<IUnitOfWork,UnitOfWork>();
			server.AddScoped<IOrderService,OrderService>();
			server.AddScoped<IProductService,ProductService>();
			server.AddScoped<IPaymentService, PaymentService>();
			server.AddScoped<IResponseCacheService, ResponseCacheService>();
			server.AddScoped<IApiUrl, ApiUrl>(); // For getting the API URL it depend on HTTP Context AND HTTP Context is available in Scoped Lifetime
			server.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true, // يتأكد من الـ issuer (اللي أنشأ التوكن)
						ValidIssuer = config["JWT:issuer"],

						ValidateAudience = true, // يتأكد من الـ audience (اللي المفروض يستلم التوكن)
						ValidAudience = config["JWT:audience"],

						ValidateLifetime = true, // يتأكد إن التوكن لسه صالح (ما انتهتش صلاحيته)
						RequireExpirationTime = true, // لازم التوكن يكون فيه وقت انتهاء `exp`

						ValidateIssuerSigningKey = true, // يتأكد إن المفتاح المستخدم لتوقيع التوكن صح
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(config["JWT:key"]!)
						),

						ClockSkew = TimeSpan.Zero // ما يسمحش بأي تأخير (افتراضي كان 5 دقايق)
					};
				});




			return server;
		}
	}
}
