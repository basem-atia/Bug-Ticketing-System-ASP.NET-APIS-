using BugTicketingSystem.DAL.Repos.BugRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.DAL;

public static class DataAccessExtensions
{
    public static void AddDataAccessService(this IServiceCollection services, IConfiguration configuration)
    {
        string? ConnectionString = configuration.GetConnectionString("ConnectionString");
        if (ConnectionString != null)
        {
            services.AddDbContext<BugTicketingContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IFileAttachmentRepository, FileAttachmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWorkHandler>();
        }
    }
}
