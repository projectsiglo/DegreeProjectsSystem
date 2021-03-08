using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface ITypePersonRepository : IRepository<TypePerson>
    {
        void Update(TypePerson typePerson);
    }
}
