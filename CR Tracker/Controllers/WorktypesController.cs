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
    public class WorktypesController : ControllerBase
    {
        private readonly IWorktypeRepository worktypeRepository;

        public WorktypesController(IWorktypeRepository worktypeRepository)
        {
            this.worktypeRepository = worktypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var worktypes = await worktypeRepository.GetWorktypesAsync();

                return Ok(worktypes.OrderBy(wt => wt.Name));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
