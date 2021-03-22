using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class DepartmentFacultyRepository : Repository<DepartmentFaculty>, IDepartmentFacultyRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentFacultyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DepartmentFaculty departmentFaculty)
        {
            var departmentFacultyDb = _db.DepartmentFaculties.FirstOrDefault(df => df.Id == departmentFaculty.Id);
            if (departmentFacultyDb != null)
            {
                departmentFacultyDb.Name = departmentFaculty.Name;
                departmentFacultyDb.FacultyId = departmentFaculty.FacultyId;
                departmentFacultyDb.Active = departmentFaculty.Active;
            }
        }

    }
}
