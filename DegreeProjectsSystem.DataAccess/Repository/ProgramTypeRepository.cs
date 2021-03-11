using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class ProgramTypeRepository : Repository<ProgramType>, IProgramTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ProgramTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProgramType programType)
        {
            var programTypeDb = _db.ProgramTypes.FirstOrDefault(pt => pt.Id == programType.Id);
            if (programTypeDb != null)
            {
                programTypeDb.Name = programType.Name;
                programTypeDb.EducationLevelId = programType.EducationLevelId;
                programTypeDb.Active = programType.Active;
            }
        }

    }
}
