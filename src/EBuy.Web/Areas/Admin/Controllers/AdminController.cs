namespace EBuy.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using EBuy.Common;

    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.EmployeeLevelAccess)]
    public abstract class AdminController : Controller
    {
    }
}
