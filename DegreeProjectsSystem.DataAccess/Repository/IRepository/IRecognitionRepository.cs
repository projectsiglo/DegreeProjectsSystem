using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IRecognitionRepository : IRepository<Recognition>
    {
        void Update(Recognition recognition);
    }
}
