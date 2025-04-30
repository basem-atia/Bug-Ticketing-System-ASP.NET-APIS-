using Microsoft.EntityFrameworkCore;
namespace BugTicketingSystem.DAL;
public class BugTicketingContext : DbContext
{
    public DbSet<Bug> Bugs => Set<Bug>();
    public DbSet<FileAttachment> FileAttachments => Set<FileAttachment>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();

    public BugTicketingContext(DbContextOptions<BugTicketingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BugTicketingContext).Assembly);
        var projects = new List<Project>()
        {
            new Project{Id=Guid.Parse("1c017e23-6987-4d3b-911f-d3cf432a1c19"),Name="Examination System",Description="Examination System For Students To Evaluate their Level of Understanding Each Subject They Studied"},
            new Project{Id=Guid.Parse("80fa2674-074a-4c48-8b6c-b63eb7ba60f1"),Name="E-Commerce Website",Description="E-Commerce Website to Practise On Angular And NodeJs"},
            new Project{Id=Guid.Parse("b8e4ce6b-3606-4535-882a-3ceaf55b6808"),Name="E-Commerce Website2",Description="E-Commerce Website to Practise On Angular And Asp.Net Using Apis"},
        };
        var bugs = new List<Bug>()
        {
            new Bug
            {
                Id= Guid.Parse("328ebc19-b6f1-4193-ae06-bcbc19105ba4"),
                Title="Error In Using Seeding",
                Description="When Using Seeding, Error You Must Use Async Seeding Also",
                Status="Solved",
                ProjectId=projects[0].Id
            },
            new Bug
            {
                Id= Guid.Parse("a546459d-089c-41b4-9a44-2629babaec06"),
                Title="Error In login",
                Description="Error In Signing In Using Auth",
                Status="Open",
                ProjectId=projects[1].Id
            },
            new Bug
            {
                Id=Guid.Parse("ce66d99b-a171-445d-b2a7-30fc399412ff"),
                Title="Error In Payment",
                Description="When Using Paymob, An Error Occurred",
                Status="Solved",
                ProjectId=projects[2].Id
            }
        };
        var attachments = new List<FileAttachment>()
        {
            new FileAttachment{Id=Guid.Parse("2cdbc40a-b617-4858-98bd-71edd758ee68"),FileName="C# Code",FilePath="/uploads/file.sln",BugId=bugs[0].Id},
            new FileAttachment{Id=Guid.Parse("a289b995-a053-4318-8555-a5a6d9e5f75d"),FileName="Error ScreenShot",FilePath="/uploads/error_screenshot.png",BugId=bugs[1].Id},
            new FileAttachment{Id=Guid.Parse("e3ba3e0f-d2f5-406a-8fd8-70e9eca99e50"),FileName="Error ScreenShot of Paymob",FilePath="/uploads/error_screenshot2.png",BugId=bugs[2].Id},

        };
        var users = new List<User>()
        {
            new User
            {
                Id=Guid.Parse("59915d5a-e035-4da7-aa76-b767ae1844d6"),
                FullName="Basem Attia Elsayed",
                EmailAddress="basem@gmail.com",
                Password="Hasehed_Password_2"
            },
            new User
            {
                Id=Guid.Parse("bb31e5e5-85a9-4d06-9fac-6b3cb7cd495d"),
                FullName="Karim Mohamed Helmy",
                EmailAddress="karim@gmail.com",
                Password="Hasehed_Password_234"
            },
            new User
            {
                Id=Guid.Parse("e63d04ce-c434-428d-866a-aff44a00916c"),
                FullName="Mohamed ElSayed Tabei",
                EmailAddress="mohamed@gmail.com",
                Password="Hasehed_Password_235"
            }
        };
        var roles = new List<Role>()
        {
            new Role{Id=Guid.Parse("a94a3ec6-77da-47b2-8428-bcbf372a3676"),RoleName=RoleEnum.Tester.ToString()},
            new Role{Id=Guid.Parse("dc435455-5ae0-43ab-919b-3029b64fb488"),RoleName=RoleEnum.Developer.ToString()},
            new Role{Id=Guid.Parse("f8b4285f-300e-4cd7-930c-f6f290faea0f"),RoleName=RoleEnum.Admin.ToString()},
        };

        modelBuilder.Entity<Bug>().HasData(bugs);
        modelBuilder.Entity<Project>().HasData(projects);
        modelBuilder.Entity<FileAttachment>().HasData(attachments);
        modelBuilder.Entity<Role>().HasData(roles);
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity("RoleUser").HasData(
            new { RolesId = roles[0].Id, UsersId = users[0].Id },
            new { RolesId = roles[1].Id, UsersId = users[0].Id },
            new { RolesId = roles[2].Id, UsersId = users[1].Id },
            new { RolesId = roles[2].Id, UsersId = users[2].Id },
            new { RolesId = roles[1].Id, UsersId = users[2].Id }
            );
        modelBuilder.Entity("BugUser").HasData(
            new { BugsId = bugs[0].Id, UsersId = users[0].Id },
            new { BugsId = bugs[2].Id, UsersId = users[0].Id },
            new { BugsId = bugs[0].Id, UsersId = users[1].Id },
            new { BugsId = bugs[1].Id, UsersId = users[1].Id },
            new { BugsId = bugs[2].Id, UsersId = users[2].Id }
            );
    }

}
