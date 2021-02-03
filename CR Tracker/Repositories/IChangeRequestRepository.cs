using CR_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Repositories
{
    public interface IChangeRequestRepository
    {
        Task<ChangeRequest> GetCRById(string crId);
        Task<IEnumerable<ChangeRequest>> GetChangeRequests(bool includeOnHold);
        Task<IEnumerable<ChangeRequest>> GetChangeRequestsByUser(int userId, bool includeOnHold);
        Task<ChangeRequest> AddChangeRequest(ChangeRequest changeRequest);
        Task<ChangeRequest> UpdateChangeRequest(ChangeRequest changeRequest);
        Task<int> DeleteChangeRequest(string crId);
    }
}
