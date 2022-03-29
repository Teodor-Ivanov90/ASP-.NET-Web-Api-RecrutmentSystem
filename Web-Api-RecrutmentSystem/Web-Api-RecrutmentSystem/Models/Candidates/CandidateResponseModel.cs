using RecrutmentSystem.Models.Skills;
using System;
using System.Collections.Generic;

namespace Web_Api_RecrutmentSystem.Models.Candidates
{
    public class CandidateResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<SkillModel> Skills { get; set; }
    }
}
