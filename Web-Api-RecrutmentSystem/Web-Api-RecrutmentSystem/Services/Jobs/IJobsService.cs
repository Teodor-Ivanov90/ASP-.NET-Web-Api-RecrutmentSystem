using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Jobs;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Jobs
{
    public interface IJobsService
    {
        ICollection<JobRequestModel> GetJobsBySkill(string skill);

        ICollection<Skill> Skills(JobRequestModel job);

        bool AlreadyExist(JobRequestModel job);

        Job AddToDb(JobRequestModel job, ICollection<Skill> skills);
    }
}
