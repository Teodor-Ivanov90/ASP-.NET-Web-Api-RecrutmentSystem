using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Models.Candidates;
using RecrutmentSystem.Services.Skills;
using System.Collections.Generic;

namespace RecrutmentSystem.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService skills;

        public SkillsController(ISkillsService skills)
            => this.skills = skills;

        [HttpGet]
        public ICollection<SkillRequestModel> Get(int id)
        {
            if (id != 0)
            {
                return  new[] { this.skills.GetById(id) };
            }

            return this.skills.Get();
        } 

        [HttpGet]
        [Route("/[controller]/active")]
        public ICollection<SkillRequestModel> GetActiveSkills() 
            => this.skills.GetActive();
    }
}
