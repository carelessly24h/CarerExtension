namespace CarerConsole.Services;

public class CorporationService(AppSettings appSettings) : IAppService
{
    public void Run()
    {
        Console.WriteLine("CorporationService is running.");
        FetchCorporations();
        FetchUserList();
    }

    private void FetchCorporations()
    {
        using var dbContext = new CarerDbContext(appSettings);
        var repository = new CorporationRepository(dbContext);
        foreach (var corp in repository.SelectAll())
        {
            Console.WriteLine($"Corporation: {corp.Id}, {corp.Name}");
        }
    }

    private void FetchUserList()
    {
        using var dbContext = new CarerDbContext(appSettings);
        var repository = new CorporationRepository(dbContext);
        foreach (var (corp, user) in repository.UserList())
        {
            Console.WriteLine($"Corporation: {corp.Id}, {corp.Name} (User: {user?.Id}, {user?.Name}, {user?.Age})");
        }
    }
}
