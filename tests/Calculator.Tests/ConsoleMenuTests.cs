using Calculator.App;

namespace Calculator.Tests;

public class ConsoleMenuTests
{
    // Feeds fake keyboard input to the menu and returns everything it printed.
    private static string RunMenu(string input)
    {
        using var reader = new StringReader(input);
        using var writer = new StringWriter();

        new ConsoleMenu(reader, writer).Run();

        return writer.ToString();
    }

    [Fact]
    public void Add_ShowsCorrectResult()
    {
        string output = RunMenu("1\n2\n3\n0\n");

        Assert.Contains("Add result: 5", output);
    }

    [Fact]
    public void InvalidNumber_ShowsErrorAndAsksAgain()
    {
        string output = RunMenu("1\nabc\n2\n3\n0\n");

        Assert.Contains("not a valid number", output);
        Assert.Contains("Add result: 5", output);
    }

    [Fact]
    public void DivideByZero_ShowsErrorAndReturnsToMenu()
    {
        string output = RunMenu("4\n5\n0\n0\n");

        Assert.Contains("Cannot divide by zero", output);
        Assert.Contains("Goodbye!", output);
    }

    [Fact]
    public void InvalidMenuOption_ShowsMessage()
    {
        string output = RunMenu("hello\n0\n");

        Assert.Contains("Invalid option", output);
    }

    [Fact]
    public void Currency_RejectsZeroRate_ThenConverts()
    {
        string output = RunMenu("9\n0\n15.5\n155\n0\n");

        Assert.Contains("must be greater than zero", output);
        Assert.Contains("10.00 USD", output);
    }

    [Fact]
    public void TemperatureConversion_Works()
    {
        string output = RunMenu("7\n100\n0\n");

        Assert.Contains("212", output);
    }

    [Fact]
    public void InputEndsSuddenly_DoesNotCrashAtMenu()
    {
        string output = RunMenu("");

        Assert.Contains("Goodbye!", output);
    }
}