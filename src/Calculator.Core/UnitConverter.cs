namespace Calculator.Core;

public static class UnitConverter
{
    private const double KilometresPerMile = 1.609344;

    public static double KilometresToMiles(double kilometres) => kilometres / KilometresPerMile;

    public static double MilesToKilometres(double miles) => miles * KilometresPerMile;

    public static double CelsiusToFahrenheit(double celsius) => celsius * 9 / 5 + 32;

    public static double FahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32) * 5 / 9;

    /// <param name="ghsPerUsd">How many cedis equal 1 US dollar, for example 15.5.</param>
    public static double GhsToUsd(double ghs, double ghsPerUsd)
    {
        if (ghsPerUsd <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(ghsPerUsd), "Rate must be greater than zero.");

        return ghs / ghsPerUsd;
    }
}