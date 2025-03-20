using Dapper;
using Database.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace Database.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly string _connectionString;

        public UserRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> CreateAsync(ApplicationUser user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                user.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [ApplicationUser] ([UserName], [Email], [PasswordHash], [FavoriteColor], [Role])
                    VALUES (@{nameof(ApplicationUser.UserName)}, @{nameof(ApplicationUser.Email)}, @{nameof(ApplicationUser.PasswordHash)},
                    @{nameof(ApplicationUser.FavoriteColor)}, @{nameof(ApplicationUser.Role)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return true;
        }

        public async Task<ApplicationUser> FindByNameAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * FROM [ApplicationUser]
                    WHERE [Email] = @{nameof(email)}", new { email });
            }
        }
    }
}
