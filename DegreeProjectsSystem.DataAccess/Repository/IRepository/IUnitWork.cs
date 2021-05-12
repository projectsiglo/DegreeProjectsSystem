using System;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IUnitWork : IDisposable
    {
        ICareerRepository Career { get; }
        ICareerPersonRepository CareerPerson { get; }
        ICityRepository City { get; }
        IDepartmentRepository Department{ get; }
        IDepartmentFacultyRepository DepartmentFaculty { get; }
        IEducationLevelRepository EducationLevel { get; }
        IFacultyRepository Faculty { get; }
        IGenderRepository Gender { get;  }
        IIdentityDocumentTypeRepository IdentityDocumentType { get; }
        IInstitutionRepository Institution { get; }
        IInstitutionContactChargeRepository InstitutionContactCharge { get; }
        IInstitutionTypeRepository InstitutionType { get; }
        IModalityRepository Modality { get; }
        IPersonRepository Person { get; }
        IProgramTypeRepository ProgramType { get; }
        IRecognitionRepository Recognition { get; }
        ISolicitudeRepository Solicitude { get;  }
        ISubmodalityRepository Submodality { get; }
        ITeachingFunctionRepository TeachingFunction { get; }   
        ITypePersonRepository TypePerson { get; }
        void Save();
    }
}
