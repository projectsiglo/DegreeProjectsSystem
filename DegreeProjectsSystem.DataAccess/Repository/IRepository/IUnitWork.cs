using System;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IUnitWork : IDisposable
    {
        ICityRepository City { get; }
        IDepartmentRepository Department{ get; }
        IEducationLevelRepository EducationLevel { get; }
        IFacultyRepository Faculty { get; }
        IInstitutionTypeRepository InstitutionType { get; }
        ICareerRepository Career { get; }
        IProgramTypeRepository ProgramType { get; }
        ISubmodalityRepository Submodality { get; }
        ITeachingFunctionRepository TeachingFunction { get; }
        
        ITypePersonRepository TypePerson { get; }
        void Save();
    }
}
