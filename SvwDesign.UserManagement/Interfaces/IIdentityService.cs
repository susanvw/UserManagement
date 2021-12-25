using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace SvwDesign.UserManagement
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string roleName);

        Task<(Result Result, JwtSecurityToken? Token)> LoginAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
