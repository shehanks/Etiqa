using Etiqa.Domain.DataModels;

namespace Etiqa.Repository.Contract
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        // Add method definitions of repository specific methods if required.
        // Override base repository methods if required.
        Task<User?> GetUserByIdAsync(int id);

        Task<User?> GetUserAsync(string? userName, string? email);
    }
}
