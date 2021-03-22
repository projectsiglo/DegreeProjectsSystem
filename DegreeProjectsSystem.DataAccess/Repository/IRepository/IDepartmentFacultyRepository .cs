using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IDepartmentFacultyRepository : IRepository<DepartmentFaculty>
    {
        void Update(DepartmentFaculty departmentFaculty);
    }
}
