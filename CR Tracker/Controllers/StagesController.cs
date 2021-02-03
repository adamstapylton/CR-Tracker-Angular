using CR_Tracker.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
    {
        private readonly IStageRepository stageRepository;

        public StagesController(IStageRepository stageRepository)
        {
            this.stageRepository = stageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var stages = await stageRepository.GetStages();

                return Ok(stages);
     
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
