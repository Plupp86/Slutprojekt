using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Slutprojekt.Models;

namespace Slutprojekt
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SlutprojektDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(o => o.LoginPath = "/Index");

			services.AddDbContext<IdentityDbContext>(o =>
			o.UseSqlServer(connString));

			services.AddTransient<AccountRepository>();

			services.AddIdentity<IdentityUser, IdentityRole>(o =>
			{
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 6;
			})
				.AddEntityFrameworkStores<IdentityDbContext>()
				.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(
				o => o.LoginPath = "/Index");


			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvc();

		}
	}
}
