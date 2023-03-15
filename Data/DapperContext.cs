using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.Data
{
    public class DapperContext
    {
        private IConfiguration _config;

        public DapperContext(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("b202"));


    }
}
