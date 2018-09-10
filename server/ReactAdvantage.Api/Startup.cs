﻿using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactAdvantage.Api.GraphQLSchema;
using ReactAdvantage.Data;

namespace ReactAdvantage.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddDbContext<ReactAdvantageContext>(options =>
                options.UseSqlServer(connectionString));

            // Add application services.
            services.AddTransient<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<ReactAdvantageQuery>();
            services.AddTransient<ReactAdvantageMutation>();
            services.AddTransient<TaskType>();
            services.AddTransient<TaskInputType>();
            services.AddTransient<UserType>();
            services.AddTransient<UserInputType>();
            services.AddTransient<ProjectType>();
            services.AddTransient<ProjectInputType>();
            var sp = services.BuildServiceProvider();
            services.AddTransient<ISchema>(x => new ReactAdvantageSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            services.AddMvc();
            
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44338";
                    options.RequireHttpsMetadata = true;

                    options.ApiName = "ReactAdvantageApi";
                });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:44398")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                ILoggerFactory loggerFactory, ReactAdvantageContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors("default");

            app.UseAuthentication();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            db.EnsureSeedData();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
