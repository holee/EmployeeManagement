using EmployeeManagement.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//Adding Services

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<DapperContext>();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Accounts/Login";
                    options.LogoutPath = "/Accounts/Logout";
                    options.ReturnUrlParameter = "ReturnUrl";
                    options.AccessDeniedPath = "/Accounts/AccessDenies";
                });

//Adding Middleware
var app = builder.Build();
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider=new PhysicalFileProvider(
//        Path.Combine(builder.Environment.ContentRootPath,"Example")),
//    RequestPath="/Example/Ex"
//});


//app.MapDefaultControllerRoute();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("home", pattern: "{controller=Employees}/{action=List}/{id?}");

app.Run();
