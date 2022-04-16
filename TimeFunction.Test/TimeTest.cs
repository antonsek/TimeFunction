using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace TimeFunction.Test
{
    public class TimeTest
    {
        Uri _url = new Uri("http://localhost:7071/api/CreateTimeEntry");

        [Fact]
        [Obsolete]
        public async Task CheckEmptyDates()
        {
            
            var response = await DateFunction.Run(GetRequest("{}"));

            var dataMessage = await response.Content.ReadAsAsync<string>();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Dates is empty", dataMessage);
        }

        [Fact]
        [Obsolete]
        public async Task CheckNotValidDates()
        {

            var response = await DateFunction.Run(GetRequest(@"{""StartOn"":""04202022"", ""EndOn"":""04212022""}"));

            var dataMessage = await response.Content.ReadAsAsync<string>();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Dates not Valid", dataMessage);
        }

        [Obsolete]
        public HttpRequestMessage GetRequest(string content)
        {
            var configuration = new HttpConfiguration();
            var request = new System.Net.Http.HttpRequestMessage();
            request.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = configuration;
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            request.Method = HttpMethod.Post;
            request.RequestUri = _url;
            return request;
        }
    }
}