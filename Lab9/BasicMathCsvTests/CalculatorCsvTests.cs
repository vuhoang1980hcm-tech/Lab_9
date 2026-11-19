using System.Globalization;
using BasicMath;
using Xunit;

namespace BasicMathCsvTests;

public class CalculatorCsvTests
{
    private readonly BasicMaths calc = new();

    public static IEnumerable<object[]> GetTestData(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, fileName);
        foreach (var line in File.ReadLines(path).Skip(1))   // skip the header row
        {
            var p = line.Split(',');
            yield return new object[]
            {
                double.Parse(p[0], CultureInfo.InvariantCulture),
                double.Parse(p[1], CultureInfo.InvariantCulture),
                double.Parse(p[2], CultureInfo.InvariantCulture)
            };
        }
    }

    [Theory]
    [MemberData(nameof(GetTestData), "add_testdata.csv")]
    public void Add_Csv(double a, double b, double expected) =>
        Assert.Equal(expected, calc.Add(a, b));

    [Theory]
    [MemberData(nameof(GetTestData), "subtract_testdata.csv")]
    public void Subtract_Csv(double a, double b, double expected) =>
        Assert.Equal(expected, calc.Subtract(a, b));

    [Theory]
    [MemberData(nameof(GetTestData), "multiply_testdata.csv")]
    public void Multiply_Csv(double a, double b, double expected) =>
        Assert.Equal(expected, calc.Multiply(a, b));

    [Theory]
    [MemberData(nameof(GetTestData), "divide_testdata.csv")]
    public void Divide_Csv(double a, double b, double expected) =>
        Assert.Equal(expected, calc.Divide(a, b));
}