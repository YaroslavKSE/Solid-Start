using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace FSMS.Starter
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Setup commands
            var rootCommand = new RootCommand("File Management System");

            var addCommand = new Command("add", "Add a file to the system")
            {
                new Argument<string>("filename", "The full path of the file"),
                new Option<string>("--shortcut", "A shortcut name for the file")
            };
            addCommand.Handler = CommandHandler.Create<string, string>((filename, shortcut) => 
                ServiceContainer.FileManagementService.AddFile(filename, shortcut));

            var removeCommand = new Command("remove", "Remove a file from the system")
            {
                new Argument<string>("shortcut", "The shortcut name of the file to remove")
            };
            removeCommand.Handler = CommandHandler.Create<string>((shortcut) => 
                ServiceContainer.FileManagementService.RemoveFile(shortcut));

            var listCommand = new Command("list", "List all files in the system")
            {
                Handler = CommandHandler.Create(() =>
                {
                    var files = ServiceContainer.FileManagementService.ListFiles();
                    var fileModels = files.ToList();
                    if (fileModels.Count == 0)
                    {
                        Console.WriteLine("No files added yet.");
                        return;
                    }
                    foreach (var file in fileModels)
                    {
                        Console.WriteLine($"{file.Shortcut} - {file.Path}");
                    }
                })
            };

            rootCommand.AddCommand(addCommand);
            rootCommand.AddCommand(removeCommand);
            rootCommand.AddCommand(listCommand);

            // Parse and invoke commands from the args
            rootCommand.InvokeAsync(args).Wait();
        }
    }
}