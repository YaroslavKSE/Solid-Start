using FSMS.System;

var inputHandler = new InputHandler();

while (true)
{
    var input = Console.ReadLine();
    if (input != null) inputHandler.ProcessInput(input);
}