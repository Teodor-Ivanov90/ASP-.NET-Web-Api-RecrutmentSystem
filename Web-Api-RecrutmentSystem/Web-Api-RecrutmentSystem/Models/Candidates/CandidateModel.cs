using RecrutmentSystem.Models.Recruiters;
using RecrutmentSystem.Models.Skills;
using System;
using System.Collections.Generic;

namespace RecrutmentSystem.Models.Candidates
{
    public class CandidateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<SkillModel> Skills { get; set; } 
        public RecruiterModel Recruiter { get; set; }
    }
}
