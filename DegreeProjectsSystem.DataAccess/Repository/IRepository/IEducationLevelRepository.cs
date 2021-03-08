using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IEducationLevelRepository : IRepository<EducationLevel>
    {
        void Update(EducationLevel educationLevel);
    }
}
