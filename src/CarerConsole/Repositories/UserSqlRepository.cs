namespace CarerConsole.Repositories;

internal class UserSqlRepository(CarerDbContext dbContext) : BaseRepository(dbContext)
{
    public IEnumerable<User> SelectAll() =>
        Users.FromSqlRaw("SELECT * FROM Users");

    public IEnumerable<User> Select(int id) =>
        Users.FromSqlInterpolated($"SELECT * FROM Users WHERE id = {id}");
}
