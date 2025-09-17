using Bunit;
using Xunit;
using Frontend.Pages;
using System.Net.Http; // HttpClient�� ���� �ʿ�
using Moq; // HttpMessageHandler ���Ǹ� ���� �ʿ�
using Moq.Protected; // Protected Ȯ�� �޼��带 ���� �ʿ�
using System.Net; // HttpStatusCode�� ���� �ʿ�
using System.Threading; // CancellationToken�� ���� �ʿ�
using System.Threading.Tasks; // Task�� ���� �ʿ�
using System;
using Microsoft.Extensions.DependencyInjection; // Uri�� ���� �ʿ�

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
            // HttpClient�� �����Ͽ� ���� �����͸� ��ȯ�ϵ��� �����մϴ�.
            // �̴� ���� ��Ʈ��ũ ȣ�� ���� ������Ʈ�� ������ �׽�Ʈ�ϱ� �����Դϴ�.
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                                  .Setup<Task<HttpResponseMessage>>("SendAsync",
                                      ItExpr.IsAny<HttpRequestMessage>(),
                                      ItExpr.IsAny<CancellationToken>())
                                  .ReturnsAsync(new HttpResponseMessage
                                  {
                                      StatusCode = HttpStatusCode.OK,
                                      Content = new StringContent("Hello from MockService!") // ���� ������
                                  });

            // ���ǵ� HttpMessageHandler�� ����Ͽ� HttpClient �ν��Ͻ��� �����մϴ�.
            var httpClient = new HttpClient(mockHttpMessageHandler.Object) { BaseAddress = new Uri("http://localhost") };

            // bunit�� ���� �÷��ǿ� ���� HttpClient�� ����մϴ�.
            // **�� �ٿ��� 'AddSingleton' ������ �߻��ϰ� �ֽ��ϴ�. �� ������ �ذ�Ǿ�� �մϴ�.**
            Services.AddSingleton(httpClient);

            var cut = RenderComponent<Home>();

            // ��ư�� Ŭ���Ͽ� �񵿱� �۾��� �����մϴ�.
            cut.Find("button").Click();

            // <p> �±װ� ���ǵ� ������("Hello from MockService!")�� ������ ������ ��ٸ��ϴ�.
            // cut.WaitForElement�� �񵿱� �۾� �� UI ������Ʈ�� ���������� ��ٸ��ϴ�.
            var pElement = cut.WaitForElement("p");
            // <p> �±��� ���� ��ũ���� ����� ��ġ�ϴ��� Ȯ���մϴ�.
            pElement.MarkupMatches("<p>Hello from MockService!</p>");
        }
    }
}