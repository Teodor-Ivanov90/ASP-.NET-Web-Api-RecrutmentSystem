using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Recruiters
{
    public interface IRecruitersService
    {
        Recruiter GetById(int id);
        Recruiter AlreadyExist(CandidateRequestModel candidate);
        ICollection<RecruiterRequestModel> RecruitersWithAvailableCandidats();
        ICollection<RecruiterRequestModel> RecruiterByExperience(int level);
    }
}
