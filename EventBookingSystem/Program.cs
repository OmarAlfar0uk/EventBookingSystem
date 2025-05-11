using DomainLayer.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Data;
using Persistence.Repositorys;
using Service;
using ServiceAbstraction;

namespace EventBookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<EventlyDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IEventService , EventService>();
            builder.Services.AddScoped<IUserService , UserService>();
            builder.Services.AddScoped<IBookingService , BookingService>();

            //builder.Services.AddDbContext<EventlyDbContext>(options =>
            //{
            //    options.UseSqlServer();
            //});


            #endregion


            var app = builder.Build();

            #region  Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion


            app.Run();
        }
    }
}
