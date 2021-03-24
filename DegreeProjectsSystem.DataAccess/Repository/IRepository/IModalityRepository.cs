using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IModalityRepository : IRepository<Modality>
    {
        void Update(Modality modality);
    }
}
