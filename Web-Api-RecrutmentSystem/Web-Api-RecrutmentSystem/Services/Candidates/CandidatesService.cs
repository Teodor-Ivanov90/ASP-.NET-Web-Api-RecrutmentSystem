using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Services.Candidates
{
    public class CandidatesService : ICandidatesService
    {
        private readonly RecrutmentSystemDbContext data;

        public CandidatesService(RecrutmentSystemDbContext data) 
            => this.data = data;

        public ICollection<CandidateRequestModel> Get()
            => this.data.Candidates
            .Select(c => new CandidateRequestModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                BirthDate = c.BirthDate,
                Bio = c.Bio,
                Recruiter = new RecruiterRequestModel
                {
                    LastName = c.Recruiter.LastName,
                    Email = c.Recruiter.Email,
                    Country = c.Recruiter.Country
                },
                Skills = c.Skills.Select(s => new SkillRequestModel { Name = s.Name }).ToList()
            })
               .ToList();

        public CandidateRequestModel GetById(int id)
            => this.data.Candidates
                .Where(c => c.Id == id)
                .Select(c => new CandidateRequestModel
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    BirthDate = c.BirthDate,
                    Bio = c.Bio,
                    Recruiter = new RecruiterRequestModel
                    {
                        LastName = c.Recruiter.LastName,
                        Email = c.Recruiter.Email,
                        Country = c.Recruiter.Country
                    },
                    Skills = c.Skills.Select(s => new SkillRequestModel { Name = s.Name }).ToList()
                })
                .FirstOrDefault();

        public void SaveToDb(CandidateRequestModel candidate, Recruiter recruiter, ICollection<Skill> skills)
        {
            var candidateToDb = new Candidate
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                Bio = candidate.Bio,
                BirthDate = candidate.BirthDate,
                Skills = skills,
                Recruiter = recruiter
            };

            this.data.Candidates.Add(candidateToDb);
            this.data.SaveChanges();
        }

        public Candidate AlreadyExist(CandidateRequestModel candidate)
            => this.data
            .Candidates
            .FirstOrDefault(c => c.Email == candidate.Email);

        
        public ICollection<Skill> Skills(CandidateRequestModel candidate)
        {
            var skills = new List<Skill>();

            foreach (var skill in candidate.Skills)
            {
                var isSkillInDb = this.data
                    .Skills.Any(s => s.Name == skill.Name);

                if (!isSkillInDb)
                {
                    var skillToDb = new Skill { Name = skill.Name };
                    skills.Add(skillToDb);
                }
                else
                {
                    var skillFromDb = this.data.Skills.FirstOrDefault(s => s.Name == skill.Name);
                    skills.Add(skillFromDb);
                }
            }

            return skills;
        }

        public void Delete(int id)
        {
           var candidate = this.data.Candidates.Where(c => c.Id == id).FirstOrDefault();
            if (candidate != null)
            {
                this.data.Candidates.Remove(candidate);
            }
        }
    }
}
