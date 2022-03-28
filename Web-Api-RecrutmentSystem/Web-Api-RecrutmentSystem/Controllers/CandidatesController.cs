﻿using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Data;
using RecrutmentSystem.Data.Models;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Services.Candidates;
using RecrutmentSystem.Services.Interviews;
using RecrutmentSystem.Services.Recruiters;
using System.Collections.Generic;
using System.Linq;

namespace RecrutmentSystem.Controllers
{
    [ApiController]
    //[Route("/candidates")]
    public class CandidatesController : ControllerBase
    {
        private readonly RecrutmentSystemDbContext data;
        private readonly IRecruitersService recruiters;
        private readonly ICandidatesService candidates;
        private readonly IInterviewsService interviews;

        public CandidatesController(RecrutmentSystemDbContext data,
            IRecruitersService recruiters,
            ICandidatesService candidates,
            IInterviewsService interviews)
        {
            this.data = data;
            this.recruiters = recruiters;
            this.candidates = candidates;
            this.interviews = interviews;
        }

        [HttpPost]
        [Route("/candidates")]
        public IActionResult PostCandidates(CandidateRequestModel candidate)
        {
            if (this.candidates.AlreadyExist(candidate) != null)
            {
                return BadRequest();
            }

            var recruiter = this.recruiters.AlreadyExist(candidate);

            var skills = this.candidates.Skills(candidate);

            this.candidates.SaveToDb(candidate, recruiter, skills);

            return Ok();
        }

        [HttpGet]
        [Route("/candidates")]

        public ICollection<CandidateRequestModel> GetCandidate(int id)
        {
            if (id == 0)
            {
                return this.candidates.Get() ;
            }

            return new[] { this.candidates.GetById(id) };
        }

        [HttpDelete]
        [Route("/candidates")]
        public IActionResult DeleteCandidate([FromQuery] int id)
        {
            var candidate = this.candidates.GetById(id);

            if (candidate == null)
            {
                return BadRequest();
            }

            //var existingCandidate = this.data.Candidates.FirstOrDefault(c => c.Id == id);
            var interview = this.interviews.GetByCandidateID(id);
            if (interview != null)
            {
                this.interviews.Delete(interview.Select(i => i.Id).ToList());
                this.data.SaveChanges();
            }
            
            this.candidates.Delete(id);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("/candidates")]
        public ActionResult<CandidateRequestModel> PutCandidate(CandidateRequestModel candidate, int id)
        {
            var candidateExist = this.candidates.GetById(id);

            if (candidateExist == null)
            {
                return BadRequest();
            }

            var recruiter = this.recruiters.AlreadyExist(candidate);

            var skills = this.candidates.Skills(candidate);

            this.candidates.SaveToDb(candidate, recruiter, skills);

            return Ok();
        }
    }
}