using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecrutmentSystem.Models.Interviews;
using RecrutmentSystem.Services.Interviews;

namespace Web_Api_RecrutmentSystem.Controllers
{
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewsService interviews;

        public InterviewsController(IInterviewsService interviews) 
            => this.interviews = interviews;

        [Route("[controller]")]
        [HttpGet]
        public ICollection<InterviewResponseModel> Get()
            => this.interviews.Get();
    }
}
