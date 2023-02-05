using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;

namespace AzureGitHubDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly AzureGitHubDemoDbContext db;

        public SkillController(AzureGitHubDemoDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("GetAllSkills")]
        public async Task<IActionResult> GetAllSkills()
        {
            string message = string.Empty;
            try
            {
                SqlConnection con = new SqlConnection(ConfManager.AdoNetConnStr);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Select * from Skill";
                con.Open();
                var rdr = await cmd.ExecuteReaderAsync();
                List<Skill> skills = new List<Skill>(); 
                while(rdr.Read())
                {
                    skills.Add(new Skill
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name= Convert.ToString(rdr["Name"])
                    });
                }
                var result = skills;
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
        [Route("GetSkillById")]
        public async Task<IActionResult> GetSkillById(int Id)
        {
            string message = string.Empty;
            try
            {
                var result = db.Skills.FirstOrDefault(x => x.Id == Id);
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
        public async Task<IActionResult> Post(Skill obj)
        {
            string message = string.Empty;
            try
            {
                db.Skills.Add(obj);
                db.SaveChanges();
                var uri = new Uri(Url.ActionLink(nameof(GetSkillById), "Skill", new { Id = obj.Id }));
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
