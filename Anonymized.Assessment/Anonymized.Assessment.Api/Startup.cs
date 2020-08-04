using System;
using System.IO;
using AutoMapper;
using Anonymized.Assessment.Api.Extensions;
using Anonymized.Assessment.Api.Infrastructure;
using Anonymized.Assessment.Api.Middlewares;
using Anonymized.Assessment.Data;
using Anonymized.Assessment.Data.Repositories;
using Anonymized.Assessment.Data.Repositories.Interfaces;
using Anonymized.Assessment.Services;
using Anonymized.Assessment.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Anonymized.Assessment.Api
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
            services.AddMvc(o =>
                            {
                                o.Filters.Add<ApiExceptionFilter>();
                            })
                .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));

            var dataSeed = Configuration.GetSection("seed");
            services.Configure<CustomersDataSeed>(dataSeed);

            // Mapping
            var mapperConfiguration = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddMaps(typeof(AccountService));
                    cfg.AddMaps(typeof(Startup));
                });
            services.AddSingleton<IMapper>(s => new Mapper(mapperConfiguration));


            services.AddDbContext<AccountManagementContext>(
                options => options.UseInMemoryDatabase(databaseName: "AccountManagement"));

            services.AddSingleton<ICustomersDataSeed>(sp =>
                sp.GetRequiredService<IOptions<CustomersDataSeed>>().Value);

            // Repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            // Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ICustomerService, CustomerService>();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Anonymized Assessment",
                    Version = "v1",
                    Description = "Demo Account Management application"
                });

                var xmlPath = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Anonymized Assessment V1");
            });

            app.InitializeTestData(serviceProvider);

            app.UseMvc();
        }
    }
}