using HouseRentingSystem.Data;
using HouseRentingSystem.Infractructure.Extensions;
using HouseRentingSystem.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<HouseRentingDbContext>(opt =>
            opt.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(IHouseService));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("HouseRentingSystem", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://localhost:7239")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("HouseRentingSystem");


            app.Run();
        }
    }
}