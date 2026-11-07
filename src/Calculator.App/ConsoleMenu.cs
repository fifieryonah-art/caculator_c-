using System.Globalization;
using Calculator.Core;

namespace Calculator.App;

public class ConsoleMenu
{
    private readonly TextReader _input;
    private readonly TextWriter _output;

    public ConsoleMenu(TextReader input, TextWriter output)
    {
        _input = input;
        _output = output;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            string? choice = _input.ReadLine();

            if (choice is null || choice.Trim() == "0")
                break;

            try
            {
                Handle(choice.Trim());
            }
            catch (DivideByZeroException ex)
            {
                _output.WriteLine($"Error: {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _output.WriteLine($"Error: {ex.Message}");
            }

            _output.WriteLine();
        }

        _output.WriteLine("Goodbye!");
    }

    private void ShowMenu()
    {
        _output.WriteLine("=== Calculator & Converter ===");
        _output.WriteLine("1. Add            2. Subtract");
        _output.WriteLine("3. Multiply       4. Divide");
        _output.WriteLine("5. km -> miles    6. miles -> km");
        _output.WriteLine("7. C -> F         8. F -> C");
        _output.WriteLine("9. GHS -> USD");
        _output.WriteLine("0. Exit");
        _output.Write("Choose an option: ");
    }

    private void Handle(string choice)
    {
        switch (choice)
        {
            case "1": RunBinary("Add", Arithmetic.Add); break;
            case "2": RunBinary("Subtract", Arithmetic.Subtract); break;
            case "3": RunBinary("Multiply", Arithmetic.Multiply); break;
            case "4": RunBinary("Divide", Arithmetic.Divide); break;
            case "5": RunUnary("Kilometres", "miles", UnitConverter.KilometresToMiles); break;
            case "6": RunUnary("Miles", "km", UnitConverter.MilesToKilometres); break;
            case "7": RunUnary("Celsius", "°F", UnitConverter.CelsiusToFahrenheit); break;
            case "8": RunUnary("Fahrenheit", "°C", UnitConverter.FahrenheitToCelsius); break;
            case "9": RunCurrency(); break;
            default: _output.WriteLine("Invalid option. Please choose 0-9."); break;
        }
    }

    private void RunBinary(string name, Func<double, double, double> operation)
    {
        double a = ReadNumber("First number: ");
        double b = ReadNumber("Second number: ");
        _output.WriteLine($"{name} result: {Format(operation(a, b))}");
    }

    private void RunUnary(string label, string unit, Func<double, double> convert)
    {
        double value = ReadNumber($"{label}: ");
        _output.WriteLine($"Result: {Format(convert(value))} {unit}");
    }

    private void RunCurrency()
    {
        double rate = ReadPositiveNumber("Rate (GHS per 1 USD): ");
        double ghs = ReadNumber("Amount in GHS: ");
        double usd = UnitConverter.GhsToUsd(ghs, rate);
        _output.WriteLine($"Result: {usd.ToString("0.00", CultureInfo.InvariantCulture)} USD");
    }

    // Keeps asking until the user types a valid number.
    private double ReadNumber(string prompt)
    {
        while (true)
        {
            _output.Write(prompt);
            string? line = _input.ReadLine();

            if (line is null)
                throw new EndOfStreamException();

            if (double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                && double.IsFinite(value))
                return value;

            _output.WriteLine("That is not a valid number. Try again.");
        }
    }

    // Same as ReadNumber, but the number must be greater than zero.
    private double ReadPositiveNumber(string prompt)
    {
        while (true)
        {
            double value = ReadNumber(prompt);

            if (value > 0)
                return value;

            _output.WriteLine("The value must be greater than zero. Try again.");
        }
    }

    private static string Format(double value) =>
        value.ToString("0.####", CultureInfo.InvariantCulture);
}