﻿using DegreeProjectsSystem.Models;
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

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<CareerPerson> CareerPeople { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentFaculty> DepartmentFaculties { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<InstitutionContactCharge> InstitutionContactCharges { get; set; }
        public DbSet<IdentityDocumentType> IdentityDocumentTypes { get; set; }
        public DbSet<InstitutionType> InstitutionTypes { get; set; }
        public DbSet<Modality> Modalities { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonTypePerson> PersonTypePeople { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<Recognition> Recognitions { get; set; }
        public DbSet<Solicitude> Solicitudes { get; set; }
        public DbSet<Submodality> Submodalities { get; set; }
        public DbSet<TeachingFunction> TeachingFunctions { get; set; }
        public DbSet<TypePerson> TypePeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Career>()
                .HasIndex(ca => ca.Name)
                .IsUnique();

            modelBuilder.Entity<CareerPerson>()
                .HasIndex(cpe => new { cpe.CareerId, cpe.PersonId })
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(c => new { c.Name, c.DepartmentId })
                .IsUnique();

            modelBuilder.Entity<DepartmentFaculty>()
                .HasIndex(df => df.Name)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<EducationLevel>()
                .HasIndex(el => el.Name)
                .IsUnique();

            modelBuilder.Entity<Faculty>()
               .HasIndex(f => f.Name)
               .IsUnique();

            modelBuilder.Entity<Gender>()
               .HasIndex(g => g.Name)
               .IsUnique();

            modelBuilder.Entity<Institution>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<InstitutionContactCharge>()
                .HasIndex(icc => icc.Name)
                .IsUnique();

            modelBuilder.Entity<IdentityDocumentType>()
                .HasIndex(idt => idt.Name)
                .IsUnique();

            modelBuilder.Entity<InstitutionType>()
                .HasIndex(it => it.Name)
                .IsUnique();

            modelBuilder.Entity<Modality>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<Person>()
               .HasIndex(pe => pe.IdentificationNumber)
               .IsUnique();

            modelBuilder.Entity<PersonTypePerson>()
               .HasIndex(ptp => new { ptp.PersonId, ptp.TypePersonId })
               .IsUnique();

            modelBuilder.Entity<ProgramType>()
               .HasIndex(pt => new { pt.Name })
               .IsUnique();

            modelBuilder.Entity<Recognition>()
               .HasIndex(re => new { re.Name })
               .IsUnique();

            modelBuilder.Entity<Solicitude>()
               .HasIndex(s => new { s.ActNumber })
               .IsUnique();

            modelBuilder.Entity<Submodality>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<TeachingFunction>()
                .HasIndex(tf => tf.Name)
                .IsUnique();

            modelBuilder.Entity<TypePerson>()
               .HasIndex(tp => tp.Name)
               .IsUnique();
        }

    }
}
