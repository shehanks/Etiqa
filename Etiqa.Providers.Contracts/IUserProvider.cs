using Etiqa.Domain.RequestModels;
using cm = Etiqa.Domain.ClientModels;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Providers.Contracts
{
    public interface IUserProvider
    {
        Task<cm.User> AddUserAsync(dm.User user);

        Task<cm.User> GetUserAsync(int id);

        Task<IEnumerable<cm.User>> GetUsers(UserListLoadOptions loadOptions);

        Task UpdateUserAsync(dm.User user);

        Task<bool> DeleteUserAsync(int id);
    }
}