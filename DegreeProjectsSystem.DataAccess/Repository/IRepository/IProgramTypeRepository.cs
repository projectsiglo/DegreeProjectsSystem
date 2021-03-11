using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IProgramTypeRepository : IRepository<ProgramType>
    {
        void Update(ProgramType programType);
    }
}
