using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Skills;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Services.Skills
{
    public class SkillsService : ISkillsService
    {
        private readonly RecrutmentSystemDbContext data;

        public SkillsService(RecrutmentSystemDbContext data) 
            => this.data = data;

        public ICollection<SkillModel> GetActive() 
            => this.data.Skills
                .Where(s => s.Candidates.Any())
                .Select(s => new SkillModel { Name = s.Name }).ToList();

        public SkillModel GetById(int id)
        {
            var skill = this.data.Skills.FirstOrDefault(s => s.Id == id);
            if (skill == null)
            {
                return null;
            }
            var skillToReturn = new SkillModel { Name = skill.Name };

            return skillToReturn;
        }

        public ICollection<Skill> PrepareSkills(CandidateModel candidate)
        {
            var skills = new List<Skill>();

            foreach (var skill in candidate.Skills)
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
