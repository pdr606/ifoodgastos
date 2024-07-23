using IFoodGastos.Api.Response;
using IFoodGastos.Api.Service.Interface;
using Newtonsoft.Json;
using RestSharp;

namespace IFoodGastos.Api.Service.Implementation
{
    public class IFoodAPI : IIFoodAPI
    {
        private readonly RestClient _client;

        public IFoodAPI(IConfiguration configuration)
        {
            _client = new RestClient(baseUrl: configuration["IFoodAPIBaseAdress"] ?? string.Empty);
        }

        public async Task<List<IFoodOrderDTO>> GetOrders(string bearerToken)
        {
            bool hasMoreOrders = true;
            int pageNumber = 0;
            List<IFoodOrderDTO> orders = [];

            while (hasMoreOrders)
            {
                hasMoreOrders = await GetNextOrders(bearerToken, pageNumber, orders);
                pageNumber++;
            }

            return orders;
        }

        private async Task<bool> GetNextOrders(string bearerToken, int pageNumber, List<IFoodOrderDTO> orders)
        {
            List<IFoodOrderDTO> currentOrders = new List<IFoodOrderDTO>();
            var request = new RestRequest($"/v4/customers/me/orders?page={pageNumber}&size=10", RestSharp.Method.Get);
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(response.Content))
                {
                    currentOrders = JsonConvert.DeserializeObject<List<IFoodOrderDTO>>(response.Content) ?? new List<IFoodOrderDTO>();
                }
                orders.AddRange(currentOrders);
            }

            return currentOrders.Count == 10;
        }
    }
}
