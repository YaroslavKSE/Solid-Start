using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using FSMS.Core.Helpers;
using FSMS.Services;

namespace FSMS.Starter
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Register services as needed
            var persistenceHelper = new PersistenceHelper();
            
            var fileManagementService = new FileManagementService(persistenceHelper);
            
            // Setup commands
            var rootCommand = new RootCommand("File Management System");
            
            var addCommand = new Command("add", "Add a file to the system")
            {
                new Argument<string>("filename", "The full path of the file"),
                new Option<string>("--shortcut", "A shortcut name for the file")
            };
            addCommand.Handler = CommandHandler.Create<string, string>((filename, shortcut) =>
                fileManagementService.AddFile(filename, shortcut));

            var removeCommand = new Command("remove", "Remove a file from the system")
            {
                new Argument<string>("shortcut", "The shortcut name of the file to remove")
            };
            removeCommand.Handler = CommandHandler.Create<string>((shortcut) =>
                fileManagementService.RemoveFile(shortcut));

            var listCommand = new Command("list", "List all files in the system")
            {
                Handler = CommandHandler.Create(() =>
                {
                    var files = fileManagementService.ListFiles();
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

            var optionsCommand =
                new Command("options", "Show a list of available actions to perform on the specified file")
                {
                    new Argument<string>("shortcut", "The shortcut name of the file"),
                };
            
            optionsCommand.Handler = CommandHandler.Create<string>((shortcut) =>
            {
                var file = fileManagementService.GetFileByShortcut(shortcut);
                if (file == null)
                {
                    Console.WriteLine("File not found.");
                    return;
                }

                Console.WriteLine($"Available actions for {shortcut}:");
                Console.WriteLine("info - View file size and location");
                if (file.Path.EndsWith(".txt"))
                {
                    Console.WriteLine("print - Print file content");
                    Console.WriteLine("summary - Show basic information about text");
                }
                if (file.Path.EndsWith(".csv") || file.Path.EndsWith(".json"))
                {
                    Console.WriteLine("print - Print file content");
                    Console.WriteLine("validate - Validate file format");
                }
            });
            
            var actionCommand = new Command("action", "Perform an action on a file")
            {
                new Argument<string>("actionName", "The action to perform"),
                new Argument<string>("shortcut", "The shortcut name of the file")
            };
            
            actionCommand.Handler = CommandHandler.Create<string, string>((actionName, shortcut) =>
            {
                var file = fileManagementService.GetFileByShortcut(shortcut);
                if (file == null)
                {
                    Console.WriteLine("File not found.");
                    return;
                }
                
                var fileAction = FileActionFactory.GetFileAction(file.Path);

                switch (actionName.ToLower())
                {
                    case "info":
                        fileAction.ViewInfo(file.Path);
                        break;
                    case "print":
                        fileAction.Print(file.Path);
                        break;
                    case "summary":
                        if (fileAction is TextFileAction action)
                        {
                            action.Summarize(file.Path);
                        }
                        else
                        {
                            Console.WriteLine("Summary action is not supported for this file type.");
                        }
                        break;
                    case "validate":
                        if (fileAction is CsvFileAction csvFileAction)
                        {
                            csvFileAction.Validate(file.Path);
                        }

                        if (fileAction is JsonFileAction jsonFileAction)
                        {
                            jsonFileAction.Validate(file.Path);
                        }
                        else
                        {
                            Console.WriteLine("Summary action is not supported for this file type.");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown action.");
                        break;
                }
            });

            rootCommand.AddCommand(addCommand);
            rootCommand.AddCommand(removeCommand);
            rootCommand.AddCommand(listCommand);
            rootCommand.AddCommand(optionsCommand);
            rootCommand.AddCommand(actionCommand);

            // Parse and invoke commands from the args
            rootCommand.InvokeAsync(args).Wait();
        }
    }
}