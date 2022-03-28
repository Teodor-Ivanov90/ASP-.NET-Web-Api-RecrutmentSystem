using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Jobs;

namespace RecrutmentSystem.Models.Interviews
{
    public class InterviewResponseModel
    {
        public int Id { get; set; }

        public JobRequestModel Job { get; set; }

        public CandidateRequestModel Candidate { get; set; }
    }
}
