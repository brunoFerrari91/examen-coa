using COA.Api.Resources;
using COA.Data;
using COA.Data.Models;
using COA.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace COA.Api
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
            #region Error Handling Data validation
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    // This code is executed when invalid ModelState occurs
                    ErrorDetails error = new ErrorDetails();
                    error.StatusCode = 400; // Set BadRequest error
                    // Map ModelState dictionary to Error dictionary
                    var dictionary = actionContext.ModelState;
                    error.ErrorList = new Dictionary<string, List<string>>();
                    // Add every invalid ModelState to the Errors List
                    foreach (var validationError in dictionary)
                    {
                        List<string> errorList = new List<string>();
                        foreach (var innerError in validationError.Value.Errors)
                        {
                            errorList.Add(innerError.ErrorMessage);
                        }
                        error.ErrorList.Add(validationError.Key != "" ? validationError.Key : "error", errorList);
                    }
                    return new BadRequestObjectResult(error);
                };
            });
            #endregion
            services.AddDbContext<ExamenDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ExamenDB")));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "COA Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coa Api V1");
            });
        }
    }
}
