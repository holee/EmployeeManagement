using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private const string conString = @"Server=localhost;database=b202
                                           ;Trusted_Connection=true;
                                           TrustServerCertificate=true;";
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

            ViewBag.deps = new List<SelectListItem>
            {
                new SelectListItem{ Text="IT",Value="1"},
                new SelectListItem{ Text="Account",Value="2"},
                new SelectListItem{ Text="HR",Value="3"},
                new SelectListItem{ Text="Marketing",Value="3"},
                new SelectListItem{ Text="Administrator",Value="4"}

            };

            ViewBag.depss = new SelectList(GetAllDepartments(), "Id", "Name");

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


        private List<Department> GetAllDepartments()
        {
            List<Department> deps = new List<Department>();
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM Department;", conn))
                {
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(dt);
                    deps = (from dr in dt.AsEnumerable()
                            select new Department
                            {
                                Id = dr.Field<int>("Id"),
                                Name = dr.Field<string>("Name"),
                                Description = dr.Field<string?>("Description")
                            }).ToList();

                }
                return deps ?? default;
            }
        }
    }
}
