namespace CarerConsole.Repositories;

internal class UserRepository(CarerDbContext dbContext) : BaseRepository(dbContext)
{
    public IEnumerable<(User, Corporation)> SelectAll()
    {
        var query = from user in Users

                    join corporation in Corporations
                    on user.CorporationId equals corporation.Id

                    select new { user, corporation };

        foreach (var row in query)
        {
            yield return (row.user, row.corporation);
        }
    }

    public IEnumerable<(User, Corporation)> Select(int id)
    {
        var query = from user in Users

                    join corporation in Corporations
                    on user.CorporationId equals corporation.Id

                    where user.Id == id
                    select new { user, corporation };

        foreach (var row in query)
        {
            yield return (row.user, row.corporation);
        }
    }

    public IEnumerable<(User, Corporation)> SelectLike(string name)
    {
        var query = from user in Users

                    join corporation in Corporations
                    on user.CorporationId equals corporation.Id

                    where EF.Functions.Like(user.Name, $"%{name}%")
                    select new { user, corporation };

        foreach (var row in query)
        {
            yield return (row.user, row.corporation);
        }
    }
}
