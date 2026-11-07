# Console Calculator & Unit Converter

A C# console application that performs basic arithmetic and unit conversions.
It validates all input, so incorrect input does not crash the program.

## Features

- Add, subtract, multiply, and divide
- Kilometres <-> miles
- Celsius <-> Fahrenheit
- GHS -> USD using an exchange rate entered by the user
- Invalid input is rejected with a message, and the user is asked again

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) 
 10.0.114
- Check the installed version with:

  ```bash
  dotnet --version
  ```

## How to run

```bash
git clone https://github.com/fifieryonah-art/calculator_c-.git
cd fifieryonah-art
dotnet run --project src/Calculator.App
```

## How to test

```bash
dotnet test
```

## Example session

```text
=== Calculator & Converter ===
1. Add            2. Subtract
3. Multiply       4. Divide
5. km -> miles    6. miles -> km
7. C -> F         8. F -> C
9. GHS -> USD
0. Exit
Choose an option: 1
First number: abc
That is not a valid number. Try again.
First number: 2
Second number: 3
Add result: 5
```

## Project structure

```text
src/
  Calculator.Core/   Pure logic (Arithmetic, UnitConverter). No console code.
  Calculator.App/    Console menu and input handling.
tests/
  Calculator.Tests/  xUnit tests for Core and for the menu.
```

## Design decisions

- **Logic separated from the UI.** Core has no `Console` calls, so it can be
  tested directly and reused by any other interface.
- **Menu takes `TextReader`/`TextWriter`.** Tests can feed fake input and read
  the output, so even the user-interaction code is tested automatically.
- **`TryParse` with a retry loop, not exceptions, for bad input.** Typing
  `abc` is normal user behaviour, not an exceptional event.
- **`InvariantCulture`.** The decimal separator is always `.`, so the program
  behaves the same on every computer.
- **Core throws, the UI catches.** `Divide` throws `DivideByZeroException` and
  `GhsToUsd` throws `ArgumentOutOfRangeException` for a bad rate. The menu
  catches them in one place and shows a friendly message.

## Assumptions

- The exchange rate means **how many GHS equal 1 USD** (for example 15.5).
  The result is `GHS / rate`.
- Numbers use `.` as the decimal separator (`3.5`, not `3,5`).
- Results are rounded for display only (up to 4 decimal places, currency to 2).
- 1 mile = 1.609344 km (the international definition).

## Known limitations

- Only two numbers per calculation (no expressions like `2 + 3 * 4`).
- Uses `double`, so very small floating-point rounding errors are possible.
  `decimal` would be better for money.
- Only GHS -> USD is supported, not USD -> GHS.
- The rate is typed in every time and is not saved.
- Typing `Infinity` or `NaN` as a number is rejected.
