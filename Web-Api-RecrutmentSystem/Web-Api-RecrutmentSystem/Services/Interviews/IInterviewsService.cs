using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Interviews;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Interviews
{
    public interface IInterviewsService
    {
        ICollection<InterviewResponseModel> Get();
        ICollection<Interview> GetByCandidateID(int id);
        ICollection<Interview> GetByJobID(int id);
        void Delete(ICollection<int> ids);
        ICollection<Candidate> GetSuitableCandidates(ICollection<Skill> skills);
        void CreateInterviews(ICollection<Candidate> candidates, Job job);
    }
}
