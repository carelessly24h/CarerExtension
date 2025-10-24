namespace CarerConsole.Repositories;

internal abstract class BaseRepository(CarerDbContext dbContext)
{
    protected DbSet<Corporation> Corporations => dbContext.Corporations;

    protected DbSet<User> Users => dbContext.Users;
}
