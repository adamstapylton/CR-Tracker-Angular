using CR_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Repositories
{
    public class MockChangeRequestRepository : IChangeRequestRepository
    {
        public MockChangeRequestRepository()
        {
            Stages = new List<Stage>()
            {
                new Stage() {StageId = 1, StageName = "Research", StageOrder = 1},
                new Stage() {StageId = 2, StageName = "In Spec", StageOrder = 2},
                new Stage() {StageId = 3, StageName = "In Build", StageOrder = 3},
                new Stage() {StageId = 4, StageName = "In QA", StageOrder = 4},
                new Stage() {StageId = 5, StageName = "In UAT", StageOrder = 5},
                new Stage() {StageId = 6, StageName = "To Upgrade", StageOrder = 6},
                new Stage() {StageId = 7, StageName = "To Release", StageOrder = 7},
                new Stage() {StageId = 8, StageName = "Upgrdaded", StageOrder = 8},
            };

            Worktypes = new List<Worktype>()
            {
                new Worktype(){WorktypeId =  1, Name = "Remortgage"},
                new Worktype(){WorktypeId =  2, Name = "Purchase E-Case"},
                new Worktype(){WorktypeId =  3, Name = "Sales E-Case"},
                new Worktype(){WorktypeId =  4, Name = "Lender Only"},
                new Worktype(){WorktypeId =  5, Name = "Panel Purhcase"},
                new Worktype(){WorktypeId =  6, Name = "Panel Sales"},
                new Worktype(){WorktypeId =  7, Name = "Panel Remortgage"},
                new Worktype(){WorktypeId =  8, Name = "Sep Rep Remortgage"},
                new Worktype(){WorktypeId =  9, Name = "Commercial Purchase"},
                new Worktype(){WorktypeId =  10, Name = "Commercial Re-Finance"},
                new Worktype(){WorktypeId =  11, Name = "ILA Only"},
            };

            Users = new List<User>()
            {
                new User(){UserId = 1, Initials = "ADST", FirstName = "Adam", Surname = "Stapylton"},
                new User(){UserId = 2, Initials = "TEST", FirstName = "Test", Surname = "Lad"},
                new User(){UserId = 3, Initials = "PETE", FirstName = "Peter", Surname = "Smith"},
            };

            ChangeRequests = new List<ChangeRequest>()
            {
                new ChangeRequest()
                {
                    ChangeRequestId = "PSG5555",
                    Description = "A New Bug to sort out",
                    //DateRequired = null,
                    AssignedToUser = Users.Where(u => u.UserId == 1).FirstOrDefault(),
                    RaisedByUser = Users.Where(u => u.UserId == 2).FirstOrDefault(),
                    Worktypes = Worktypes.Where(wt => wt.WorktypeId == 1),
                    BillingRulesRequired = false,
                    OnHold = false,
                    Stage = Stages.Where(s => s.StageId == 2).FirstOrDefault(),
                    
                }
            };
        }

        public List<ChangeRequest> ChangeRequests { get; set; }
        public List<Stage> Stages { get; set; }
        public List<User> Users { get; set; }
        public List<Worktype> Worktypes { get; set; }

        public async Task<ChangeRequest> AddChangeRequest(ChangeRequest changeRequest)
        {
            ChangeRequests.Add(changeRequest);
            return changeRequest;
        }

        public async Task<int> DeleteChangeRequest(string crId)
        {
            var changeRequest = await GetCRById(crId);
            if (ChangeRequests.Remove(changeRequest))
            {
                return 1;
            }

            return 0;
            
        }

        public async Task<IEnumerable<ChangeRequest>> GetChangeRequests(bool includeOnHold)
        {
            if (includeOnHold)
            {
                return ChangeRequests;
            }
            else
            {
                return ChangeRequests.Where(cr => cr.OnHold != true).ToList();
            }
            
        }

        public async Task<IEnumerable<ChangeRequest>> GetChangeRequestsByUser(int userId, bool includeOnHold)
        {
            var changeRequests = await GetChangeRequests(includeOnHold);
            return changeRequests.Where(cr => cr.AssignedToUser.UserId == userId).ToList();
        }

        public async Task<ChangeRequest> GetCRById(string crId)
        {
            return ChangeRequests.Where(cr => cr.ChangeRequestId == crId).FirstOrDefault();
        }

        public async Task<ChangeRequest> UpdateChangeRequest(ChangeRequest changeRequest)
        {
            var changeRequestToUpdate = ChangeRequests.Where(cr => cr.ChangeRequestId == changeRequest.ChangeRequestId).FirstOrDefault();
            changeRequestToUpdate = changeRequest;
            return changeRequest;
        }
    }
}
