using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Recruiters;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Services.Recruiters
{
    public class RecruitersService : IRecruitersService
    {
        private readonly RecrutmentSystemDbContext data;

        public RecruitersService(RecrutmentSystemDbContext data) 
            => this.data = data;

        public Recruiter GetById(int id)
            => this.data.Recruiters
                .FirstOrDefault(r => r.Id == id);

        public Recruiter GetByEmail(string email)
            => this.data
               .Recruiters
               .Where(r => r.Email == email)
               .FirstOrDefault();

        public Recruiter Create(CandidateModel candidate)
        {
            var recruiter = this.GetByEmail(candidate.Email);

            if (recruiter != null)
            {
                recruiter.Experience++;
            }
            else
            {
                recruiter = new Recruiter
                {
                    LastName = candidate.Recruiter.LastName,
                    Email = candidate.Recruiter.Email,
                    Country = candidate.Recruiter.Country
                };
            }

            return recruiter;
        }
        public Recruiter Change(CandidateModel candidate)
        {
            var recruiter = this.GetByEmail(candidate.Recruiter.Email);

            if (recruiter != null)
            {
                recruiter.LastName = candidate.Recruiter.LastName ;
                recruiter.Country = candidate.Recruiter.Country;

                this.data.SaveChanges();
            }
            else
            {
                recruiter = new Recruiter
                {
                    LastName = candidate.Recruiter.LastName,
                    Email = candidate.Recruiter.Email,
                    Country = candidate.Recruiter.Country
                };
            }

            return recruiter;
        }

        public ICollection<RecruiterModel> RecruiterByExperience(int level)
            => this.data.Recruiters
                .Where(r => r.Experience == level)
                .Select(r => new RecruiterModel
                {
                    LastName = r.LastName,
                    Country = r.Country,
                    Email = r.Email
                })
                .ToList();

        public ICollection<RecruiterModel> WithAvailableCandidats()
            => this.data.Recruiters
                .Where(r => r.Candidates.Any())
                .Select(r => new RecruiterModel
                {
                    LastName = r.LastName,
                    Country = r.Country,
                    Email = r.Email
                })
                .ToList();

    }
}
