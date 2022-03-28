using Microsoft.EntityFrameworkCore;
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

        public void ChangeInDb(Candidate candidateFromDb ,CandidateRequestModel candidate, Recruiter recruiter, ICollection<Skill> skills)
        {

            candidateFromDb.FirstName = candidate.FirstName;
            candidateFromDb.LastName = candidate.LastName;
            candidateFromDb.Email = candidate.Email;
            candidateFromDb.Bio = candidate.Bio;
            candidateFromDb.BirthDate = candidate.BirthDate;
            candidateFromDb.Skills = skills;
            candidateFromDb.Recruiter = recruiter;

            this.data.SaveChanges();
        }

        public void SaveToDb(CandidateRequestModel candidate, Recruiter recruiter, ICollection<Skill> skills)
        {
           
        }

        public Candidate AlreadyExist(CandidateRequestModel candidate)
            => this.data
            .Candidates
            .Include(c => c.Skills)
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


        public ICollection<Skill> ChangeSkills(CandidateRequestModel candidate)
        {
            var skills = new List<Skill>();

            var candidateFromDB = this.AlreadyExist(candidate);
            candidateFromDB.Skills.Clear();

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

            this.data.SaveChanges();
        }

    }
}
