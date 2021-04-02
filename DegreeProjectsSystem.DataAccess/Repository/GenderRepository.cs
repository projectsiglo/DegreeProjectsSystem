using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        private readonly ApplicationDbContext _db;

        public GenderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Gender gender)
        {
            var genderDb = _db.Genders.FirstOrDefault(g => g.Id == gender.Id);
            if (genderDb != null)
            {
                genderDb.Name = gender.Name;
                genderDb.Active = gender.Active;
            }
        }

    }
}
