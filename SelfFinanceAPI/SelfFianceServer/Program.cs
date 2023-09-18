using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using SelfFianceServer.Services.Interfaces;
using SelfFinanceAPI.Core.Services;
using SelfFianceServer.Services;
using MudBlazor;
using MudBlazor.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace SelfFianceServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string apiUrl = configuration.GetSection("ConnectionStrings:ApiConnection").Value;

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient("v1",
                httpClient =>
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                });

            builder.Services.AddScoped<IDialogService, DialogService>();
            builder.Services.AddScoped<ISnackbar, SnackbarService>();
            builder.Services.AddScoped<ICommonService, CommonService>(
                provider => new CommonService(
                    provider.GetRequiredService<IDialogService>()));
            builder.Services.AddSingleton<IApiAppealService>(provider => new ApiAppealService(
                provider.GetRequiredService<IHttpClientFactory>()));
            builder.Services.AddScoped<IExpenseTypeService>(provider => new ExpenseTypeService(
                provider.GetRequiredService<IApiAppealService>(),
                provider.GetRequiredService<ICommonService>(),
                provider.GetRequiredService<ISnackbar>()));
            builder.Services.AddScoped<IFinancialOperationService>(provider => new FinancialOperationService(
                provider.GetRequiredService<IApiAppealService>(),
                provider.GetRequiredService<ICommonService>(),
                provider.GetRequiredService<ISnackbar>()));
            builder.Services.AddScoped<IReportService>(provider => new ReportService(
                provider.GetRequiredService<IApiAppealService>(),
                provider.GetRequiredService<ISnackbar>()));

            builder.Services.AddMudServices();

            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}