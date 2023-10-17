using ErrorOr;
using Etiqa.Domain.ClientModels;
using Etiqa.Domain.RequestModels;
using cm = Etiqa.Domain.ClientModels;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Services.Contract
{
    public interface IUserService : IServiceBase<dm.User, UserListLoadOptions>
    {
        Task<cm.User> AddUserAsync(dm.User user);

        Task<ErrorOr<cm.User>> GetUserAsync(int id);

        Task<ErrorOr<UserList>> GetUsers(UserListLoadOptions loadOptions);

        Task UpdateUserAsync(dm.User user);

        Task<ErrorOr<bool>> DeleteUserAsync(int id);
    }
}