using BCPAO.PermitManager.Data;
using BCPAO.PermitManager.Data.Entities;
using BCPAO.PermitManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCPAO.PermitManager
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		private readonly IHostingEnvironment _hostingEnv;

		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnv)
		{
			Configuration = configuration;
			_hostingEnv = hostingEnv;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DatabaseContext>(options =>
				 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<User, Role>()
				 .AddEntityFrameworkStores<DatabaseContext>()
				 .AddUserStore<UserStore<User, Role, DatabaseContext, int>>()
				 .AddRoleStore<RoleStore<Role, DatabaseContext, int>>()
				 .AddDefaultTokenProviders();

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddScoped<IPermitRepository, PermitRepository>();

			// Requires all authentication on all controllers. Use [AllowAnonymous] for anonymous access.
			services.AddMvc(config =>
			{
				var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
				config.Filters.Add(new AuthorizeFilter(policy));
			});
			
			var skipSSL = Configuration.GetValue<bool>("LocalTest:skipSSL");
			services.Configure<MvcOptions>(options =>
			{
				if (_hostingEnv.IsDevelopment() && !skipSSL)
				{
					options.Filters.Add(new RequireHttpsAttribute());
				}
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
						 name: "default",
						 template: "{controller=Home}/{action=Index}/{id?}");
			});

			var defaultAdminPwd = Configuration["defaultAdminPwd"];
			
			//try
			//{
			//	SeedData.Initialize(app.ApplicationServices, defaultAdminPwd).Wait();
			//}
			//catch (Exception)
			//{
			//	throw;
			//}

			//using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			//{
			//	if (!serviceScope.ServiceProvider.GetService<DatabaseContext>().AllMigrationsApplied())
			//	{
			//		serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
			//		serviceScope.ServiceProvider.GetService<DatabaseContext>().EnsureSeeded();
			//	}
			//}
		}
	}
}
