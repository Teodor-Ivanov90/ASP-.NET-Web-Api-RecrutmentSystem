using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Candidates
{
    public interface ICandidatesService
    {
        CandidateModel GetById(int id);
        Candidate GetFromDB(int id);
        void Delete(int id);
        ICollection<CandidateModel> Get();
        void SaveToDb(CandidateModel candidate,Recruiter recruiter, ICollection<Skill> skills);
        void ChangeInDb(Candidate candidateFromDb, CandidateModel candidate, Recruiter recruiter, ICollection<Skill> skills);
        bool AlreadyExist(CandidateModel candidate);

        
        ICollection<Skill> ChangeSkills(CandidateModel candidate, int candidateID);
    }
}
