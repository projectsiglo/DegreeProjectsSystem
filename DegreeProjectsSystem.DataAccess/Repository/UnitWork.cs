using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _db;
        public IDepartmentRepository Department { get; private set; }
        public IEducationLevelRepository EducationLevel { get; private set; }
        public IFacultyRepository Faculty { get; private set; }
        public ITypePersonRepository TypePerson { get; private set; }
       
        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Department = new DepartmentRepository(_db); // Inicializamos
            EducationLevel = new EducationLevelRepository(_db);
            Faculty = new FacultyRepository(_db);
            TypePerson = new TypePersonRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
