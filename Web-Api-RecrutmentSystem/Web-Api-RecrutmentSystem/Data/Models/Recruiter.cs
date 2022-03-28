using System.Collections.Generic;

namespace RecrutmentSystem.Data.Models
{
    public class Recruiter
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public int Experience { get; set; } = 1;
        public int FreeSlots { get; set; } = 5;
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
