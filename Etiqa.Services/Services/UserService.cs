using AutoMapper;
using ErrorOr;
using Etiqa.DataAccess;
using Etiqa.Domain.ClientModels;
using Etiqa.Services.Contract;
using Etiqa.Services.ServiceErrors;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task<ErrorOr<User>> GetUserAsync(int id)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (user == null)
                return Errors.User.NotFound;

            return mapper.Map<User>(user);
        }

        public async Task UpdateUserAsync(dm.User user)
        {
            await unitOfWork.UserRepository.UpdateAsync(user);
            await unitOfWork.CompleteAsync();
        }

        public async Task<ErrorOr<bool>> DeleteUserAsync(int id)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
            if (user == null)
                return Errors.User.NotFound;

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