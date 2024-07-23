using IFoodGastos.Api.Response;

namespace IFoodGastos.Models
{
    public class IFoodTotalOrdersDTO
    {
        public string Bearer { get; set; } = string.Empty;
        public int OrderCount { get; set; }
        public long Price { get; set; }
        public List<IFoodOrderDTO> Orders { get; set; } = [];
    }
}
