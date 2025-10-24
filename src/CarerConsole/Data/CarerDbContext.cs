namespace CarerConsole.Data;

public class CarerDbContext(string connectionString) : DbContext
{
    public CarerDbContext(AppSettings appSettings) : this(appSettings.ConnectionString)
    {
    }

    public DbSet<Corporation> Corporations { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString);
    }

    public void ExecuteTransaction(Action action) =>
        ExecuteTransaction(_ => action());

    public void ExecuteTransaction(Action<IDbContextTransaction> action)
    {
        using var transaction = Database.BeginTransaction();
        try
        {
            action(transaction);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
