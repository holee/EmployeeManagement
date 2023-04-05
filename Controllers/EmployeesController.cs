using Dapper;
using EmployeeManagement.Data;
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
        private readonly DapperContext _dapper;

        public EmployeesController(DapperContext dapper)
        {
            _dapper = dapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var emps = _dapper.Connection.Query<Employee>("spr_employee_select",
                                                        new
                                                        {
                                                            @EmpId=0
                                                        },commandType:CommandType.StoredProcedure);
            return View(emps);
        }
        [HttpGet]
        public IActionResult Create()
        {

            var deps = _dapper.Connection.Query<Department>("SELECT * FROM department;");
            ViewBag.deps = new SelectList(deps, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Store([Bind("FirstName","LastName","Gender","Address","DepartmentId")]Employee emp)
        {
            if (ModelState.IsValid)
            {
               var rowEff=await _dapper.Connection.ExecuteAsync("spr_employee_insert", 
                                                commandType: CommandType.StoredProcedure,
                                                param: new
                                                {
                                                    emp.FirstName,
                                                    emp.LastName,
                                                    emp.DepartmentId,
                                                    emp.Gender,
                                                    emp.Address
                                                });
                if (rowEff > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var deps = _dapper.Connection.Query<Department>("SELECT * FROM department;");
            ViewBag.deps = new SelectList(deps, "Id", "Name");
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emp = _dapper.Connection.QuerySingleOrDefault<Employee>("spr_employee_select",
                                                        new
                                                        {
                                                            @EmpId=id
                                                        }, 
                                                        commandType: CommandType.StoredProcedure);
            if (emp == null) return NotFound();

            var deps = _dapper.Connection.Query<Department>("SELECT * FROM department;");
            ViewBag.deps = new SelectList(deps, "Id", "Name");
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee emp)
        {
            var found = await _dapper.Connection.QueryFirstOrDefaultAsync<Employee>("spr_employee_select",
                                                commandType: CommandType.StoredProcedure,
                                                param: new
                                                {
                                                    @EmpId=id
                                                });
            if (found == null) return NotFound();
            if (ModelState.IsValid)
            {
                var rowEff = await _dapper.Connection.ExecuteAsync("spr_employee_update",
                                                 commandType: CommandType.StoredProcedure,
                                                 param: new
                                                 {
                                                     emp.EmpId,
                                                     emp.FirstName,
                                                     emp.LastName,
                                                     emp.DepartmentId,
                                                     emp.Gender,
                                                     emp.Address
                                                 });
                if (rowEff > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var deps = _dapper.Connection.Query<Department>("SELECT * FROM department;");
            ViewBag.deps = new SelectList(deps, "Id", "Name");
            return View(emp);
        }

        public IActionResult Delete(int id)
        {
            var deleted = _dapper.Connection.Execute("spr_employee_delete",
                                                    new
                                                    {
                                                        @EmpId = id
                                                    },
                                                    commandType:CommandType.StoredProcedure);
            if (deleted > 0) {

                return Redirect("/Employees/Index");
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var emp = _dapper.Connection.QuerySingleOrDefault<Employee>("spr_employee_select",
                                                        new
                                                        {
                                                            @EmpId = id
                                                        },
                                                        commandType: CommandType.StoredProcedure);
            if (emp == null) return NotFound();
            return View(emp);
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
