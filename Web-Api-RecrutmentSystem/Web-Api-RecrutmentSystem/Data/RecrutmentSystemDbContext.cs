using Microsoft.EntityFrameworkCore;
using RecrutmentSystem.Data.Models;

namespace RecrutmentSystem.Data
{
    public class RecrutmentSystemDbContext : DbContext
    {
        public RecrutmentSystemDbContext()
        {
        }

        public RecrutmentSystemDbContext(DbContextOptions dbContextOptionsBuilder)
            :base(dbContextOptionsBuilder)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Interview> Interviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
