using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        void Update(City city);
    }
}
