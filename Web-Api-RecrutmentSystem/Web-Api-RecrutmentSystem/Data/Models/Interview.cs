namespace RecrutmentSystem.Data.Models
{
    public class Interview
    {
        public int Id { get; set; }

        public Job Job { get; set; }

        public Candidate Candidate { get; set; }
    }
}
