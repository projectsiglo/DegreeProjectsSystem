using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class PersonTypePersonRepository : Repository<PersonTypePerson>, IPersonTypePersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonTypePersonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PersonTypePerson personTypePerson)
        {
            var personTypePersonDb = _db.PersonTypePeople.FirstOrDefault(cpe => cpe.Id == personTypePerson.Id);
            if (personTypePersonDb != null)
            {
                personTypePersonDb.PersonId = personTypePerson.PersonId;
                personTypePersonDb.TypePersonId = personTypePerson.TypePersonId;
                personTypePersonDb.Active = personTypePerson.Active;
            }
        }

    }
}
