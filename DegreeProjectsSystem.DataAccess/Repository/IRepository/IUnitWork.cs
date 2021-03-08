﻿using System;

namespace DegreeProjectsSystem.DataAccess.Repository.IRepository
{
    public interface IUnitWork : IDisposable
    {
        IDepartmentRepository Department{ get; }
        IFacultyRepository Faculty { get; }
        IEducationLevelRepository EducationLevel { get; }
        ITypePersonRepository TypePerson { get; }
        void Save();
    }
}
