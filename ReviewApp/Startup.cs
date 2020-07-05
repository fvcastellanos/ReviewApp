using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReviewApp.Cognitive.Client;
using ReviewApp.Data;
using ReviewApp.Services;
using ReviewApp.Storage.Client;

namespace ReviewApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<ReviewContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("REVIEW_APP_CONNECTION_STRING") ?? 
                                       "server=localhost;database=review_application;user=root;password=r00t";
                options.UseMySQL(connectionString);
            });

            services.AddSingleton<ITextAnalysisClient>(service =>
            {
                var key = Environment.GetEnvironmentVariable("AZURE_KEY_CREDENTIAL") ?? "";
                var url = Environment.GetEnvironmentVariable("AZURE_SERVICE_URL") ?? "";
                var loggerFactory = service.GetService<ILoggerFactory>();
                
                return new TextAnalysisClient(key, url, loggerFactory);
            });

            services.AddSingleton(service =>
            {
                var awsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? "";
                var awsSecret = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? "";
                var spacesBasePath = Environment.GetEnvironmentVariable("SPACES_BASE_PATH") ?? "";
                var spacesBucket = Environment.GetEnvironmentVariable("SPACES_BUCKET") ?? "";
                var spacesUrl = Environment.GetEnvironmentVariable("SPACES_URL") ?? "";                

                return new SpacesClient(awsAccessKey, awsSecret, spacesBasePath, spacesBucket, spacesUrl);
            });

            services.AddScoped<ITextAnalysisService, TextAnalysisService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReviewService, ReviewService>();
            
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/review");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
