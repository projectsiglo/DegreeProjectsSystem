using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreeProjectsSystem.DataAccess.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _db;
        public IDepartmentRepository Department { get; private set; }

        public UnitWork(ApplicationDbContext db)
        {
            _db = db;
            Department = new DepartmentRepository(_db); // Inicializamos
            

        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
