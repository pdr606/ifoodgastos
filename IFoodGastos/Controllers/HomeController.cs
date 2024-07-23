using IFoodGastos.Api.Response;
using IFoodGastos.Api.Service.Interface;
using IFoodGastos.Models;
using Microsoft.AspNetCore.Mvc;

namespace IFoodGastos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIFoodAPI _iFoodAPI;

        public HomeController(IIFoodAPI iFoodAPI)
        {
            _iFoodAPI = iFoodAPI;
        }

        public IActionResult Index()
        {
            return View(new IFoodTotalOrdersDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string bearerToken)
        {
            var request = await _iFoodAPI.GetOrders(bearerToken);

            var response = new IFoodTotalOrdersDTO()
            {
                Bearer = bearerToken,
                OrderCount = request.Count,
                Price = 0,
                Orders = request
            };

            return View(response);
        }
    }
}
