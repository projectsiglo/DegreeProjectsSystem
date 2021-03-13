using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class CareerRepository : Repository<Career>, ICareerRepository
    {
        private readonly ApplicationDbContext _db;

        public CareerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Career career)
        {
            var careerDb = _db.Careers.FirstOrDefault(c => c.Id == career.Id);
            if (careerDb != null)
            {
                careerDb.Name = career.Name;
                careerDb.ProgramTypeId = career.ProgramTypeId;
                careerDb.Active = career.Active;
            }
        }

    }
}
