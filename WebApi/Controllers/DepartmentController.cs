using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Training.Models;
using WebAPI_Training.Repositories;

namespace WebAPI_Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentRepo dept;
        public DepartmentController(IDepartmentRepo _dept)
        {
            dept = _dept;
        }
        //ApplicationDbContext context;
        //public DepartmentController(ApplicationDbContext _context)
        //{
        //    context = _context;
        //}
        [HttpGet]
        public IActionResult DisplayAll()
        {
            List<Department> departmentList = dept.GetAll();
            //return Ok(dept.GetAll());
            return Ok(departmentList);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            //return Ok(dept.GetById(id));
            Department department = dept.GetById(id);    //context.departments.FirstOrDefault(department => department.Id == id);
            return Ok(department);
        }
        [HttpPost]
        public IActionResult Add(Department department)
        {
            dept.Add(department);
            dept.Save();
            //context.Add(department);
            //context.SaveChanges();
            //return Created($"http://localhost:5223/api/Department/{department.Id}", department);
            return CreatedAtAction("GetById", new {id = department.Id} , department);
        }
        [HttpPut("{Id}")]
        public IActionResult Update(int Id ,Department department)
        {
            if (department !=null)
            {
                dept.Update(Id ,department);
                dept.Save();
                return NoContent();
            }
            else
            {
                return NotFound("Agent not found...");
            }
        }
    }
}
