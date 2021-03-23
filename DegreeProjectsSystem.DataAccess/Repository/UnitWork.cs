﻿using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _db;
        public ICityRepository City { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IDepartmentFacultyRepository DepartmentFaculty { get; private set; }
        public IInstitutionRepository Institution { get; private set; }
        public IIdentityDocumentTypeRepository IdentityDocumentType { get; private set; }
        public IEducationLevelRepository EducationLevel { get; private set; }
        public IFacultyRepository Faculty { get; private set; }
        public IInstitutionContactChargeRepository InstitutionContactCharge { get; private set; }
        public IInstitutionTypeRepository InstitutionType { get; private set; }
        public ICareerRepository Career { get; private set; }
        public IProgramTypeRepository ProgramType { get; private set; }
        public ISubmodalityRepository Submodality { get; private set; }
        public ITeachingFunctionRepository TeachingFunction { get; private set; }
        public ITypePersonRepository TypePerson { get; private set; }

        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Career = new CareerRepository(_db); // Inicializamos
            City = new CityRepository(_db);
            Department = new DepartmentRepository(_db);
            DepartmentFaculty = new DepartmentFacultyRepository(_db);
            EducationLevel = new EducationLevelRepository(_db);
            Faculty = new FacultyRepository(_db);
            Institution = new InstitutionRepository(_db);
            IdentityDocumentType = new IdentityDocumentTypeRepository(_db);
            InstitutionContactCharge = new InstitutionContactChargeRepository(_db);
            InstitutionType = new InstitutionTypeRepository(_db);
            ProgramType = new ProgramTypeRepository(_db);
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
