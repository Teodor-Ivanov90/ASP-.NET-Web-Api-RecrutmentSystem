using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Models.Jobs;
using RecrutmentSystem.Services.Interviews;
using RecrutmentSystem.Services.Jobs;


namespace RecrutmentSystem.Controllers
{
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobsService jobs;
        private readonly IInterviewsService interviews;

        public JobsController(IJobsService jobs,
            IInterviewsService interviews)
        {
            this.jobs = jobs;
            this.interviews = interviews;
        } 

        [HttpPost]
        [Route("[controller]")]
        public IActionResult Post(JobModel job)
        {
            if (this.jobs.AlreadyExist(job))
            {
                return BadRequest();
            }

            var skills = this.jobs.PrepareSkills(job);

            this.jobs.AddToDb(job, skills);

            var jobFromDb = this.jobs.GetFromDB(job);

            var candidates = this.interviews.GetSuitableCandidates(skills);

            this.interviews.CreateInterviews(candidates, jobFromDb);

            return Ok();
        }

        [HttpGet]
        [Route("[controller]")]
        public ICollection<JobModel> GetBySkill(string skill)
            => this.jobs.GetJobsBySkill(skill);

        [HttpDelete]
        [Route("[controller]")]
        public ActionResult<ICollection<JobModel>> Delete(int id)
        {
            if (!this.jobs.AlreadyExist(id))
            {
                return BadRequest();
            }

            var job = this.jobs.GetByID(id);

            var interviews = this.interviews.GetByJobID(id);

            if(interviews != null)
            {
                this.interviews.Delete(interviews.Select(i => i.Id).ToList());
            }

            this.jobs.Delete(id);

            return Ok();
        }
    }
}
