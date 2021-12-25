using System.Threading;
using System.Threading.Tasks;

namespace SvwDesign.UserManagement
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
