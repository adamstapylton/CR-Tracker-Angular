using CR_Tracker.Models;
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
    public class ChangeRequestsController : ControllerBase
    {
        private readonly IChangeRequestRepository changeRequestRepository;

        public ChangeRequestsController(IChangeRequestRepository changeRequestRepository)
        {
            this.changeRequestRepository = changeRequestRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool includeOnHold = false)
        {
            try
            {
                var changeRequests = await changeRequestRepository.GetChangeRequests(includeOnHold);

                return Ok(changeRequests);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{changeRequestId}")]
        public async Task<IActionResult> Get(string changeRequestId)
        {
            try
            {
                var changeRequest = await changeRequestRepository.GetCRById(changeRequestId);

                if (changeRequest != null)
                {
                    return Ok(changeRequest);
                }

                return BadRequest("Change Request not found");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ChangeRequest changeRequest)
        {
            try
            {
                if (changeRequest == null)
                {
                    return BadRequest("No Change Request sent through");
                }

                if (await changeRequestRepository.GetCRById(changeRequest.ChangeRequestId) != null)
                {
                    return BadRequest("Change Request with this Id already exists");
                }

                var addedCr = await changeRequestRepository.AddChangeRequest(changeRequest);

                if(addedCr != null)
                {
                    return Ok(addedCr);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{changeRequestId}")]
        public async Task<IActionResult> Delete(string changeRequestId)
        {
            try
            {
                var changeRequest = await changeRequestRepository.GetCRById(changeRequestId);

                if (changeRequest == null)
                {
                    return BadRequest("Change Request not found, unable to delete");
                }

                if (await changeRequestRepository.DeleteChangeRequest(changeRequestId) > 0)
                {
                    return Ok("Change Request Deleted");
                }

                return BadRequest("Unable to delete change request");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{changeRequestId}")]
        public async Task<IActionResult> Update(string changeRequestId, ChangeRequest changeRequest)
        {
            try
            {
                if (changeRequestId != changeRequest.ChangeRequestId)
                {
                    return BadRequest("Unable to update Change Request Id");
                }

                var crToUpdate = await changeRequestRepository.GetCRById(changeRequestId);

                if (crToUpdate == null)
                {
                    return BadRequest("Change Request not found, unable to update");
                }

                var updatedCr = await changeRequestRepository.UpdateChangeRequest(changeRequest);

                if (updatedCr != null)
                {
                    return Ok(updatedCr);
                }

                return BadRequest("Unable to update Change request");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
