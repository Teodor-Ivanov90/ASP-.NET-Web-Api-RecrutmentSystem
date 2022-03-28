using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Candidates
{
    public interface ICandidatesService
    {
        CandidateRequestModel GetById(int id);


        void Delete(int id);

        ICollection<CandidateRequestModel> Get();
        void SaveToDb(CandidateRequestModel candidate,Recruiter recruiter, ICollection<Skill> skills);
        Candidate AlreadyExist(CandidateRequestModel candidate);
        ICollection<Skill> Skills(CandidateRequestModel candidate);
    }
}
