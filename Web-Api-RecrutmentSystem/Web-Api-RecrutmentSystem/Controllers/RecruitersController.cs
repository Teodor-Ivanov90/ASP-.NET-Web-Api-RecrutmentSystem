using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Models.Recruiters;
using RecrutmentSystem.Services.Recruiters;

namespace RecrutmentSystem.Controllers
{
    [ApiController]
    public class RecruitersController : ControllerBase
    {
        private readonly IRecruitersService recruiters;

        public RecruitersController(IRecruitersService recruiters) 
            => this.recruiters = recruiters;

        [HttpGet]
        [Route("/[controller]")]
        public ICollection<RecruiterModel> Get(int level)
        {
            if (level == 0)
            {
                return this.recruiters.WithAvailableCandidats();
            }

            return this.recruiters.RecruiterByExperience(level);
            
        } 
    }
}
