using BugTicketingSystem.BL.Managers.Bugs;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.BL
{
    public static class BussinessExtensions
    {
        public static void AddBussinessService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IBugManager, BugManager>();
            services.AddValidatorsFromAssembly(typeof(BussinessExtensions).Assembly);
        }
    }
}
