namespace EBuy.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using EBuy.Services.Contracts;

    public class PurchaseHistoryModel : PageModel
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseHistoryModel(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        public List<PurchaseViewModel> Purchases { get; set; } 

        public class PurchaseViewModel
        {
            public string DateOfOrder { get; set; }

            public string Address { get; set; }

            public decimal Amount => this.Products.Sum(x => x.Price * x.Quantity);

            public List<PurchasedProductViewModel> Products { get; set; }
        }

        public class PurchasedProductViewModel
        {
            public string Name { get; set; }

            public decimal Price { get; set; }

            public int Quantity { get; set; }
        }

        public async Task<IActionResult> OnGet()
        {
            var products = await this.purchaseService.GetUserPurchaseHistory<PurchaseViewModel>(this.User.Identity.Name);

            this.Purchases = products.ToList();

            return Page();
        }
    }
}