using Microsoft.Playwright;

namespace TestProject1.Pages
{
    internal class HomePage(IPage page)
    {
        public ILocator RegisterLink => page.Locator("#registerLink");

        public async Task GotoAsync()
        {
            await page.GotoAsync("https://ensekautomationcandidatetest.azurewebsites.net/");
        }

        internal async Task<RegistrationPage> ClickRegisterLink()
        {
            await RegisterLink.ClickAsync();
            return new RegistrationPage(page);
        }
    }
}