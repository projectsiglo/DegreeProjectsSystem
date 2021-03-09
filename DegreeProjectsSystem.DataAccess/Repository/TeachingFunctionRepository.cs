using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    class TeachingFunctionRepository : Repository<TeachingFunction>, ITeachingFunctionRepository
    {
        private readonly ApplicationDbContext _db;

        public TeachingFunctionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TeachingFunction teachingFunction)
        {
            var teachingFunctionDb = _db.TeachingFunctions.FirstOrDefault(tf => tf.Id == teachingFunction.Id);
            if (teachingFunctionDb != null)
            {
                teachingFunctionDb.Name = teachingFunction.Name;
                teachingFunctionDb.Active = teachingFunction.Active;
            }
        }

    }

}
