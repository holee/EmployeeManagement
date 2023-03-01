using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.Controllers
{
    public class DepartmentsController : Controller
    {
        private const string conString = @"Server=localhost;database=b202
                                           ;Trusted_Connection=true;
                                           TrustServerCertificate=true;";
        public IActionResult Index()
        {
            var deps = GetAllDepartments();
            return View(deps);
        }

        [HttpGet]
        public IActionResult List()
        {
            var deps = GetAllDepartmentAsDt();
            return View(deps);
        }
        public IActionResult Details(int id)
        {
            var dep = GetDepartment(id);
            return View(dep);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dep)
        {
            if (ModelState.IsValid)
            {
                if (CreateDeparment(dep))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(dep);
            }
            return View(dep);

        }


        public IActionResult Edit(int id)
        {
            var dep = GetDepartment(id);
            return View(dep);
        }

        [HttpPost]
        public IActionResult Edit(int id,Department dep)
        {
            
            if (ModelState.IsValid)
            {
                if (UpdateDeparment(id, dep)) return RedirectToAction("Index");
                return View(dep);
            }
            return View(dep);

        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (DeleteDeparment(id)) return RedirectToAction("Index");
            return BadRequest();
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

        private DataTable GetAllDepartmentAsDt() 
        {
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM Department;", conn))
                {
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    dap.Fill(dt);
                    return dt;
                }
            }
        }


        private Department GetDepartment(int id)
        {
            Department dep = new();
            dep = GetAllDepartments().Where(d => d.Id == id)
                                        .FirstOrDefault();
            return dep;
    
        }

        private bool CreateDeparment(Department dep)
        {
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"INSERT INTO Department(Name,Description)
                                                VALUES(@name,@dep)", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", dep.Name);
                    cmd.Parameters.AddWithValue("@dep", dep.Description);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private bool UpdateDeparment(int id,Department dep)
        {
            var dp = GetDepartment(id);
            if (dp == null) return false;
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"UPDATE Department SET Name=@name,
                                                Description=@desc WHERE id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id",id);
                    cmd.Parameters.AddWithValue("@name", dep.Name);
                    cmd.Parameters.AddWithValue("@desc", dep.Description);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private bool DeleteDeparment(int id)
        {
            var dp = GetDepartment(id);
            if (dp == null) return false;
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"DELETE FROM Department WHERE id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
