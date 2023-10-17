using ErrorOr;
using Etiqa.Domain.ClientModels;
using Etiqa.Domain.RequestModels;
using cm = Etiqa.Domain.ClientModels;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Services.Contract
{
    public interface IUserService : IServiceBase<dm.User, UserListLoadOptions>
    {
        Task<ErrorOr<cm.User>> AddUserAsync(dm.User user);

        Task<ErrorOr<cm.User>> GetUserAsync(int id);

        Task<ErrorOr<UserList>> GetUsersAsync(UserListLoadOptions loadOptions);

        Task<ErrorOr<bool>> UpdateUserAsync(dm.User user);

        Task<ErrorOr<bool>> DeleteUserAsync(int id);
    }
}