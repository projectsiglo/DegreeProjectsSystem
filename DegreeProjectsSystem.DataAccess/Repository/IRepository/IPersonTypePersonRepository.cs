using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IPersonTypePersonRepository : IRepository<PersonTypePerson>
    {
        void Update(PersonTypePerson personTypePerson);
    }
}
