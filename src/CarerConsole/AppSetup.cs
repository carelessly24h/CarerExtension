namespace CarerConsole;

public class AppSetup
{
    public static void Run(string[] args)
    {
        var host = Configure(args);
        RunServices(host);
    }

    private static IHost Configure(string[] args)
    {
        var builder = Host
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                var env = hostContext.HostingEnvironment;

                // Reading appsettings.json.
                configApp.AddJsonFile(@"Configs\appsettings.json", optional: false, reloadOnChange: false);
                if (env.IsDevelopment())
                {
                    configApp.AddJsonFile(@$"Configs\appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false);
                }

                // Environment variables reading.
                configApp.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Example of reading a configuration value.
                //var paths = hostContext.Configuration["PATH"];

                // Binding appsettings.json.
                var setting = new AppSettings();
                hostContext.Configuration.Bind(setting);

                // Dependency Injection setup.
                services.AddSingleton(setting);
                services.AddTransient<IAppService, CorporationService>();
                services.AddTransient<IAppService, UserService>();
            });
        return builder.Build();
    }

    private static void RunServices(IHost host)
    {
        using var serviceScope = host.Services.CreateScope();
        var appServices = serviceScope.ServiceProvider.GetServices<IAppService>();
        foreach (var appService in appServices)
        {
            appService.Run();
        }
    }
}
