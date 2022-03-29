using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Jobs;
using RecrutmentSystem.Models.Skills;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Services.Jobs
{
    public class JobsService : IJobsService
    {
        private readonly RecrutmentSystemDbContext data;

        public JobsService(RecrutmentSystemDbContext data)
            => this.data = data;

        public ICollection<JobModel> GetJobsBySkill(string skillName)
            => this.data.Jobs
                .Where(j => j.Skills.Any(s => s.Name == skillName))
                .Select(j => new JobModel { 
                Title = j.Title,
                Description = j.Description,
                Salary = j.Salary,
                Skills = j.Skills.Select(s => new SkillModel { Name = s.Name}).ToList()
                })
                .ToList();
        

        public void AddToDb(JobModel job, ICollection<Skill> skills)
        {
            var JobToDb = new Job
            {
                Title = job.Title,
                Description = job.Description,
                Salary = job.Salary,
                Skills = skills
            };

            this.data.Jobs.Add(JobToDb);
            this.data.SaveChanges();

        }

        public bool AlreadyExist(JobModel job)
            => this.data.Jobs
            .Any(j => j.Title == job.Title);

        public ICollection<Skill> PrepareSkills(JobModel job)
        {
            var skills = new List<Skill>();

            foreach (var skill in job.Skills)
            {
                var isSkillInDb = this.data
                    .Skills.Any(s => s.Name == skill.Name);

                if (!isSkillInDb)
                {
                    var skillToDb = new Skill { Name = skill.Name };
                    skills.Add(skillToDb);
                }
                else
                {
                    var skillFromDb = this.data.Skills.FirstOrDefault(s => s.Name == skill.Name);
                    skills.Add(skillFromDb);
                }
            }

            return skills;
        }

        public Job GetFromDB(JobModel job)
            => this.data.Jobs
            .FirstOrDefault(j => j.Title == job.Title);

        public Job GetByID(int id)
            => this.data.Jobs
            .FirstOrDefault(j => j.Id == id);

        public bool AlreadyExist(int id)
        => this.data.Jobs
            .Any(j => j.Id == id);

        public void Delete(int id)
        {
            var job = this.data.Jobs.Where(j => j.Id == id).FirstOrDefault();
            if (job != null)
            {
                this.data.Jobs.Remove(job);
            }

            this.data.SaveChanges();
        }
    }
}
