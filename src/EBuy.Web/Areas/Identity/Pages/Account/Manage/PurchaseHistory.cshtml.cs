namespace EBuy.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using EBuy.Models;
    using EBuy.Services.Contracts;
    using EBuy.Services.Mapping;

    public class PurchaseHistoryModel : PageModel
    {
        private readonly IUserService userService;

        public PurchaseHistoryModel(IUserService userService)
        {
            this.userService = userService;
        }

        public List<PurchaseViewModel> Purchases { get; set; } 

        public class PurchaseViewModel : IMapFrom<Purchase>, IHaveCustomMappings
        {
            public string DateOfOrder { get; set; }

            public string Address { get; set; }

            public decimal Amount => this.Products.Sum(x => x.Price * x.Quantity);

            public List<PurchasedProductViewModel> Products { get; set; }

            public void CreateMappings(IProfileExpression configuration)
            {
                configuration.CreateMap<Purchase, PurchaseViewModel>()
                    .ForMember(x => x.DateOfOrder, opt => opt.MapFrom(y => y.DateOfOrder.ToString("dd/MM/yyyy")));
            }
        }

        public class PurchasedProductViewModel : IMapFrom<PurchasedProduct>
        {
            public string Name { get; set; }

            public decimal Price { get; set; }

            public int Quantity { get; set; }
        }

        public async Task<IActionResult> OnGet()
        {
            var products = await this.userService.GetPurchaseHistory<PurchaseViewModel>(this.User.Identity.Name);

            this.Purchases = products.ToList();

            return Page();
        }
    }
}