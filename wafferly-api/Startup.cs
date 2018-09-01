//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaveBudgetApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace SaveBudgetApi
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
            
            //services.AddDbContext<BSaverContext>(options => options.UseInMemoryDatabase("EzayNwafar"));
            services.AddDbContext<BSaverContext>(options => options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));

            services.
                AddMvc()
                .AddJsonOptions(option => 
                                option.SerializerSettings
                                .ReferenceLoopHandling = 
                                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //The perceding code is used to show the error message during the development mode
            //it should be removed to not show the error to the normal users

            app.UseMvc();
        }
    }
}
