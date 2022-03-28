using System;
using System.Collections.Generic;

namespace RecrutmentSystem.Models.Candidates
{
    public class CandidateRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<SkillRequestModel> Skills { get; set; } 
        public RecruiterRequestModel Recruiter { get; set; }
    }
}
