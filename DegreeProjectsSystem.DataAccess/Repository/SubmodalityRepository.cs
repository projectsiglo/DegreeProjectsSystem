using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class SubmodalityRepository : Repository<Submodality>, ISubmodalityRepository
    {
        private readonly ApplicationDbContext _db;

        public SubmodalityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Submodality submodality)
        {
            var submodalityDb = _db.Submodalities.FirstOrDefault(s => s.Id == submodality.Id);
            if (submodalityDb != null)
            {
                submodalityDb.Name = submodality.Name;
                submodalityDb.Active = submodality.Active;
            }
        }

    }
}
