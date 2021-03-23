using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class InstitutionRepository : Repository<Institution>, IInstitutionRepository
    {
        private readonly ApplicationDbContext _db;

        public InstitutionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Institution institution)
        {
            var institutionDb = _db.Institutions.FirstOrDefault(i => i.Id == institution.Id);
            if (institutionDb != null)
            {
                institutionDb.Name = institution.Name;
                institutionDb.InstitutionTypeId = institution.InstitutionTypeId;
                institutionDb.Nit = institution.Nit;
                institutionDb.Email = institution.Email;
                institutionDb.Phone = institution.Phone;
                institutionDb.Observations = institution.Observations;
                institutionDb.Active = institution.Active;
            }
        }

    }
}