using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Skills;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Skills
{
    public interface ISkillsService
    {
        ICollection<Skill> PrepareSkills(CandidateModel candidate);
        SkillModel GetById(int id);
        ICollection<SkillModel> GetActive();
    }
}
