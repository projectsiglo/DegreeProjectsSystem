using DegreeProjectsSystem.Models;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IIdentityDocumentTypeRepository : IRepository<IdentityDocumentType>
    {
        void Update(IdentityDocumentType identityDocumentType);
    }
}
