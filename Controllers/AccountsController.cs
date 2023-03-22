using Dapper;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Login()  
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = _dContext.Connection.QuerySingle<RegisterDto>("SELECT * FROM users WHERE UserName=@username",
                    new
                    {
                        @username = model.UserName
                    });

                if (user != null)
                {
                    var claims = new List<Claim>{
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                            new Claim(ClaimTypes.Name,user.UserName),
                            new Claim(ClaimTypes.Email,user.Email)
                        };

                    var claimIdenities = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrinciple = new ClaimsPrincipal(claimIdenities);
                    await HttpContext.SignInAsync(claimPrinciple, new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    });

                    return Redirect("/Employees/Index");

                }
                return View(model);
            }
            return View(model);
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
            //username must has value
            //username has  3 character at least
            //username not exceed 100 characters
            if (string.IsNullOrEmpty(model.UserName))
            {
                ModelState.AddModelError(nameof(model.UserName), "please enter your username");
            }
            if(model.UserName !=null && model.UserName.Length < 3)
            {
                ModelState.AddModelError(nameof(model.UserName), "username has at least 3 characters.");
            }
            if (model.UserName != null && model.UserName.Length >100)
            {
                ModelState.AddModelError("", "username has only 100 characters.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var sql = "INSERT INTO [Users] VALUES(@id,@username,@email,@password)";
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



        public JsonResult CheckUserName(string userName)
        {
            var user = _dContext.Connection.Query<RegisterDto>("SELECT * FROM users WHERE username=@username",
                                                            new
                                                            {
                                                                @username = userName
                                                            }).FirstOrDefault();
            if(user != null)
            {
                return Json(data:false);
            }

            return Json(data: true);

        }


    }
}
