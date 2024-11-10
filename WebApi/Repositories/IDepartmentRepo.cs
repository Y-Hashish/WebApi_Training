using WebAPI_Training.Models;

namespace WebAPI_Training.Repositories
{
    public interface IDepartmentRepo
    {
        public List<Department> GetAll();

        public Department GetById(int id);

        public void Add(Department department);

        public void Update(int Id ,Department department);
        public List<Department> GetWithEmp();

        public void Save();
        
    }
}
