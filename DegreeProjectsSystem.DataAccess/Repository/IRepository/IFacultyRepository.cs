using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IFacultyRepository : IRepository<Faculty>
    {
        void Update(Faculty faculty);
    }
}