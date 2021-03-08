using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class InstitutionTypeRepository : Repository<InstitutionType>, IInstitutionTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public InstitutionTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InstitutionType institutionType)
        {
            var institutionTypeDb = _db.InstitutionTypes.FirstOrDefault(it => it.Id == institutionType.Id);
            if (institutionTypeDb != null)
            {
                institutionTypeDb.Name = institutionType.Name;
                institutionTypeDb.Active = institutionType.Active;
            }
        }

    }
}
