using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var result = db.Employees.ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var result = db.Employees.FirstOrDefault(x => x.Id == Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
            var uri = new Uri(Url.ActionLink(nameof(GetEmployeeById), "Employee", new { Id = emp.Id }));
            string url = uri.ToString();
            return new CreatedResult(url, emp);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AzureGitHubDemoDbContext db;

        public DepartmentController(AzureGitHubDemoDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = db.Departments.ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = db.Departments.FirstOrDefault(x => x.Id == id);
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Post(Department obj)
        {
            db.Departments.Add(obj);
            db.SaveChanges();
            var uri = new Uri(Url.ActionLink(nameof(GetDepartmentById), "Department", new { Id = obj.Id }));
            string url = uri.ToString();
            return new CreatedResult(url, obj);
        }
    }
}
