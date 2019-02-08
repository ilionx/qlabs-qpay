using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Portal.Business;
using Portal.DataAccess;
using Portal.Interfaces;
using RequestLocalizationOptions = Microsoft.AspNetCore.Builder.RequestLocalizationOptions;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace QPayPortal
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
            //services.AddAuthentication(sharedOptions =>
            //{
            //    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //})
            //.AddAzureAd(options => Configuration.Bind("AzureAd", options))
            //.AddCookie();

            services.AddMvc();
            services.AddDbContext<PortalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("QPayDatabase"), b => b.MigrationsAssembly("Portal.DataAccess")));

            services.AddGetUserInfo();

            //Business
            services.AddTransient<RequestUserData>();
            services.AddTransient<RegisterNewUser>();
            services.AddTransient<RequestUserTransactions>();
            services.AddTransient<RequestNewCards>();
            services.AddTransient<RequestAllUsers>();
            services.AddTransient<RequestAllTransactions>();
            services.AddTransient<RequestTerminalWithProduct>();
            services.AddTransient<RequestUserWithUserId>();
            services.AddTransient<EditEmployeeWithId>();
            services.AddTransient<RequestAllProducts>();
            services.AddTransient<RequestProductWithProductId>();
            services.AddTransient<EditProductWithId>();
            services.AddTransient<RegisterNewProduct>();
            services.AddTransient<RegisterNewTerminal>();
            services.AddTransient<RemoveTerminal>();
            services.AddTransient<RemoveProduct>();
            services.AddTransient<RegisterOfflineTopUp>();
            services.AddTransient<RequestOpenTopUps>();
            services.AddTransient<RegisterProductToTerminal>();
            services.AddTransient<RequestAllTransactionsWithType>();
            services.AddTransient<EditTransactionTypeWithId>();
            services.AddTransient<ExternalPayment>();

            //Interfaces
            services.AddTransient<IRequestUserInfoFromDataprovider, RequestUserInfoFromDataprovider>();
            services.AddTransient<ICheckIfCardIsAttachedToUser, CheckCardId>();
            services.AddTransient<ISaveCardToUser, SaveCardToUser>();
            services.AddTransient<IOfflineTopUp, SaveOfflineTopUp>();
            services.AddTransient<INewExternalPayment, Portal.DataAccess.PayPal>();

            //DataAccess
            services.AddTransient<GetLoggedInUserInfo>();
            services.AddTransient<GetUserWithCardId>();
            services.AddTransient<SaveCardToUser>();
            services.AddTransient<GetUsersTransactions>();
            services.AddTransient<GetNewCards>();
            services.AddTransient<GetAllUsers>();
            services.AddTransient<GetAllTransactions>();
            services.AddTransient<GetTerminalAndProduct>();
            services.AddTransient<GetUserWithUserId>();
            services.AddTransient<SaveEmployeeEditWithCardId>();
            services.AddTransient<GetAllProducts>();
            services.AddTransient<GetProductWithProductId>();
            services.AddTransient<SaveProductEditWithProductId>();
            services.AddTransient<SaveNewProduct>();
            services.AddTransient<SaveNewTerminal>();
            services.AddTransient<DeleteTerminalWithId>();
            services.AddTransient<DeleteProductWithId>();
            services.AddTransient<SaveOfflineTopUp>();
            services.AddTransient<GetOpenTopUps>();
            services.AddTransient<EditTerminalProduct>();
            services.AddTransient<GetAllTransactionsWithType>();
            services.AddTransient<EditTransactionType>();
            services.AddTransient<Portal.DataAccess.PayPal>();

            //Logging
            services.AddTransient<ILoggerFactory, LoggerFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var locale = Configuration["SiteLocale"];
            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo> { new CultureInfo(locale) },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo(locale) },
                DefaultRequestCulture = new RequestCulture(locale)
            };
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRequestLocalization(localizationOptions);

            app.UseStaticFiles();

            //app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    public static class Extension
    {
        public static void AddGetUserInfo(this IServiceCollection services)
        {
            services.AddTransient<IRequestUserInfoFromDataprovider, RequestUserInfoFromDataprovider>();
        }
    }
}
