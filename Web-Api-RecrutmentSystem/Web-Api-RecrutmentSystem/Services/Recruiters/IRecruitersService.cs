using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Recruiters;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Recruiters
{
    public interface IRecruitersService
    {
        Recruiter GetById(int id);
        Recruiter GetByEmail(string email);
        Recruiter Create(CandidateModel candidate);
        Recruiter Change(CandidateModel candidate);
        ICollection<RecruiterModel> WithAvailableCandidats();
        ICollection<RecruiterModel> RecruiterByExperience(int level);
    }
}
