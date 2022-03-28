using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
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

        public Recruiter AlreadyExist(CandidateRequestModel candidate)
        {
            var recruiter = this.data
               .Recruiters
               .Where(r => r.LastName == candidate.Recruiter.LastName
                   && r.Email == candidate.Recruiter.Email
                   && r.Country == candidate.Recruiter.Country)
               .FirstOrDefault();

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

        public ICollection<RecruiterRequestModel> RecruiterByExperience(int level)
            => this.data.Recruiters
                .Where(r => r.Experience == level)
                .Select(r => new RecruiterRequestModel
                {
                    LastName = r.LastName,
                    Country = r.Country,
                    Email = r.Email
                })
                .ToList();

        public ICollection<RecruiterRequestModel> RecruitersWithAvailableCandidats()
            => this.data.Recruiters
                .Where(r => r.Candidates.Any())
                .Select(r => new RecruiterRequestModel
                {
                    LastName = r.LastName,
                    Country = r.Country,
                    Email = r.Email
                })
                .ToList();
        
    }
}
