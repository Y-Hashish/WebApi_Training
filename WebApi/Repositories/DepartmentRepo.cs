using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI_Training.Models;

namespace WebAPI_Training.Repositories
{
    public class DepartmentRepo : IDepartmentRepo
    {
        ApplicationDbContext context;
        public DepartmentRepo(ApplicationDbContext _context)
        {
            context = _context;
        }
        public List<Department> GetAll()
        {
            return context.departments.ToList();
        }
        public Department GetById(int id)
        {
            return context.departments.FirstOrDefault(d => d.Id == id);
        }
        public void Add(Department department)
        {
            context.departments.Add(department);
        }
        public void Update(int Id, Department department)
        {
            Department deptDb = GetById(Id);
            if (deptDb != null)
            {
                deptDb.Name = department.Name;
                deptDb.MangerName = department.MangerName;
            }
        }
        public List<Department> GetWithEmp()
        {
            return context.departments.Include(d=>d.Employees).ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }

    }
}
