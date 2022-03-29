using Microsoft.EntityFrameworkCore;
using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Recruiters;
using RecrutmentSystem.Models.Skills;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Services.Candidates
{
    public class CandidatesService : ICandidatesService
    {
        private readonly RecrutmentSystemDbContext data;

        public CandidatesService(RecrutmentSystemDbContext data) 
            => this.data = data;

        public ICollection<CandidateModel> Get()
            => this.data.Candidates
            .Select(c => new CandidateModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                BirthDate = c.BirthDate,
                Bio = c.Bio,
                Recruiter = new RecruiterModel
                {
                    LastName = c.Recruiter.LastName,
                    Email = c.Recruiter.Email,
                    Country = c.Recruiter.Country
                },
                Skills = c.Skills.Select(s => new SkillModel { Name = s.Name }).ToList()
            })
               .ToList();

        public CandidateModel GetById(int id)
            => this.data.Candidates
                .Where(c => c.Id == id)
                .Select(c => new CandidateModel
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    BirthDate = c.BirthDate,
                    Bio = c.Bio,
                    Recruiter = new RecruiterModel
                    {
                        LastName = c.Recruiter.LastName,
                        Email = c.Recruiter.Email,
                        Country = c.Recruiter.Country
                    },
                    Skills = c.Skills.Select(s => new SkillModel { Name = s.Name }).ToList()
                })
                .FirstOrDefault();

        public void ChangeInDb(Candidate candidateFromDb ,CandidateModel candidate, Recruiter recruiter, ICollection<Skill> skills)
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

        public void SaveToDb(CandidateModel candidate, Recruiter recruiter, ICollection<Skill> skills)
        {
            var candidateForDB = new Candidate
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                BirthDate = candidate.BirthDate,
                Bio = candidate.Bio,
                Recruiter = recruiter,
                Skills = skills
            };

            this.data.Candidates.Add(candidateForDB);
            this.data.SaveChanges();
        }

        public bool AlreadyExist(CandidateModel candidate)
            => this.data
            .Candidates
            .Any(c => c.Email == candidate.Email);

        public ICollection<Skill> ChangeSkills(CandidateModel candidate, int candidateId)
        {
            var skills = new List<Skill>();

            var candidateFromDB = this.GetFromDB(candidateId);
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

        public Candidate GetFromDB(int id)
            => this.data.Candidates
            .Include(c => c.Skills)
            .FirstOrDefault(c => c.Id == id);
    }
}
