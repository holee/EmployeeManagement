using EmployeeManagement.Data;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//Adding Services

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DapperContext>();

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

app.MapControllerRoute("home", pattern: "{controller=Employees}/{action=List}/{id?}");

app.Run();
