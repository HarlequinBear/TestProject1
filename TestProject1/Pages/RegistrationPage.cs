using Microsoft.Playwright;

namespace TestProject1.Pages
{
    internal class RegistrationPage(IPage page)
    {
        public ILocator RegisterLink => page.Locator("#registerLink");

        public ILocator EmailField => page.Locator("#Email");
        
        public ILocator PasswordField => page.Locator("#Password");
        
        public ILocator ConfirmPasswordField => page.Locator("#ConfirmPassword");

        public ILocator RegisterButton => page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Register" });
    }
}