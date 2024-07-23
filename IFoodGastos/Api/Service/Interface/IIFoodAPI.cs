using IFoodGastos.Api.Response;

namespace IFoodGastos.Api.Service.Interface
{
    public interface IIFoodAPI
    {
        Task<List<IFoodOrderDTO>> GetOrders(string bearerToken);
    }
}
