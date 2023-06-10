using System.Text.Json;
using System.Text.Json.Serialization;
using Zerodha.ServiceContracts;

namespace Zerodha.Services
{
    public class FinhubService:IFinhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public FinhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task< Dictionary<string, object>>  GetStockDetails(string stockSymbol)
        {
            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinhubToken"]}"),
                    Method = HttpMethod.Get

                };

                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);
                Stream stream = responseMessage.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();
                dataDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
            }
            if (dataDictionary == null)
            {
                return null;
            }
            return dataDictionary;
        }

       
    }
}
