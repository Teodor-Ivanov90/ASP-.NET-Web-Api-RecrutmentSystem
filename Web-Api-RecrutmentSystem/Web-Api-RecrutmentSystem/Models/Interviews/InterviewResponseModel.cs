using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Models.Jobs;
using Web_Api_RecrutmentSystem.Models.Candidates;

namespace RecrutmentSystem.Models.Interviews
{
    public class InterviewResponseModel
    {
        public JobModel Job { get; set; }

        public CandidateResponseModel Candidate { get; set; }
    }
}
