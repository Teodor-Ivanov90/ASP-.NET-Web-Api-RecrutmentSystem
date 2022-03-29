using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Services.Candidates;
using RecrutmentSystem.Services.Interviews;
using RecrutmentSystem.Services.Recruiters;
using RecrutmentSystem.Services.Skills;

namespace RecrutmentSystem.Controllers
{
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IRecruitersService recruiters;
        private readonly ICandidatesService candidates;
        private readonly IInterviewsService interviews;
        private readonly ISkillsService skills;

        public CandidatesController(
            IRecruitersService recruiters,
            ICandidatesService candidates,
            IInterviewsService interviews,
            ISkillsService skills)
        {
            this.recruiters = recruiters;
            this.candidates = candidates;
            this.interviews = interviews;
            this.skills = skills;
        }

        [HttpPost]
        [Route("/candidates")]
        public IActionResult Post(CandidateModel candidate)
        {
            if (this.candidates.AlreadyExist(candidate))
            {
                return BadRequest();
            }

            var recruiter = this.recruiters.Create(candidate);

            var skills = this.skills.PrepareSkills(candidate);

            this.candidates.SaveToDb(candidate, recruiter, skills);

            return Ok();
        }

        [HttpGet]
        [Route("/candidates")]

        public CandidateModel Get(int id)
        {
            return this.candidates.GetById(id);
        }
        [HttpPut]
        [Route("/candidates")]
        public ActionResult<CandidateModel> Put(CandidateModel candidate, int id)
        {
            var candidateFromDb = this.candidates.GetFromDB(id);

            if (candidateFromDb == null)
            {
                return BadRequest();
            }

            var recruiter = this.recruiters.Change(candidate);

            var skills = this.candidates.ChangeSkills(candidate, id);

            this.candidates.ChangeInDb(candidateFromDb, candidate, recruiter, skills);

            return Ok();
        }


        [HttpDelete]
        [Route("/candidates")]
        public IActionResult Delete(int id)
        {
            var candidate = this.candidates.GetById(id);

            if (candidate == null)
            {
                return BadRequest();
            }

            var interviews = this.interviews.GetByCandidateID(id);

            if (interviews != null)
            {
                this.interviews.Delete(interviews.Select(i => i.Id).ToList());
            }
            
            this.candidates.Delete(id);

            return Ok();
        }

    }
}
