using Microsoft.EntityFrameworkCore;
using SitrepAPI.Entities;

namespace SitrepAPI.DbContexts
{
    /// <summary>
    /// Database context class
    /// </summary>
    public class SitrepDbContext : DbContext
    {
        /// <summary>
        /// Cases 
        /// </summary>
        public virtual DbSet<Case> Cases { get; set; }
        /// <summary>
        /// CaseImages
        /// </summary>
        public virtual DbSet<CaseImage> CaseImages { get; set; }
        /// <summary>
        /// CaseLogs
        /// </summary>
        public virtual DbSet<CaseLog> CaseLogs { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public virtual DbSet<Status> Status { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        public virtual DbSet<Priority> Priority { get; set; }

        /// <summary>
        /// constructor of class
        /// </summary>
        /// <param name="options"></param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SitrepDbContext(DbContextOptions<SitrepDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        /// <summary>
        /// Definitions to create database upon
        /// </summary>
        /// <param name="modelBuilder">Modelbilder factory</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Default Status values
            ICollection<Status> statusClasses = new List<Status>
            {
                new Status() { StatusId = 1, Name = "Oprettet" },
                new Status() { StatusId = 2, Name = "Godkendt" },
                new Status() { StatusId = 3, Name = "Igangværende" },
                new Status() { StatusId = 4, Name = "Afsluttet" },
                new Status() { StatusId = 5, Name = "Afvist" },
                new Status() { StatusId = 6, Name = "Slettet" }
            };

            modelBuilder.Entity<Status>().HasData(statusClasses);
            // Default Priority values

            ICollection<Priority> priorities = new List<Priority>
            {
                new Priority() { PriorityId = 1, Name = "Unset" },
                new Priority() { PriorityId = 2, Name = "Lav" },
                new Priority() { PriorityId = 3, Name = "Mellem" },
                new Priority() { PriorityId = 4, Name = "Høj" }
            };

            modelBuilder.Entity<Priority>().HasData(priorities);

            //seed data for Case
            //removed due to relations issues 

        }
    }
}
