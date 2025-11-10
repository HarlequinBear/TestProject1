using Microsoft.Playwright;
using System.Text.Json;

namespace TestProject1.Tests
{
    public class PlaywrightConfig
    {
        public string Browser { get; set; } = "chromium";
        public bool Headless { get; set; } = false;
    }

    public abstract class TestsBase : PageTest
    {
        protected new IBrowser Browser { get; set; }
        protected new IBrowserContext Context { get; set; }
        protected new IPage Page { get; set; }

        [SetUp]
        public async Task Setup()
        {
            var locadConfig = LoadConfig(GetOptions());

            var launchOptions = new BrowserTypeLaunchOptions { Headless = locadConfig.Headless };

            Browser = (locadConfig.Browser?.ToLowerInvariant()) switch
            {
                "firefox" => await Playwright.Firefox.LaunchAsync(launchOptions),
                "webkit" => await Playwright.Webkit.LaunchAsync(launchOptions),
                _ => await Playwright.Chromium.LaunchAsync(launchOptions),
            };
            Context = await Browser.NewContextAsync();
            Page = await Context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            if (Context != null) await Context.CloseAsync();
            if (Browser != null) await Browser.CloseAsync();
            Playwright?.Dispose();
        }

        private JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        private PlaywrightConfig LoadConfig(JsonSerializerOptions options)
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "playwrightsettings.json");
                if (!File.Exists(path)) return new PlaywrightConfig();
                var json = File.ReadAllText(path);
                var playwrightConfig = JsonSerializer.Deserialize<PlaywrightConfig>(json, options);
                return playwrightConfig ?? new PlaywrightConfig();
            }
            catch
            {
                return new PlaywrightConfig();
            }
        }
    }
}