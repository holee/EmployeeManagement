﻿using EmployeeManagement.Models;
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












        public IActionResult Details(int id)
        {
            var dep = GetDepartment(id);
            return View(dep);
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
        private Department GetDepartment(int id)
        {
            Department dep = new();
            dep = GetAllDepartments().Where(d => d.Id == id)
                                        .FirstOrDefault();
            return dep;
    
        }

    }
}
