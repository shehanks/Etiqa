using Etiqa.Domain.DataModels;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Repository.Contract
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        // Add method definitions of repository specific methods if required.
        // Override base repository methods if required.
        Task<dm.User?> GetUserByIdAsync(int id);
    }
}
