using DegreeProjectsSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        void Update(Department department);
    }
}
