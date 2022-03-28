using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Services.Recruiters;
using System.Collections.Generic;
using System.Linq;

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
            var interview = this.data.Interviews
                 .Where(i => ids.Contains(i.Id))
                 .ToList();

            this.data.Interviews.RemoveRange(interview);
        }

        public ICollection<Interview> GetByCandidateID(int id)
        {
            return this.data
                .Interviews
                .Where(i => i.Candidate.Id == id)
                .ToList();
        }

        public ICollection<Candidate> GetSuitableCandidates(ICollection<Skill> skills)
            => this.data.Candidates
                .Where(c => c.Skills.Any(s => skills.Contains(s)) && c.Recruiter.FreeSlots > 0) // A.Any(x => B.Contains(x))
                .ToList();

    }
}
