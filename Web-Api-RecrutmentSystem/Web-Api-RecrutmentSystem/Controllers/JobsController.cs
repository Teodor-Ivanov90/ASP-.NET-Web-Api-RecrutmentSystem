using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Jobs;
using RecrutmentSystem.Services.Interviews;
using RecrutmentSystem.Services.Jobs;
using RecrutmentSystem.Services.Recruiters;
using System.Collections.Generic;

namespace RecrutmentSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobsService jobs;
        private readonly IInterviewsService interviews;
        private readonly IRecruitersService recruiters;

        public JobsController(IJobsService jobs,
            IInterviewsService interviews,
            IRecruitersService recruiters)
        {
            this.jobs = jobs;
            this.interviews = interviews;
            this.recruiters = recruiters;
        } 

        [HttpPost]
        public IActionResult PostJob(JobRequestModel job)
        {
            if (this.jobs.AlreadyExist(job))
            {
                return BadRequest();
            }

            var skills = this.jobs.Skills(job);

             var jobFromDb =this.jobs.AddToDb(job, skills);

            var candidates = this.interviews.GetSuitableCandidates(skills);

            this.interviews.CreateInterviews(candidates, jobFromDb);

            return Ok();
        }

        [HttpGet]
        public ICollection<JobRequestModel> GetJobsBySkill(string skill)
            => this.jobs.GetJobsBySkill(skill);
    }
}
