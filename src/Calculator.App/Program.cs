using Calculator.App;

try
{
    new ConsoleMenu(Console.In, Console.Out).Run();
}
catch (EndOfStreamException)
{
    // Input ended (for example Ctrl+D), so exit quietly.
}
