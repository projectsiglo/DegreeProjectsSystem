using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(City city)
        {
            var cityDb = _db.Cities.FirstOrDefault(c => c.Id == city.Id);
            if (cityDb != null)
            {
                cityDb.Name = city.Name;
                cityDb.DepartmentId = city.DepartmentId;
                cityDb.Active = city.Active;
            }
        }

    }
}