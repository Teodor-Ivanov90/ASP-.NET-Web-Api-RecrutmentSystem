using RecrutmentSystem.Data.Models;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Interviews
{
    public interface IInterviewsService
    {
        ICollection<Interview> GetByCandidateID(int id);

        void Delete(ICollection<int> ids);
        ICollection<Candidate> GetSuitableCandidates(ICollection<Skill> skills);

        void CreateInterviews(ICollection<Candidate> candidates, Job job);
    }
}
