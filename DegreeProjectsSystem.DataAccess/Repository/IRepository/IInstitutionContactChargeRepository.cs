using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IInstitutionContactChargeRepository : IRepository<InstitutionContactCharge>
    {
        void Update(InstitutionContactCharge institutionContactCharge);
    }
}
