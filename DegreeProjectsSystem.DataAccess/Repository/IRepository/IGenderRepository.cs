using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IGenderRepository : IRepository<Gender>
    {
        void Update(Gender gender);
    }
}
