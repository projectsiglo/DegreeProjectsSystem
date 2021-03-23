using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IInstitutionRepository : IRepository<Institution>
    {
        void Update(Institution institution);
    }
}
