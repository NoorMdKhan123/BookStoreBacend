using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersBL _ordersBL;
        private IConfiguration _iconfig;

        public OrdersController(IOrdersBL ordersBL, IConfiguration iconfig)
        {

            _ordersBL = ordersBL;
            _iconfig = iconfig;
        }
    }
}
