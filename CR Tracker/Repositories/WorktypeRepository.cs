using CR_Tracker.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CR_Tracker.Repositories
{
    public class WorktypeRepository : IWorktypeRepository
    {
        private readonly IConfiguration configuration;

        public WorktypeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string ConnectionString { get; set; }

        public async Task<IEnumerable<Worktype>> GetWorktypesAsync()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var query = @"SELECT * FROM Worktypes";

                    return await conn.QueryAsync<Worktype>(query);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
