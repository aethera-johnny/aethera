using Bunit;
using Xunit;
using Frontend.Pages;
using System.Net.Http; // HttpClient를 위해 필요
using Moq; // HttpMessageHandler 모의를 위해 필요
using Moq.Protected; // Protected 확장 메서드를 위해 필요
using System.Net; // HttpStatusCode를 위해 필요
using System.Threading; // CancellationToken을 위해 필요
using System.Threading.Tasks; // Task를 위해 필요
using System;
using Microsoft.Extensions.DependencyInjection; // Uri를 위해 필요

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
                Welcome to your new app.

                <button class=""btn btn-primary"" >Get Hello String</button>

                <p></p>");
        }

        [Fact]
        public void GetHelloString_ShouldDisplayHelloMessage_UnitTest()
        {
            // HttpClient를 모의하여 가상 데이터를 반환하도록 설정합니다.
            // 이는 실제 네트워크 호출 없이 컴포넌트의 로직을 테스트하기 위함입니다.
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                                  .Setup<Task<HttpResponseMessage>>("SendAsync",
                                      ItExpr.IsAny<HttpRequestMessage>(),
                                      ItExpr.IsAny<CancellationToken>())
                                  .ReturnsAsync(new HttpResponseMessage
                                  {
                                      StatusCode = HttpStatusCode.OK,
                                      Content = new StringContent("Hello from MockService!") // 가상 데이터
                                  });

            // 모의된 HttpMessageHandler를 사용하여 HttpClient 인스턴스를 생성합니다.
            var httpClient = new HttpClient(mockHttpMessageHandler.Object) { BaseAddress = new Uri("http://localhost") };

            // bunit의 서비스 컬렉션에 모의 HttpClient를 등록합니다.
            // **이 줄에서 'AddSingleton' 오류가 발생하고 있습니다. 이 문제가 해결되어야 합니다.**
            Services.AddSingleton(httpClient);

            var cut = RenderComponent<Home>();

            // 버튼을 클릭하여 비동기 작업을 시작합니다.
            cut.Find("button").Click();

            // <p> 태그가 모의된 데이터("Hello from MockService!")를 포함할 때까지 기다립니다.
            // cut.WaitForElement는 비동기 작업 후 UI 업데이트를 안정적으로 기다립니다.
            var pElement = cut.WaitForElement("p");
            // <p> 태그의 최종 마크업이 예상과 일치하는지 확인합니다.
            pElement.MarkupMatches("<p>Hello from MockService!</p>");
        }
    }
}