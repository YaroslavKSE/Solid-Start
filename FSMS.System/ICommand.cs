namespace FSMS.System;

public interface ICommand
{
    public void Add(string filename, string shortcut);
    public void Remove(string shortcut);
    public void List();
}