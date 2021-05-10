using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class CareerPersonRepository : Repository<CareerPerson>, ICareerPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public CareerPersonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CareerPerson careerPerson)
        {
            var careerPersonDb = _db.CareerPeople.FirstOrDefault(cpe => cpe.Id == careerPerson.Id);
            if (careerPersonDb != null)
            {
                careerPersonDb.CareerId = careerPerson.CareerId;
                careerPersonDb.PersonId = careerPerson.PersonId;
                careerPersonDb.Active = careerPerson.Active;
            }
        }

    }
}
