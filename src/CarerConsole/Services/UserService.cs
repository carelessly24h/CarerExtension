using NPOI.SS.Formula.Functions;

namespace CarerConsole.Services;

public class UserService(AppSettings appSettings) : IAppService
{
    public void Run()
    {
        Console.WriteLine("UserService is running.");
        FetchUsers();
        FetchUsersBySql();
    }

    private void FetchUsers()
    {
        using var dbContext = new CarerDbContext(appSettings);
        var repository = new UserRepository(dbContext);
        foreach (var (user, corp) in repository.SelectAll())
        {
            Console.WriteLine($"User: {user.Id}, {user.Name}, {user.Age} (Corp: {corp.Id}, {corp.Name})");
        }
    }

    private void FetchUsersBySql()
    {
        using var dbContext = new CarerDbContext(appSettings);
        var repository = new UserSqlRepository(dbContext);
        foreach (var user in repository.SelectAll())
        {
            Console.WriteLine($"User: {user.Id}, {user.Name}, {user.Age}");
        }
    }
}
