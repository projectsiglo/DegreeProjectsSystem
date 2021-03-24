using System;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IUnitWork : IDisposable
    {
        ICareerRepository Career { get; }
        ICityRepository City { get; }
        IDepartmentRepository Department{ get; }
        IDepartmentFacultyRepository DepartmentFaculty { get; }
        IEducationLevelRepository EducationLevel { get; }
        IFacultyRepository Faculty { get; }
        IIdentityDocumentTypeRepository IdentityDocumentType { get; }
        IInstitutionRepository Institution { get; }
        IInstitutionContactChargeRepository InstitutionContactCharge { get; }
        IInstitutionTypeRepository InstitutionType { get; }
        IModalityRepository Modality { get; }
        IProgramTypeRepository ProgramType { get; }
        ISubmodalityRepository Submodality { get; }
        ITeachingFunctionRepository TeachingFunction { get; }   
        ITypePersonRepository TypePerson { get; }
        void Save();
    }
}
