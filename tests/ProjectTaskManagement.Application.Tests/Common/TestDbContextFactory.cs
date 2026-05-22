using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Infrastructure.Persistence;

namespace ProjectTaskManagement.Application.Tests.Common;

internal static class TestDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"TaskPilotTests-{Guid.NewGuid()}")
            .EnableSensitiveDataLogging()
            .Options;

        var dbContext = new ApplicationDbContext(options);
        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}
