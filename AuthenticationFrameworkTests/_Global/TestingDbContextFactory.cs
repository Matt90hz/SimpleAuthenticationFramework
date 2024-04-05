using Microsoft.EntityFrameworkCore;

namespace AuthenticationFrameworkTests._Global;

internal sealed class TestingDbContextFactory(DbContextOptions? options = null) : IDbContextFactory<TestingDbContext>
{
    public TestingDbContext CreateDbContext()
    {
        options ??= new DbContextOptionsBuilder()
            .UseSqlite("Datasource=ReadonlyTestsDatabase")
            .Options;

        TestingDbContext context = new(options);

        context.Database.EnsureCreated();

        return context;
    }
}

