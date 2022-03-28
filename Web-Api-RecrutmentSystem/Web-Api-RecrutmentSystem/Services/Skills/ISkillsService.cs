using RecrutmentSystem.Models.Candidates;
using System.Collections.Generic;

namespace RecrutmentSystem.Services.Skills
{
    public interface ISkillsService
    {
        SkillRequestModel GetById(int id);
        ICollection<SkillRequestModel> Get();

        ICollection<SkillRequestModel> GetActive();
    }
}
