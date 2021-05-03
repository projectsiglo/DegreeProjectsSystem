using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Person person)
        {
            var personDb = _db.People.FirstOrDefault(pe => pe.Id == person.Id);
            if (personDb != null)
            {
                personDb.IdentificationNumber = person.IdentificationNumber;
                personDb.IdentityDocumentTypeId = person.IdentityDocumentTypeId;
                personDb.Names = person.Names;
                personDb.Surnames = person.Surnames;
                personDb.GenderId = person.GenderId;
                personDb.DepartmentId = person.DepartmentId;
                personDb.CityId = person.CityId;
                personDb.Address = person.Address;
                personDb.Phone = person.Phone;
                personDb.Mobile = person.Mobile;
                personDb.Active = person.Active;
            }
        }

    }
}