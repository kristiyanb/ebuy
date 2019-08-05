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

        [HttpPost(Name = "Send")]
        [Route("send")]
        public async Task<ActionResult> Send(OrderInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.StatusCode(400);
            }

            var orderDto = this.mapper.Map<OrderDto>(input);
            var isCreated = await this.orderService.Create(orderDto, this.User.Identity.Name);

            if (!isCreated)
            {
                return this.StatusCode(400);
            }

            return this.StatusCode(201);
        }
    }
}
