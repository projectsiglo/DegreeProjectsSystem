using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        void Update(Department department);
    }
}
