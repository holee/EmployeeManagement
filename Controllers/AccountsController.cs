using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Login()  
        {
            return View();
        }
    }
}
