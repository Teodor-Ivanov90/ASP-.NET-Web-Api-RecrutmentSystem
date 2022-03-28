using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Jobs;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Services.Jobs
{
    public class JobsService : IJobsService
    {
        private readonly RecrutmentSystemDbContext data;

        public JobsService(RecrutmentSystemDbContext data)
            => this.data = data;

        public ICollection<JobRequestModel> GetJobsBySkill(string skillName)
            => this.data.Jobs
                .Where(j => j.Skills.Any(s => s.Name == skillName))
                .Select(j => new JobRequestModel { 
                Title = j.Title,
                Description = j.Description,
                Salary = j.Salary,
                Skills = j.Skills.Select(s => new SkillRequestModel { Name = s.Name}).ToList()
                })
                .ToList();
        

        public Job AddToDb(JobRequestModel job, ICollection<Skill> skills)
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

            return JobToDb;
        }

        public bool AlreadyExist(JobRequestModel job)
            => this.data.Jobs
            .Any(j => j.Title == job.Title);

        public ICollection<Skill> Skills(JobRequestModel job)
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
    }
}
