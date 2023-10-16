using ErrorOr;
using cm = Etiqa.Domain.ClientModels;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Services.Contract
{
    public interface IUserService
    {
        Task<cm.User> AddUserAsync(dm.User user);

        Task<ErrorOr<cm.User>> GetUserAsync(int id);

        Task UpdateUserAsync(dm.User user);

        Task<ErrorOr<bool>> DeleteUserAsync(int id);
    }
}