using Data;
using Microsoft.AspNetCore.Identity;
using Core.Models;
using Core.Repository;
using Core;
using Core.Service;
using Services;
using Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Vezeeta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //builder.Services.Configure<Jwt>(Configuration.GetSection("JWT"));
            //you Need these packages to enable Jwt:
            //Install - Package Microsoft.AspNetCore.Authentication.JwtBearer
            //Install - Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
            //Install - Package Microsoft.VisualStudio.Web.CodeGeneration.Design
            //Install - Package System.IdentityModel.Tokens.Jwt

            //builder.Services.AddAuthentication(auth =>
            //{
            //    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,

            //        ValidAudience = "http://Vezeeta.net",
            //        ValidIssuer = "http://Vezeeta.net",

            //        RequireExpirationTime = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyKey")),
            //        ValidateIssuerSigningKey = true
            //    };
            //});


            //to create user model
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            #region Injected Services

            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDiscountCouponService, DiscountCouponService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            #endregion


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}