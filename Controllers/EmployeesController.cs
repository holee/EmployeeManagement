using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        [ActionName("List")]
        [HttpGet]
        public IActionResult Index()
        {
            //using view bag
            ViewBag.MyName = "Vanthy";
            ViewBag.Names = new[] { "Vanthy","Kosol","Theara","Ly An" };
            ViewBag.Employee = new Employee { Name = "Sreynou", Id = 1 };
            //Using ViewData
            ViewData["Name"] = "Ry Mang";
            ViewData["newNames"] = new[] { "Vanthy", "Kosol", "Theara", "Ly An" };
            ViewData["Emp"] = new Employee { Name = "Sok Pisey", Id = 2 };



            return View("Index");
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
