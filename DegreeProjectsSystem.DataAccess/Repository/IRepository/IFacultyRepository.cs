using DegreeProjectsSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IFacultyRepository : IRepository<Faculty>
    {
        void Update(Faculty faculty);
    }
}