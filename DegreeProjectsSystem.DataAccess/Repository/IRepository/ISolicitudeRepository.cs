using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ISolicitudeRepository : IRepository<Solicitude>
    {
        void Update(Solicitude solicitude);
    }
}
