using BCPAO.PermitManager.Data;
using BCPAO.PermitManager.Data.Entities;
using BCPAO.PermitManager.Data.Extensions;
using BCPAO.PermitManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCPAO.PermitManager
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

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

			services.AddMvc();
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

			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				if (!serviceScope.ServiceProvider.GetService<DatabaseContext>().AllMigrationsApplied())
				{
					serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
					serviceScope.ServiceProvider.GetService<DatabaseContext>().EnsureSeeded();
				}
			}
		}
	}
}
