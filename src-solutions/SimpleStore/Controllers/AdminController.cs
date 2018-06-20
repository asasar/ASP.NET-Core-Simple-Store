using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SimpleStore.Controllers.Web
{
    [Authorize(Roles = "Backend")]
    public class AdminController:Controller
    {
        public AdminController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
