using CookingQuest.Web.Controllers;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CookingQuest.Tests
{
    public class Tests
    {
        [Fact]
        public void Store()
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("[{'id':1,'value':'1'}]"),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            var subjectUnderTest = new StoreController(httpClient, new Web.MyConfiguration() { ServiceUrl = "http://test.com/"});

            var result = subjectUnderTest.Index();
            var result2 = subjectUnderTest.Add(new Web.API_Models.EquipmentModel() { Difficulty = 2, EquipmentId = 1, Modifier = 1, Name = "a", PlayerEquipmentId = 1, Price = 1, Type = "Dunegon" });


            Assert.NotNull(result);
            Assert.NotNull(result2);
        }

    }
}
