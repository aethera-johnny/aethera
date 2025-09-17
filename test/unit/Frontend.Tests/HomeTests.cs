using Bunit;
using Frontend.Pages;

namespace Frontend.Tests
{
    public class HomeTests : Bunit.TestContext
    {
        [Fact]
        public void Home_ShouldRenderCorrectly()
        {
            var cut = RenderComponent<Home>();

            cut.MarkupMatches(@"
                <h1>Hello, world!</h1>
                Welcome to your new app.");
        }
    }
}