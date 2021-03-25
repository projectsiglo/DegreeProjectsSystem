using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using System.Linq;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class SolicitudeRepository : Repository<Solicitude>, ISolicitudeRepository
    {
        private readonly ApplicationDbContext _db;

        public SolicitudeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Solicitude solicitude)
        {
            var solicitudeDb = _db.Solicitudes.FirstOrDefault(s => s.Id == solicitude.Id);
            if (solicitudeDb != null)
            {
                solicitudeDb.TitleDegreeWork = solicitude.TitleDegreeWork;
                solicitudeDb.ActNumber = solicitude.ActNumber;
                solicitudeDb.ActDate = solicitude.ActDate;
                solicitudeDb.ModalityChange = solicitude.ModalityChange;
                solicitudeDb.Observations = solicitude.Observations;
                solicitudeDb.Active = solicitude.Active;
            }
        }

    }
}
