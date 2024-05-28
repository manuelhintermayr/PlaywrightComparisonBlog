using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaywrightTests
{
    [TestClass]
    public class TestExample : PageTest
    {
        [TestMethod]
        public async Task ShouldClickButton()
        {
            await Page.GotoAsync("data:text/html,<script>var result;</script><button onclick='result=\"Clicked\"'>Go</button>");
            await Page.GetByRole(AriaRole.Button).ClickAsync();
            Assert.AreEqual("Clicked", Page.EvaluateAsync<string>("result").Result);
        }

        [TestMethod]
        public async Task ShouldCheckTheBox()
        {
            await Page.SetContentAsync("<input id='checkbox' type='checkbox'></input>");
            await Page.GetByRole(AriaRole.Checkbox).ClickAsync();
            Assert.IsTrue(Page.EvaluateAsync<bool>("() => window['checkbox'].checked").Result);
        }

        [TestMethod]
        public async Task ShouldSearchWiki()
        {
            await Page.GotoAsync("https://www.wikipedia.org/");
            await Page.SelectOptionAsync("#searchLanguage", "English");
            await Page.Locator("input[name=\"search\"]").ClickAsync();
            await Page.Locator("input[name=\"search\"]").FillAsync("Playwright (software)");
            await Page.Locator("input[name=\"search\"]").PressAsync("Enter");
            Assert.AreEqual("https://en.wikipedia.org/wiki/Playwright_(software)", Page.Url);
        }
    }
}