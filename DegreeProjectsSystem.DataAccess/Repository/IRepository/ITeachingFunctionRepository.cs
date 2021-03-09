using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ITeachingFunctionRepository : IRepository<TeachingFunction>
    {
        void Update(TeachingFunction teachingFunction);
    }
}
