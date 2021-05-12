using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class RecognitionRepository : Repository<Recognition>, IRecognitionRepository
    {
        private readonly ApplicationDbContext _db;

        public RecognitionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Recognition recognition)
        {
            var recognitionDb = _db.Recognitions.FirstOrDefault(re => re.Id == recognition.Id);
            if (recognitionDb != null)
            {
                recognitionDb.Name = recognition.Name;
                recognitionDb.EducationLevelId = recognition.EducationLevelId;
                recognitionDb.Active = recognition.Active;
            }
        }

    }
}
