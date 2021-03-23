using System;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IUnitWork : IDisposable
    {
        ICityRepository City { get; }
        IDepartmentRepository Department{ get; }
        IDepartmentFacultyRepository DepartmentFaculty { get; }
        IEducationLevelRepository EducationLevel { get; }
        IInstitutionRepository Institution { get; }
        IIdentityDocumentTypeRepository IdentityDocumentType { get; }
        IFacultyRepository Faculty { get; }
        IInstitutionContactChargeRepository InstitutionContactCharge { get; }
        IInstitutionTypeRepository InstitutionType { get; }
        ICareerRepository Career { get; }
        IProgramTypeRepository ProgramType { get; }
        ISubmodalityRepository Submodality { get; }
        ITeachingFunctionRepository TeachingFunction { get; }   
        ITypePersonRepository TypePerson { get; }
        void Save();
    }
}
