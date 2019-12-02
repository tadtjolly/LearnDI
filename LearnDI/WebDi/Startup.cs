using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using LearnDI;
using LearnDI.Connection;
using LearnDI.Interface;
using LearnDI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace WebDi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddSingleton<IDiService, DiService>();
            services.AddDbContext<DiDbContext>(option => option.UseSqlServer("Data Source=CPP00134171D\\ANHVT22;Initial Catalog=DiDB;Integrated Security=True"), ServiceLifetime.Singleton);
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "",
                    Name = "Authorization",
                    Type = "apiKey",
                    In = "header"
                });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                {"Bearer", new string[] { }},
                };
                c.SwaggerDoc("v1", new Info { Title = "Utop User", Version = "v1" });
                //c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityRequirement(security);
            });
            return services.Bootstrap();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Utop");
                c.RoutePrefix = string.Empty;
            });
            app.ConfigApp();
            app.UseHttpsRedirection();
            var t = typeof(LearnDIModule);
            app.UseMvc();
        }
    }
}
