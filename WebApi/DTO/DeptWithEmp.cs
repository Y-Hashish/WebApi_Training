using System.Security.Principal;

namespace WebApi.DTO
{
    public class DeptWithEmp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmpCount { get; set; }

    }
}
