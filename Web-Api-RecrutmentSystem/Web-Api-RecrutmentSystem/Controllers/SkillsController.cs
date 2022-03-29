using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Models.Skills;
using RecrutmentSystem.Services.Skills;

namespace RecrutmentSystem.Controllers
{
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService skills;

        public SkillsController(ISkillsService skills)
            => this.skills = skills;

        [HttpGet]
        [Route("/[controller]")]
        public SkillModel Get(int id)
        {
            return this.skills.GetById(id);
        } 

        [HttpGet]
        [Route("/[controller]/active")]
        public ICollection<SkillModel> GetActiveSkills() 
            => this.skills.GetActive();
    }
}
