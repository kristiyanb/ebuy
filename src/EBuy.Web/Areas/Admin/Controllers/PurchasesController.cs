namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using EBuy.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Models.Purchases;

    public class PurchasesController : AdminController
    {
        private readonly IPurchaseService purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        public async Task<IActionResult> Index()
        {
            var purchases = await this.purchaseService.GetAll<PurchaseViewModel>();

            return View(new PurchaseListModel { Purchases = purchases.ToList() });
        }
    }
}
