using RecrutmentSystem.Models.Skills;
using System.Collections.Generic;

namespace RecrutmentSystem.Models.Jobs
{
    public class JobModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }

        public ICollection<SkillModel> Skills { get; set; }
    }
}
