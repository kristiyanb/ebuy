namespace EBuy.Web.Areas.Identity.Pages.Account.Manage
{
    using EBuy.Models;
    using EBuy.Services.Contracts;
    using EBuy.Services.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PurchaseHistoryModel : PageModel
    {
        private readonly IUserService userService;

        public PurchaseHistoryModel(IUserService userService)
        {
            this.userService = userService;
        }

        public List<PurchaseViewModel> Purchases { get; set; } 

        public class PurchaseViewModel : IMapFrom<Purchase>
        {
            public string DateOfOrder { get; set; }

            public string Address { get; set; }

            public decimal Amount { get; set; }

            public List<PurchasedProductViewModel> Products { get; set; }
        }

        public class PurchasedProductViewModel : IMapFrom<PurchasedProduct>
        {
            public string Name { get; set; }

            public decimal Price { get; set; }
        }

        public async Task<IActionResult> OnGet()
        {
            var products = await this.userService.GetPurchaseHistory<PurchaseViewModel>(this.User.Identity.Name);

            this.Purchases = products.ToList();

            return Page();
        }
    }
}