using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Jobs;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Jobs
{
    public interface IJobsService
    {
        ICollection<JobModel> GetJobsBySkill(string skill);

        ICollection<Skill> PrepareSkills(JobModel job);

        bool AlreadyExist(JobModel job);
        bool AlreadyExist(int id);

        void AddToDb(JobModel job, ICollection<Skill> skills);
        Job GetFromDB(JobModel job);
        Job GetByID(int id);

        void Delete(int id);
    }
}
