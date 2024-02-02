namespace FSMS.System;

public class InputHandler : CommandHandler {

    public void ProcessInput(string userInput)
    {
        var inputArray = userInput.Split(" ");

        var command = "";
        var filename = "";
        var shortcut = "";

        switch (inputArray.Length)
        {
            case 3:
                command = inputArray[0];
                filename = inputArray[1];
                shortcut = inputArray[2];
                break;
            case 2:
                command = inputArray[0];
                shortcut = inputArray[1];
                break;
            case 1:
                command = inputArray[0];
                break;
        }

        switch (command)
        {
            case "add":
                Add(filename, shortcut);
                break;
            case "remove":
                Remove(shortcut);
                break;
            case "list":
                List();
                break;
            default:
                Console.WriteLine($"The command {command} is not supported");
                break;
        }
    }
}