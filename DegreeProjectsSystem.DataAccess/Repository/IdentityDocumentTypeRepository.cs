using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class IdentityDocumentTypeRepository : Repository<IdentityDocumentType>, IIdentityDocumentTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public IdentityDocumentTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(IdentityDocumentType identityDocumentType)
        {
            var identityDocumentTypeDb = _db.IdentityDocumentTypes.FirstOrDefault(idt => idt.Id == identityDocumentType.Id);
            if (identityDocumentTypeDb != null)
            {
                identityDocumentTypeDb.Name = identityDocumentType.Name;
                identityDocumentTypeDb.Active = identityDocumentType.Active;
            }
        }

    }
}
