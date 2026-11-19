using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace LoginAutomation;

public class LoginTests
{
    public static IEnumerable<object[]> GetTestData()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "LoginTestData.csv");
        foreach (var line in File.ReadLines(path).Skip(1))   // skip header
        {
            var p = line.Split(',');
            yield return new object[] { p[0].Trim(), p[1].Trim(), p[2].Trim() };
        }
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Login_ShowsExpectedMessage(string username, string password, string expected)
    {
        using IWebDriver driver = new ChromeDriver();

        var page = new Uri(Path.Combine(AppContext.BaseDirectory, "login.html")).AbsoluteUri;
        driver.Navigate().GoToUrl(page);

        driver.FindElement(By.Id("username")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("loginButton")).Click();

        Thread.Sleep(3000);   // keep the window open for 3 seconds (for screenshots)

        var message = driver.FindElement(By.Id("message")).Text;
        Assert.Contains(expected, message);
    }
}