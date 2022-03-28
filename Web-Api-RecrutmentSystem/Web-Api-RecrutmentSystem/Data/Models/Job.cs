using System.Collections.Generic;

namespace RecrutmentSystem.Data.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }
}
