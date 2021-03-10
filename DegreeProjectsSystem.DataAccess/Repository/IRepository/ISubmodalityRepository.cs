using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ISubmodalityRepository : IRepository<Submodality>
    {
        void Update(Submodality submodality);
    }
}
