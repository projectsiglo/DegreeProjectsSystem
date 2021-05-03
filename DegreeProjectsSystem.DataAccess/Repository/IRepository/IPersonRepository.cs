using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IPersonRepository : IRepository<Person>
    {
        void Update(Person person);
    }
}
