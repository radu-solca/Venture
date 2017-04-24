using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Venture.Users.Data
{
    public class UserStore : IUserStore
    {
        private const string connectionString = 
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Users; Integrated Security=True";

        private const string findUserSql =
            @"select * from Users user where user.Id = @UserId";

        public async Task<User> Get(int userId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var users = (await conn.QueryAsync<User>(findUserSql, new {userId})).ToList();

                if (!users.Any())
                {
                    return null;
                }

                return users.FirstOrDefault();
            }
        }

    }
}
