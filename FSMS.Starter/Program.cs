using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using DI.Core;
using FSMS.Core.Interfaces;
using FSMS.Services.Factories;

namespace FSMS.Starter;

internal static class Program
{
    static async Task Main(string[] args)
    {
        // Configure services 
        var diContainer = new DiContainer();
        var serviceConfigurator = new ServiceConfigurator(diContainer);

        serviceConfigurator.ConfigureServices();
        serviceConfigurator.RegisterFileActions();

        // Resolve services
        var profileManager = diContainer.Resolve<IProfileManager>();
        var fileManagementService = diContainer.Resolve<IFileManagementService>();

        var logger = diContainer.Resolve<IEventLoggingService>();
        var factory = new FileActionFactory(diContainer);

        var actionExecutor = new FileActionExecutor(factory, logger);

        // Setup commands
        var rootCommand = new RootCommand("File Management System");
        CommandConfigurator.ConfigureCommands(rootCommand, fileManagementService, profileManager, actionExecutor);

        // Setup a command line builder and parser
        var commandLineBuilder = new CommandLineBuilder(rootCommand)
            .UseDefaults()
            .Build();

        Console.WriteLine("File Management System started. Enter 'exit' to quit.");

        // Start the interactive loop
        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            // Exit condition
            if (input?.Trim().ToLower() == "exit")
            {
                break;
            }

            // Parse and invoke the command
            await commandLineBuilder.InvokeAsync(input ?? string.Empty);
        }
    }
}