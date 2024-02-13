﻿using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using FSMS.Core.Helpers;
using FSMS.Core.Interfaces;
using FSMS.Services;

namespace FSMS.Starter;

public class CommandConfigurator
{
    public static void ConfigureCommands(RootCommand rootCommand, IFileManagementService fileManagementService,
        IProfileManager profileManager)
    {
        var addCommand = new Command("add", "Add a file to the system")
        {
            new Argument<string>("filename", "The full path of the file"),
            new Option<string>("--shortcut", "A shortcut name for the file")
        };
        addCommand.Handler = CommandHandler.Create<string, string>((filename, shortcut) =>
        {
            if (!CommandHelper.EnsureLoggedIn(profileManager)) return;
            fileManagementService.AddFile(filename, shortcut);
        });


        var removeCommand = new Command("remove", "Remove a file from the system")
        {
            new Argument<string>("shortcut", "The shortcut name of the file to remove")
        };


        removeCommand.Handler = CommandHandler.Create<string>( shortcut =>
        {
            if (!CommandHelper.EnsureLoggedIn(profileManager)) return;
            fileManagementService.RemoveFile(shortcut);
        });

        var listCommand = new Command("list", "List all files in the system")
        {
            Handler = CommandHandler.Create(() =>
            {
                if (!CommandHelper.EnsureLoggedIn(profileManager)) return;
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

        var loginCommand = new Command("login", "Login or switch to a profile")
        {
            new Argument<string>("profileName", "Name of the profile")
        };
        loginCommand.Handler = CommandHandler.Create<string>(profileManager.LoginOrCreateProfile);

        var optionsCommand =
            new Command("options", "Show a list of available actions to perform on the specified file")
            {
                new Argument<string>("shortcut", "The shortcut name of the file"),
            };

        optionsCommand.Handler = CommandHandler.Create<string>((shortcut) =>
        {
            if (!CommandHelper.EnsureLoggedIn(profileManager)) return;

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
        var changePlanCommand = new Command("change_plan", "Change the user's current plan")
        {
            new Argument<string>("newPlanName", "The name of the new plan to switch to")
        };
        changePlanCommand.Handler = CommandHandler.Create<string>(profileManager.ChangeUserPlan);

        rootCommand.AddCommand(changePlanCommand);

        
        actionCommand.Handler = CommandHandler.Create<string, string>((actionName, shortcut) =>
        {
            if (!CommandHelper.EnsureLoggedIn(profileManager)) return;

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
                    switch (fileAction)
                    {
                        case CsvFileAction csvFileAction:
                            csvFileAction.Validate(file.Path);
                            break;
                        case JsonFileAction jsonFileAction:
                            jsonFileAction.Validate(file.Path);
                            break;
                        default:
                            Console.WriteLine("Validate action is not supported for this file type.");
                            break;
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
        rootCommand.AddCommand(loginCommand);
    }
}