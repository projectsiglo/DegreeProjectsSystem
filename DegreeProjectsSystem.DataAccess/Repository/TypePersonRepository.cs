using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class TypePersonRepository : Repository<TypePerson>, ITypePersonRepository
    {
        private readonly ApplicationDbContext _db;

        public TypePersonRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TypePerson typePerson)
        {
            var typePersonDb = _db.TypePeople.FirstOrDefault(tp => tp.Id == typePerson.Id);
            if (typePersonDb != null)
            {
                typePersonDb.Name = typePerson.Name;
                typePersonDb.Active = typePerson.Active;
            }
        }

    }
}
