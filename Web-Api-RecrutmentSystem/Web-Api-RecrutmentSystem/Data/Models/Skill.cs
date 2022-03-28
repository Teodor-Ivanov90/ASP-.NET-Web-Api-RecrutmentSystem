using System.Collections.Generic;

namespace RecrutmentSystem.Data.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
