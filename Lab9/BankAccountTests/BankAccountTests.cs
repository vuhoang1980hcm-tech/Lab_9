using System.Globalization;
using BankAccountApp;
using Xunit;

namespace BankAccountTests;

public class BankAccountTests
{
    // ---- CSV data-driven test (EP: valid debit + insufficient funds) ----
    public static IEnumerable<object[]> GetTestData()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "BankAccountTestData.csv");
        foreach (var line in File.ReadLines(path).Skip(1))   // skip header
        {
            var p = line.Split(',');
            yield return new object[]
            {
                p[0].Trim(),
                decimal.Parse(p[1], CultureInfo.InvariantCulture),
                decimal.Parse(p[2], CultureInfo.InvariantCulture),
                p[3].Trim()
            };
        }
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Debit_Csv(string name, decimal initial, decimal debit, string expected)
    {
        var acc = new BankAccount(name, initial);
        if (expected == "Insufficient funds")
        {
            Assert.Throws<InvalidOperationException>(() => acc.Debit(debit));
        }
        else
        {
            acc.Debit(debit);
            Assert.Equal(decimal.Parse(expected, CultureInfo.InvariantCulture), acc.Balance);
        }
    }

    // ---- Credit: EP + BVA ----
    [Theory]
    [InlineData(1000, 200, 1200)]   // EP: normal credit
    [InlineData(1000, 0, 1000)]     // BVA: zero credit
    [InlineData(0, 100, 100)]       // BVA: credit to an empty account
    public void Credit_UpdatesBalance(int initial, int amount, int expected)
    {
        var acc = new BankAccount("Test", initial);
        acc.Credit(amount);
        Assert.Equal((decimal)expected, acc.Balance);
    }

    [Fact]
    public void Credit_SmallestUnit_UpdatesBalance()   // BVA: 0.01
    {
        var acc = new BankAccount("Test", 0m);
        acc.Credit(0.01m);
        Assert.Equal(0.01m, acc.Balance);
    }

    // ---- Withdraw: EP + BVA ----
    [Theory]
    [InlineData(1000, 200, 800)]    // EP: normal withdraw
    [InlineData(500, 500, 0)]       // BVA: exactly the balance
    [InlineData(500, 0, 500)]       // BVA: zero withdrawal
    public void Withdraw_UpdatesBalance(int initial, int amount, int expected)
    {
        var acc = new BankAccount("Test", initial);
        acc.Withdraw(amount);
        Assert.Equal((decimal)expected, acc.Balance);
    }

    [Fact]
    public void Withdraw_JustOverBalance_Throws()      // BVA: balance + 0.01
    {
        var acc = new BankAccount("Test", 500m);
        Assert.Throws<InvalidOperationException>(() => acc.Withdraw(500.01m));
    }
}