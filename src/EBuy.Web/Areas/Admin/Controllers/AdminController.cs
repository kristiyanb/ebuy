namespace EBuy.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    [Authorize(Roles = "Administrator, Manager, Employee")]
    public abstract class AdminController : Controller
    {
    }
}
