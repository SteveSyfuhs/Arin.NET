using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Arin.NET.Entities;

namespace Arin.NET.Client
{
    public class ArinClient
    {
        private static readonly Uri DefaultIpEndpoint = new Uri("https://rdap.arin.net/bootstrap/ip/");
        private static readonly JsonSerializerOptions SerializeOptions = new JsonSerializerOptions();
        private readonly HttpClient httpClient;

        static ArinClient()
        {
            SerializeOptions.Converters.Add(new JCardSerializer());
        }

        public ArinClient(Uri endpoint = null)
        {
            this.Endpoint = endpoint ?? DefaultIpEndpoint;
            this.httpClient = new HttpClient();
        }

        public Uri Endpoint { get; set; }

        public async Task<RdapResponse> Query(IPAddress query, CancellationToken cancellation = default)
        {
            var endpoint = new Uri(this.Endpoint, query.ToString());

            var response = await httpClient.GetAsync(endpoint, cancellation);

            if (!response.IsSuccessStatusCode)
            {
                return await DeserializeAs<ErrorResponse>(response.Content, cancellation);
            }

            return await DeserializeAs<IpResponse>(response.Content, cancellation);
        }

        private static async Task<T> DeserializeAs<T>(HttpContent content, CancellationToken cancellation)
        {
            var stream = await content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync(stream, typeof(T), options: SerializeOptions, cancellationToken: cancellation);

            return (T)result;
        }
    }
}
