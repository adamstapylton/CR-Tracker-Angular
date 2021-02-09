using CR_Tracker.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CR_Tracker.Repositories
{
    public class ChangeRequestRepository : IChangeRequestRepository
    {
        private readonly IConfiguration configuration;

        public ChangeRequestRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string ConnectionString { get; set; }

        public async Task<ChangeRequest> AddChangeRequest(ChangeRequest changeRequest)
        {
            try
            {
                string query;
                object parameters;

                if (changeRequest.DateRequired == DateTime.MinValue)
                {
                    parameters = new
                    {
                        ChangeRequestId = changeRequest.ChangeRequestId,
                        Description = changeRequest.Description,
                        //AssignedToUser = changeRequest.AssignedToUser,
                        RaisedByUserId = changeRequest.RaisedByUser.UserId,
                        BillingRules = changeRequest.BillingRulesRequired,
                        OnHold = changeRequest.OnHold,
                        StageId = 1

                    };

                    query = $@"INSERT INTO ChangeRequests (ChangeRequestId, Description, RaisedByUserId, BillingRulesRequired, OnHold, StageId)
                        VALUES (@ChangeRequestId, @Description, @RaisedByUserId, @BillingRules, @OnHold, @StageId);";

                }
                else
                {
                    parameters = new
                    {
                        ChangeRequestId = changeRequest.ChangeRequestId,
                        Description = changeRequest.Description,
                        DateRequired = changeRequest.DateRequired,
                        //AssignedToUser = changeRequest.AssignedToUser,
                        RaisedByUserId = changeRequest.RaisedByUser.UserId,
                        BillingRules = changeRequest.BillingRulesRequired,
                        OnHold = changeRequest.OnHold,
                        StageId = 1

                    };

                    query = $@"INSERT INTO ChangeRequests (ChangeRequestId, Description, DateRequired, RaisedByUserId, BillingRulesRequired, OnHold, StageId)
                        VALUES (@ChangeRequestId, @Description, @DateRequired, @RaisedByUserId, @BillingRules, @OnHold, @StageId);";
                }


                


                using (var conn = new SqlConnection(ConnectionString))
                {
                    
                    await conn.ExecuteAsync(query, parameters);

                    foreach (var worktype in changeRequest.Worktypes)
                    {
                        var worktypeParameters = new
                        {
                            ChangeRequestId = changeRequest.ChangeRequestId,
                            WorktypeId = worktype.WorktypeId
                        };

                        var worktypeQuery = @"INSERT INTO ChangeRequestsWorktypes (ChangeRequestId, WorktypeId)
                                VALUES(@ChangeRequestId, @WorktypeId); ";

                        await conn.ExecuteAsync(worktypeQuery, worktypeParameters);
 
                    }

                    return changeRequest;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> DeleteChangeRequest(string crId)
        {
            try
            {
                var parameters = new { ChangeRequestId = crId };

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var query = @"DELETE FROM ChangeRequestsWorktypes WHERE ChangeRequestId = @ChangeRequestId;
                        DELETE FROM ChangeRequests WHERE ChangeRequestId = @ChangeRequestId;";

                    return await conn.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ChangeRequest>> GetChangeRequests(bool includeOnHold)
        {
            try
            {
                var changeRequests = new List<ChangeRequest>();
                string query;

                using (var conn = new SqlConnection(ConnectionString))
                {

                    if (includeOnHold)
                    {
                        query = @"SELECT ChangeRequestId, Description,  DateRequired, AssignedToUserId, RaisedByUserId, BillingRulesRequired, OnHold, Stages.StageId, StageName, StageOrder, 
								RaisedByUser.UserId AS RaisedByUserId, RaisedByUser.UserId ,RaisedByUser.Initials, RaisedByUser.FirstName, RaisedByUser.Surname, RaisedByUser.Email
                                FROM ChangeRequests
                                INNER JOIN Stages ON ChangeRequests.StageId = Stages.StageId
								INNER JOIN Users AS RaisedByUser ON RaisedByUserId = RaisedByUser.UserId;";
                    }
                    else
                    {
                        query = @"SELECT ChangeRequestId, Description,  DateRequired, AssignedToUserId, RaisedByUserId, BillingRulesRequired, OnHold, Stages.StageId, StageName, StageOrder, 
								RaisedByUser.UserId AS RaisedByUserId, RaisedByUser.UserId ,RaisedByUser.Initials, RaisedByUser.FirstName, RaisedByUser.Surname, RaisedByUser.Email
                                FROM ChangeRequests
                                INNER JOIN Stages ON ChangeRequests.StageId = Stages.StageId
								INNER JOIN Users AS RaisedByUser ON RaisedByUserId = RaisedByUser.UserId
                                WHERE OnHold IS NULL OR OnHold = 0;";
                    }
                    

                    var results = await conn.QueryAsync<ChangeRequest, Stage, User, ChangeRequest>(query,
                        (cr, stage, raisedUser) => {
                            cr.Stage = stage;
                            cr.RaisedByUser = raisedUser;
                            return cr;
                        },
                        splitOn: "StageId, RaisedByUserId");

                    changeRequests = results.ToList();

                    foreach (var cr in changeRequests)
                    {
                        var parameters = new { ChangeRequestId = cr.ChangeRequestId };

                        var worktypeQuery = @"SELECT Worktypes.WorktypeId, Worktypes.Name
                            FROM ChangeRequestsWorktypes
                            INNER JOIN Worktypes ON ChangeRequestsWorktypes.WorktypeId = Worktypes.WorktypeId
                            WHERE ChangeRequestId = @ChangeRequestId;";

                        cr.Worktypes = await conn.QueryAsync<Worktype>(worktypeQuery, parameters);

                        var assignedToQuery = @"SELECT UserId, Initials, FirstName, Surname, Email
                            FROM ChangeRequests
                            INNER JOIN Users On ChangeRequests.AssignedToUserId = UserId
                            WHERE ChangeRequestId = @ChangeRequestId";

                        cr.AssignedToUser = await conn.QuerySingleOrDefaultAsync<User>(assignedToQuery, parameters);
                    }

                }

                return changeRequests;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ChangeRequest>> GetChangeRequestsByUser(int userId, bool includeOnHold)
        {
            try
            {
                var changeRequests = await GetChangeRequests(includeOnHold);

                return changeRequests.Where(cr => cr.AssignedToUser.UserId == userId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ChangeRequest> GetCRById(string crId)
        {
            try
            {
                var changeRequests = await GetChangeRequests(true);

                 return changeRequests.Where(cr => cr.ChangeRequestId == crId).FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ChangeRequest> UpdateChangeRequest(ChangeRequest changeRequest)
        {
            try
            {
                var parameters = new
                {
                    ChangeRequestId = changeRequest.ChangeRequestId,
                    Description = changeRequest.Description,
                    DateRequired = changeRequest.DateRequired.ToString("s"),
                    AssignedTo = changeRequest.AssignedToUser.UserId,
                    RaisedBy = changeRequest.RaisedByUser.UserId,
                    BillingRules = changeRequest.BillingRulesRequired,
                    OnHold = changeRequest.OnHold,
                    StageId = changeRequest.Stage.StageId
                };

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var query = @"UPDATE ChangeRequests SET Description = @Description, 
							DateRequired = @DateRequired, 
							AssignedToUserId = @AssignedTo, 
							RaisedByUserId = @RaisedBy, 
							BillingRulesRequired = @BillingRules, 
							OnHold = @OnHold, 
							StageId = @StageId
                            WHERE ChangeRequestId = @ChangeRequestId;";

                    await conn.ExecuteAsync(query, parameters);
                }

                return await GetCRById(changeRequest.ChangeRequestId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateChangeRequestStage(string crId, int stageId)
        {
            try
            {
                var parameters = new
                {
                    ChangeRequestId = crId,
                    StageId = stageId
                };

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var query = @"UPDATE ChangeRequests SET StageId = @StageId WHERE ChangeRequestId = @ChangeRequestId";

                    return await conn.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
