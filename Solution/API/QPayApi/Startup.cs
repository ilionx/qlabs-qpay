using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentTerminal.Business;
using PaymentTerminal.DataAccess;
using PaymentTerminal.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace QPayApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "QPay Terminal API test",
                    Version = "v1",
                    Description = "Simple api test for Qpay Terminal API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Qlabs - ilionx",
                        Email = "qlabs@ilionx.com",
                        Url = "https://github.com/ilionx"
                    },
                    License = new License
                    {
                        Name = "License??",
                        Url = "link to license terms"
                    },
                });
            });

            services.AddMvc();

            services.AddDbContext<PaymentTerminalContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("QPayDatabase")));

            services.AddCheckBalance();

            services.AddTransient<ICheckBalance, CheckBalance>();
            services.AddTransient<ICheckTerminal, CheckTerminal>();
            services.AddTransient<IProcessPayment, ProcessPayment>();
            services.AddTransient<INewCardScanned, NewCardScanned>();
            services.AddTransient<IWriteNewCard, WriteNewCard>();
            services.AddTransient<IWriteTransaction, WriteTransaction>();
            services.AddTransient<ValidateScan>();
            services.AddTransient<GetBalance>();
            services.AddTransient<GetProduct>();
            services.AddTransient<WriteTransaction>();

            services.AddTransient<ILoggerFactory, LoggerFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scan card");
                    c.RoutePrefix = string.Empty;
                });

                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }


    public static class Extension
    {
        public static void AddCheckBalance(this IServiceCollection services)
        {
            services.AddTransient<ICheckBalance, CheckBalance>();
            services.AddTransient<ICheckTerminal, CheckTerminal>();
            services.AddTransient<IProcessPayment, ProcessPayment>();
            services.AddTransient<INewCardScanned, NewCardScanned>();
        }
    }
}