using Dapper;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AccountsController : Controller
    {
        //resolve dependency service
        private readonly DapperContext _dContext;

        public AccountsController(DapperContext dContext)
        {
            _dContext = dContext;
        }

        public IActionResult Login()  
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(string username,string password,bool rememberMe)
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public IActionResult Authenticate(LoginDto login)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreateUser(RegisterDto model)
        {
            var sql = "INSERT INTO [Accounts] VALUES(@id,@username,@email,@password)";
            model.Id = Guid.NewGuid();
            await _dContext.Connection.ExecuteAsync(sql, model);
            return View("Create");
        }

        [HttpGet]
        public IActionResult CreateUser1(RegisterDto model)
        {
            var sql = "INSERT INTO resgisters VALUES(@id,@username,@email,@password)";
            model.Id = Guid.NewGuid();
            _dContext.Connection.Execute(sql, model);
            return View("Create");
        }


    }
}
