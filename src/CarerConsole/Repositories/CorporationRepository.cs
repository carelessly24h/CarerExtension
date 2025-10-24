namespace CarerConsole.Repositories;

internal class CorporationRepository(CarerDbContext dbContext) : BaseRepository(dbContext)
{
    public IEnumerable<Corporation> SelectAll() =>
        from corporation in Corporations
        select corporation;

    public IEnumerable<Corporation> Select(int id) =>
        from corporation in Corporations
        where corporation.Id == id
        select corporation;

    public IEnumerable<Corporation> SelectLike(string name) =>
        from corporation in Corporations
        where EF.Functions.Like(corporation.Name, $"%{name}%")
        select corporation;

    public IEnumerable<(Corporation, User?)> UserList()
    {
        var query = from corporation in Corporations

                    join user in Users
                    on corporation.Id equals user.CorporationId into joinedUsers
                    from joinedUser in joinedUsers.DefaultIfEmpty()

                    select new { corporation, joinedUser };
        foreach (var row in query)
        {
            yield return (row.corporation, row.joinedUser);
        }
    }
}
