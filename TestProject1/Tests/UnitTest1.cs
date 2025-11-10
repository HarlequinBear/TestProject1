using TestProject1.Pages;

namespace TestProject1.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : TestsBase
    {
        private HomePage homePage;

        [SetUp]
        public void SetupHomePage()
        {
            homePage = new HomePage(Page);
        }

        [Test]
        public async Task Register()
        {
            await homePage.GotoAsync();

            await Expect(Page).ToHaveTitleAsync("ENSEK Energy Corp. - Candidate Test");

            var registrationPage = await homePage.ClickRegisterLink();

            await registrationPage.EmailField.FillAsync("Blah@example.com");
            await registrationPage.PasswordField.FillAsync("Blah1234!");
            await registrationPage.ConfirmPasswordField.FillAsync("Blah1234!");
            await registrationPage.RegisterButton.ClickAsync();

            // The test times out because the DB server is unreachable
        }
    }
}