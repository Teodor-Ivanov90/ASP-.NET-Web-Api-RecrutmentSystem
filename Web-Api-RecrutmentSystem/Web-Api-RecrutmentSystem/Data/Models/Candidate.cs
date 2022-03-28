using System;
using System.Collections.Generic;

namespace RecrutmentSystem.Data.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio{ get; set; }
        public DateTime BirthDate { get; set; }
        public int RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
