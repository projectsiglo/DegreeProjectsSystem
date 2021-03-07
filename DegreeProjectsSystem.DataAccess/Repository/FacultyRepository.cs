using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        private readonly ApplicationDbContext _db;

        public FacultyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Faculty faculty)
        {
            var facultyDb = _db.Departments.FirstOrDefault(f => f.Id == faculty.Id);
            if (facultyDb != null)
            {
                facultyDb.Name = facultyDb.Name;
                facultyDb.Active = facultyDb.Active;
            }
        }

    }
}
