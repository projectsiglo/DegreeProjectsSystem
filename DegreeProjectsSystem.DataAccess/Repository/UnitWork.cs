using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _db;
        public ICareerRepository Career { get; private set; }
        public ICareerPersonRepository CareerPerson { get; private set; }
        public ICityRepository City { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IDepartmentFacultyRepository DepartmentFaculty { get; private set; }
        public IInstitutionRepository Institution { get; private set; }
        public IIdentityDocumentTypeRepository IdentityDocumentType { get; private set; }
        public IEducationLevelRepository EducationLevel { get; private set; }
        public IFacultyRepository Faculty { get; private set; }
        public IGenderRepository Gender { get; private set; }
        public IInstitutionContactChargeRepository InstitutionContactCharge { get; private set; }
        public IInstitutionTypeRepository InstitutionType { get; private set; }
        public IModalityRepository Modality { get; private set; }
        public IPersonRepository Person { get; private set; }
        public IProgramTypeRepository ProgramType { get; private set; }
        public ISolicitudeRepository Solicitude { get; private set; }
        public ISubmodalityRepository Submodality { get; private set; }
        public ITeachingFunctionRepository TeachingFunction { get; private set; }
        public ITypePersonRepository TypePerson { get; private set; }

        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Career = new CareerRepository(_db); // Inicializamos
            CareerPerson = new CareerPersonRepository(_db);
            City = new CityRepository(_db);
            Department = new DepartmentRepository(_db);
            DepartmentFaculty = new DepartmentFacultyRepository(_db);
            EducationLevel = new EducationLevelRepository(_db);
            Faculty = new FacultyRepository(_db);
            Gender = new GenderRepository(_db);
            Institution = new InstitutionRepository(_db);
            IdentityDocumentType = new IdentityDocumentTypeRepository(_db);
            InstitutionContactCharge = new InstitutionContactChargeRepository(_db);
            InstitutionType = new InstitutionTypeRepository(_db);
            Modality = new ModalityRepository(_db);
            Person = new PersonRepository(_db);
            ProgramType = new ProgramTypeRepository(_db);
            Solicitude = new SolicitudeRepository(_db);
            Submodality = new SubmodalityRepository(_db);
            TeachingFunction = new TeachingFunctionRepository(_db);
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
