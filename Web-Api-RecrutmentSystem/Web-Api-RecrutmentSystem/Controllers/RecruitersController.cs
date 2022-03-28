using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Services.Recruiters;
using System.Collections.Generic;

namespace RecrutmentSystem.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class RecruitersController : ControllerBase
    {
        private readonly IRecruitersService recruiters;

        public RecruitersController(IRecruitersService recruiters) 
            => this.recruiters = recruiters;

        [HttpGet]
        [Route("/[controller]")]
        public ICollection<RecruiterRequestModel> Get(int level)
        {
            if (level == 0)
            {
                return this.recruiters.RecruitersWithAvailableCandidats();
            }

            return this.recruiters.RecruiterByExperience(level);
            
        } 

        
    }
}
