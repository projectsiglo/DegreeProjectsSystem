using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<InstitutionType> InstitutionTypes { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<Submodality> Submodalities { get; set; }
        public DbSet<TeachingFunction> TeachingFunctions { get; set; }
        public DbSet<TypePerson> TypePeople { get; set; }

    }
}
