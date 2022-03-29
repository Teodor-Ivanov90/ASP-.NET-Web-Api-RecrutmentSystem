using Microsoft.EntityFrameworkCore;
using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Interviews;
using RecrutmentSystem.Models.Jobs;
using RecrutmentSystem.Models.Skills;
using RecrutmentSystem.Services.Recruiters;
using System.Collections.Generic;
using System.Linq;
using Web_Api_RecrutmentSystem.Models.Candidates;

namespace RecrutmentSystem.Services.Interviews
{
    public class InterviewsService : IInterviewsService
    {
        private readonly RecrutmentSystemDbContext data;
        private readonly IRecruitersService recruiters;

        public InterviewsService(RecrutmentSystemDbContext data, IRecruitersService recruiters)
        {
            this.data = data;
            this.recruiters = recruiters;
        }

        public void CreateInterviews(ICollection<Candidate> candidates, Job job)
        {
            foreach (var candidate in candidates)
            {
                var candidateRecruiter = this.recruiters.GetById(candidate.RecruiterId);
                candidateRecruiter.Experience++;
                candidateRecruiter.FreeSlots--;

                var interview = new Interview
                {
                    Candidate = candidate,
                    Job = job
                };

                this.data.Interviews.Add(interview);
                this.data.SaveChanges();
            }

        }

        public void Delete(ICollection<int> ids)
        {
            var interviews = this.data.Interviews
                .Include(i => i.Candidate)
                .ThenInclude(c => c.Recruiter)
                 .Where(i => ids.Contains(i.Id))
                 .ToList();

            foreach (var interview in interviews)
            {
                var recruiter = this.recruiters.GetById(interview.Candidate.RecruiterId);
                recruiter.FreeSlots++;
            }

            this.data.Interviews.RemoveRange(interviews);
            this.data.SaveChanges();
        }

        public ICollection<InterviewResponseModel> Get()
        {
            var interviews = this.data.Interviews
                .Include(i => i.Job)
                .Include(i => i.Candidate)
                .ThenInclude(c => c.Skills)
                .Select(i => new InterviewResponseModel
                {
                    Job = new JobModel
                    {
                        Title = i.Job.Title,
                        Description = i.Job.Description,
                        Salary = i.Job.Salary,
                        Skills = i.Job.Skills.Select(s => new SkillModel { Name = s.Name}).ToList()
                    },
                    Candidate = new CandidateResponseModel
                    {
                        FirstName = i.Candidate.FirstName,
                        LastName = i.Candidate.LastName,
                        Email = i.Candidate.Email,
                        BirthDate = i.Candidate.BirthDate,
                        Bio = i.Candidate.Bio,
                        Skills = i.Candidate.Skills.Select(s => new SkillModel { Name = s.Name}).ToList()
                        
                    }
                })
                .ToList();

            return interviews;
        }

        public ICollection<Interview> GetByCandidateID(int id)
            => this.data
                .Interviews
                .Where(i => i.Candidate.Id == id)
                .ToList();

        public ICollection<Interview> GetByJobID(int id)
            => this.data
                .Interviews
                .Where(i => i.Job.Id == id)
                .ToList();

        public ICollection<Candidate> GetSuitableCandidates(ICollection<Skill> skills)
            => this.data.Candidates
                .Where(c => c.Skills.Any(s => skills.Contains(s)) && c.Recruiter.FreeSlots > 0) // A.Any(x => B.Contains(x))
                .ToList();

    }
}
