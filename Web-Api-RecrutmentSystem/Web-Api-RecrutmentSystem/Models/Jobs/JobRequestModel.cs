using RecrutmentSystem.Models.Candidates;
using System.Collections.Generic;

namespace RecrutmentSystem.Models.Jobs
{
    public class JobRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }

        public ICollection<SkillRequestModel> Skills { get; set; }
    }
}
