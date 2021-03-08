using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class EducationLevelRepository : Repository<EducationLevel>, IEducationLevelRepository
    {
        private readonly ApplicationDbContext _db;

        public EducationLevelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(EducationLevel educationLevel)
        {
            var educationLevelDb = _db.EducationLevels.FirstOrDefault(en => en.Id == educationLevel.Id);
            if (educationLevelDb != null)
            {
                educationLevelDb.Name = educationLevel.Name;
                educationLevelDb.Active = educationLevel.Active;
            }
        }

    }
}
