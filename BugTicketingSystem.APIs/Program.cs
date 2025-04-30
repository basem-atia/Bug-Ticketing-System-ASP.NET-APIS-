
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace BugTicketingSystem.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Default
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();
            #endregion

            #region Extensions
            builder.Services.AddDataAccessService(builder.Configuration);
            builder.Services.AddBussinessService(builder.Configuration);
            #endregion
            #region authorization
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["jwt:Issuer"],
                       ValidAudience = builder.Configuration["jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]!))
                   };
               }
                );

            #endregion
            var app = builder.Build();

            #region Default
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
