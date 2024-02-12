using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using FSMS.Core.Interfaces;

namespace FSMS.Starter;

internal static class Program
{
    static async Task Main(string[] args)
    {
        // Configure services using the new ServiceConfiguration class
        ServiceConfigurator.ConfigureServices();

        // Resolve services
        var fileManagementService = ServiceConfigurator.Resolve<IFileManagementService>();
        var profileManager = ServiceConfigurator.Resolve<IProfileManager>();

        // Setup commands
        var rootCommand = new RootCommand("File Management System");
        CommandConfigurator.ConfigureCommands(rootCommand, fileManagementService, profileManager);

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