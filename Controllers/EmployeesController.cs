using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var emp=new Employee { FirstName="Data",LastName="Data"};
            return View(emp);
        }
        [NonAction] 
        public ActionResult Dialog() 
        {
            return PartialView("Dialog");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult Store()
        {
            return RedirectToAction("Edit",new { id=1000});
        }
        public RedirectResult YoutTube()
        {
            return Redirect("https://www.youtube.com");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        public int Edit(int id, Employee emp)
        {
            return id;
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

    }
}
