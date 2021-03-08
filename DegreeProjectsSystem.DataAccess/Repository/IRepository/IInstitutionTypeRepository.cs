using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IInstitutionTypeRepository : IRepository<InstitutionType>
    {
        void Update(InstitutionType institutionType);
    }
}
