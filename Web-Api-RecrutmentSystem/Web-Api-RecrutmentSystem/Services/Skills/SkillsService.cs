using RecrutmentSystem.Data;
using RecrutmentSystem.Models.Candidates;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Services.Skills
{
    public class SkillsService : ISkillsService
    {
        private readonly RecrutmentSystemDbContext data;

        public SkillsService(RecrutmentSystemDbContext data) 
            => this.data = data;

        public ICollection<SkillRequestModel> Get()
            => this.data.Skills
                .Select(s => new SkillRequestModel { Name = s.Name })
                .ToList();

        public ICollection<SkillRequestModel> GetActive() 
            => this.data.Skills
                .Where(s => s.Candidates.Any())
                .Select(s => new SkillRequestModel { Name = s.Name }).ToList();

        public SkillRequestModel GetById(int id)
        {
            var skill = this.data.Skills.FirstOrDefault(s => s.Id == id);
            if (skill == null)
            {
                return null;
            }
            var skillToReturn = new SkillRequestModel { Name = skill.Name };

            return skillToReturn;
        }
        
    }
}
