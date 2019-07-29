namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
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

            return this.View(new PurchaseListModel { Purchases = purchases });
        }
    }
}
