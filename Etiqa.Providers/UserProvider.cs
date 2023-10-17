using AutoMapper;
using Etiqa.DataAccess;
using Etiqa.Domain.ClientModels;
using Etiqa.Domain.RequestModels;
using Etiqa.Providers.Contracts;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public UserProvider(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<User> AddUserAsync(dm.User user)
        {
            await unitOfWork.UserRepository.InsertAsync(user);
            if (user.UserSkills.Any())
                await unitOfWork.UserSkillRepository.InsertRangeAsync(user.UserSkills);

            await unitOfWork.CompleteAsync();
            return mapper.Map<User>(user);
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
            return mapper.Map<User>(user);
        }

        public async Task<IEnumerable<User>> GetUsers(UserListLoadOptions loadOptions)
        {
            var users = await unitOfWork.UserRepository.GetAsync(
                filter: string.IsNullOrEmpty(loadOptions.email) ?
                    null :
                    x => !string.IsNullOrEmpty(x.Email) && x.Email.StartsWith(loadOptions.email),
                orderBy: user => user.OrderBy(u => u.Id),
                skip: loadOptions.page - 1,
                take: loadOptions.pageSize,
                includeProperties: "UserSkills");
            return mapper.Map<IEnumerable<User>>(users);
        }

        public async Task UpdateUserAsync(dm.User user)
        {
            await unitOfWork.UserRepository.UpdateAsync(user);
            await unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (user == null)
                return false;

            if (user.UserSkills.Any())
            {
                foreach (var item in user.UserSkills)
                    await unitOfWork.UserSkillRepository.DeleteAsync(item);
            }

            await unitOfWork.UserRepository.DeleteAsync(user);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}