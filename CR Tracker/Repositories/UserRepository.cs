using CR_Tracker.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CR_Tracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private string ConnectionString { get; set; }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    var query = "SELECT * FROM Users";
                    return await conn.QueryAsync<User>(query);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
