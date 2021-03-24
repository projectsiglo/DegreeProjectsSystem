using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class ModalityRepository : Repository<Modality>, IModalityRepository
    {
        private readonly ApplicationDbContext _db;

        public ModalityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Modality modality)
        {
            var modalityDb = _db.Modalities.FirstOrDefault(m => m.Id == modality.Id);
            if (modalityDb != null)
            {
                modalityDb.Name = modality.Name;
                modalityDb.EducationLevelId = modalityDb.EducationLevelId;
                modalityDb.Active = modality.Active;
            }
        }

    }
}
