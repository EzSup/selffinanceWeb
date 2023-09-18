using Microsoft.EntityFrameworkCore;
using SelfFinanceAPI.Core;
using SelfFinanceAPI.Core.Repositories;
using SelfFinanceAPI.Core.Repositories.Interfaces;
using SelfFinanceAPI.Core.Services;
using SelfFinanceAPI.Core.Services.Interfaces;
using System.Text.Json.Serialization;

namespace SelfFinanceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x => 
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IExpenseTypesRepository, ExpenseTypesRepository>();
            builder.Services.AddScoped<IFinancialOperationsRepository, FinancialOperationsRepository>();
            builder.Services.AddScoped<IExpenseTypesService, ExpenseTypesService>();
            builder.Services.AddScoped<IFinancialOperationsService, FinancialOperationsService>();
            builder.Services.AddScoped<IReportsService, ReportsService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SelfFinanceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SelfFinanceDbContext>();
                context.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}