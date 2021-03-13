using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ICareerRepository : IRepository<Career>
    {
        void Update(Career career);
    }
}
