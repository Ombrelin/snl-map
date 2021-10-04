using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SnlMaps.Core.Tests
{
    public class FakeHttpClientHandler : HttpClientHandler
    {
        public string Url { get; set; }
        public string Method { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(File.ReadAllText("mock-response.json"))
            });
        }
    }
}