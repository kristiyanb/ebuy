namespace EBuy.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using EBuy.Services.Models;
    using Models;
    using Models.Contacts;
    using Models.Products;
    using Models.Search;
    using Models.Users;

    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        public HomeController(IProductService productService,
            IUserService userService, 
            IMessageService messageService, 
            IMapper mapper)
        {
            this.productService = productService;
            this.userService = userService;
            this.messageService = messageService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.productService.GetLastFiveProducts<ProductGridModel>();

            return this.View(new ProductsCarouselModel { Products = products });
        }

        public async Task<IActionResult> Contacts()
        {
            if (this.User.Identity.Name != null)
            {
                var user = await this.userService.GetUserByUserName<UserViewModel>(this.User.Identity.Name);

                this.ViewData["Name"] = $"{user.FirstName} {user.LastName}";
                this.ViewData["Email"] = user.Email;
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contacts(MessageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return await this.Contacts();
            }

            var messageDto = this.mapper.Map<MessageDto>(input);

            await this.messageService.Add(messageDto, this.User.Identity.Name);

            return this.Redirect("/Home/MessageReceived");
        }

        public async Task<IActionResult> MessageReceived()
        {
            return this.View();
        }

        public async Task<IActionResult> ConfirmEmail()
        {
            return this.View();
        }

        public async Task<IActionResult> Result(string query)
        {
            var products = await this.productService.GetProductsByNameOrCategoryMatch<ProductGridModel>(query);

            return this.View(new SearchViewModel { Name = query, Products = products });
        }

        [Route("{*url}", Order = 999)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
