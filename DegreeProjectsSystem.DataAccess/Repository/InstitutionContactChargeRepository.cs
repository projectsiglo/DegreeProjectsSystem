using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class InstitutionContactChargeRepository : Repository<InstitutionContactCharge>, IInstitutionContactChargeRepository
    {
        private readonly ApplicationDbContext _db;

        public InstitutionContactChargeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InstitutionContactCharge institutionContactCharge)
        {
            var institutionContactChargeDb = _db.InstitutionContactCharges.FirstOrDefault(icc => icc.Id == institutionContactCharge.Id);
            if (institutionContactChargeDb != null)
            {
                institutionContactChargeDb.Name = institutionContactCharge.Name;
                institutionContactChargeDb.Active = institutionContactCharge.Active;
            }
        }

    }
}
