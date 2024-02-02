namespace FSMS.System;

public class CommandHandler : ICommand
{
    private readonly Dictionary<string, string> _files = new();
    
    public void Add(string filename, string shortcut)
    {
        _files.Add(shortcut, filename);
        Console.WriteLine($"File {filename} was added");
    }

    public void Remove(string shortcut)
    {
        _files.Remove(shortcut);
        Console.WriteLine($"File {shortcut} was removed");
    }

    public void List()
    {
        foreach (var file in _files)
        {
            Console.WriteLine($"{file.Key}"); 
        }
    }
}