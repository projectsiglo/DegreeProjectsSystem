using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ICareerPersonRepository : IRepository<CareerPerson>
    {
        void Update(CareerPerson careerPerson);
    }
}
