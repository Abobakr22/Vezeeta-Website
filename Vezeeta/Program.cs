using Data;
using Microsoft.AspNetCore.Identity;
using Core.Models;

using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Core.Repository;
using Data.Repository;
using Core;

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


            //to create user model
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            #region Injected Services

            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IDiscountCouponRepository, DiscountCouponRepository>();

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

            //void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            //    UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            //{
            //    // Other configurations...

            //    SeedData.Initialize(userManager, roleManager).Wait();
            //}

            app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            
            } 
    }
}









//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter your jwt key"
//    });
//});


//to enable Jwt
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






//options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireLowercase = true;