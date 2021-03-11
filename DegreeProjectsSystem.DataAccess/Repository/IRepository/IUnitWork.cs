using System;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IUnitWork : IDisposable
    {
        IDepartmentRepository Department{ get; }
        IEducationLevelRepository EducationLevel { get; }
        IFacultyRepository Faculty { get; }
        IInstitutionTypeRepository InstitutionType { get; }
        IProgramTypeRepository ProgramType { get; }
        ISubmodalityRepository Submodality { get; }
        ITeachingFunctionRepository TeachingFunction { get; }
        
        ITypePersonRepository TypePerson { get; }
        void Save();
    }
}
