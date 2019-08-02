namespace EBuy.Web.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using EBuy.Services.Models;
    using Models.Orders;

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost("Send")]
        [Route("send")]
        public async Task<ActionResult> Send(OrderInputModel input)
        {
            var orderDto = this.mapper.Map<OrderDto>(input);

            await this.orderService.Create(orderDto, this.User.Identity.Name);

            return this.StatusCode(201);
        }
    }
}
