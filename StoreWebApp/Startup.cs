using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoreWebApp.Models;
using StoreWebApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //For CORS Origins
        //string AllowOrigins = "*";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //To Prevent The Multi Part Body Length Error
            services.Configure<FormOptions>(f =>
            {
                f.ValueLengthLimit = int.MaxValue;
                f.MultipartBodyLengthLimit = int.MaxValue;
                f.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllers();

            //services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            //});

            //string connect = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region Lazy-Loading & Reference-Loop-Handling
            //Working Using Lazy Loading
            //services.AddDbContext<StoreDbContext>(option =>
            //option.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ////To Ignore Reference Loop Handling
            //services.AddControllers().AddNewtonsoftJson(o =>
            //    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            #endregion

            //services.AddScoped<ProductRepository>(); //Dependency Injection
            services.AddScoped<IProduct, ProductRepository>(); //Dependency Inverion

            services.AddSwaggerDocument();

            services.AddCors();

            #region To Allow Origins CORS
            //To Allow Origins CORS
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(AllowOrigins,
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin();
            //            builder.AllowAnyMethod();
            //            builder.AllowAnyHeader();
            //        });
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: AllowOrigins,
            //            builder =>
            //            {
            //                builder.WithOrigins("http:/localhost:4200");
            //            });
            //});
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //To Allow Origins CORS
            //app.UseCors(AllowOrigins);

            //app.UseCors(options =>
            //    options.WithOrigins(AllowOrigins).AllowAnyMethod().AllowAnyHeader());

            app.UseCors(options =>
                options.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions { 
                FileProvider = new PhysicalFileProvider (Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
