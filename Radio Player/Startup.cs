using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Radio_Player.Authentication;
using Radio_Player.Business.Abstract;
using Radio_Player.Business.Concrete;
using Radio_Player.Controllers;
using Radio_Player.Dal.Data.Abstract;
using Radio_Player.Dal.Data.Concrete;
using Radio_Player.Dal.Data.Identity;
using Radio_Player.Dal.Repositories.Abstract;
using Radio_Player.Dal.Repositories.Concrete;
using Radio_Player.Models;
using Radio_Player.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radio_Player
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
            
            services.Configure<RadioStationDatabaseSettings>(
                Configuration.GetSection(nameof(RadioStationDatabaseSettings)));

            services.AddSingleton<IRadioStationDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<RadioStationDatabaseSettings>>().Value);

            services.AddHttpContextAccessor();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton<IEntityRepository<Radio>, RadioRepository>();
            //services.AddSingleton<IEntityRepository<Radio>, RedisRepository>();
            services.AddSingleton<IRedisRepository, RedisRepository>();

            services.AddSingleton<IRedisService, RedisService>();          
            services.AddSingleton<IRadioService, RadioService>();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation();
            services.AddStackExchangeRedisCache(action => 
            {
                action.Configuration = "localhost:6379";
            });

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "SampleInstance";
            });

            services.AddDbContext<AppDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();



            services.Configure<IdentityOptions>(options => {

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });


        


            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})

            //.AddJwtBearer(options =>
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = Configuration["JWT:ValidAudience"],
            //        ValidIssuer = Configuration["JWT:ValidIssuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
            //    };
            //});
           
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.Expiration = TimeSpan.FromDays(5);
            //    options.LoginPath = "/Account/Login";
            //});

            services.AddTransient<IValidator<RadioAddViewModel>, RadioAddViewModelValidation>();
            services.AddTransient<IValidator<RadioUpdateViewModel>, RadioUpdateViewModelValidation>();
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidation>();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
