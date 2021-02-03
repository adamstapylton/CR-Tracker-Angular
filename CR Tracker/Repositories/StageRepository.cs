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
    public class StageRepository : IStageRepository
    {
        private readonly IConfiguration configuration;

        public StageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string ConnectionString { get; set; }


        public async Task<Stage> GetStageById(int id)
        {
            var stages = await GetStages();
            return stages.Where(s => s.StageId == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Stage>> GetStages()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var query = "SELECT * FROM Stages;";
                    return await conn.QueryAsync<Stage>(query);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
