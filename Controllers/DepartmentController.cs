using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AzureGitHubDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AzureGitHubDemoDbContext db;

        public EmployeeController(AzureGitHubDemoDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            string message = string.Empty;
            try
            {
                var result = db.Employees.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                message = ex.Message + Environment.NewLine;
                message += ex.InnerException?.Message + Environment.NewLine;
                message += ex.StackTrace;
               Response.WriteAsJsonAsync(message);
            }
            return UnprocessableEntity(message);
            
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            string message = string.Empty;
            try
            {
                var result = db.Employees.FirstOrDefault(x => x.Id == Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                message = ex.Message + Environment.NewLine;
                message += ex.InnerException?.Message + Environment.NewLine;
                message += ex.StackTrace;
                Response.WriteAsJsonAsync(message);
            }
            return UnprocessableEntity(message);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Employee emp)
        {
            string message = string.Empty;
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                var uri = new Uri(Url.ActionLink(nameof(GetEmployeeById), "Employee", new { Id = emp.Id }));
                string url = uri.ToString();
                return new CreatedResult(url, emp);
            }
            catch (Exception ex)
            {
                message = ex.Message + Environment.NewLine;
                message += ex.InnerException?.Message + Environment.NewLine;
                message += ex.StackTrace;
                Response.WriteAsJsonAsync(message);
            }
            return UnprocessableEntity(message);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AzureGitHubDemoDbContext db;
        private string message;

        public DepartmentController(AzureGitHubDemoDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var result = db.Departments.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                message = ex.Message + Environment.NewLine;
                message += ex.InnerException?.Message + Environment.NewLine;
                message += ex.StackTrace;
                Response.WriteAsJsonAsync(message);
            }
            return UnprocessableEntity(message);
            
        }

        [HttpGet]
        [Route("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var result = db.Departments.FirstOrDefault(x => x.Id == id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                message = ex.Message + Environment.NewLine;
                message += ex.InnerException?.Message + Environment.NewLine;
                message += ex.StackTrace;
                Response.WriteAsJsonAsync(message);
            }
            return UnprocessableEntity(message);
            
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Department obj)
        {
            try
            {
                db.Departments.Add(obj);
                db.SaveChanges();
                var uri = new Uri(Url.ActionLink(nameof(GetDepartmentById), "Department", new { Id = obj.Id }));
                string url = uri.ToString();
                return new CreatedResult(url, obj);
            }
            catch (Exception ex)
            {
                message = ex.Message + Environment.NewLine;
                message += ex.InnerException?.Message + Environment.NewLine;
                message += ex.StackTrace;
                Response.WriteAsJsonAsync(message);
            }
            return UnprocessableEntity(message);
            
        }
    }
}
