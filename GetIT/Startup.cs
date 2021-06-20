using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetIT.Context;
using GetIT.DatabaseLayer.Dto;
using GetIT.DatabaseLayer.Repository.Implementation;
using GetIT.DatabaseLayer.Repository.Interface;
using GetIT.Helpers.Implementation;
using GetIT.Helpers.Interface;
using GetIT.Utility.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace GetIT
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("GetITDbConnection")));
            services.Configure<AzureStorageConfig>(_config.GetSection("xxxxx"));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSingleton<IImageStorageHelper, ImageStorageHelper_Local>();

            var mailKitOptions = _config.GetSection("MailKitOptions").Get<MailKitOptions>();

            services.AddMailKit(config => { config.UseMailKit(mailKitOptions);});

            services.AddIdentity<ApplicationUser, IdentityRole>(options=>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseExceptionHandler("/Error/");
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");                
                //app.UseDeveloperExceptionPage();                
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name : "default",
                    template : "{controller=Home}/{action=Index}/{id?}");
            } );
            
        }
    }
}
