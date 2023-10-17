using AutoMapper;
using ErrorOr;
using Etiqa.Domain.ClientModels;
using Etiqa.Domain.RequestModels;
using Etiqa.Providers.Contracts;
using Etiqa.Services.Contract;
using Etiqa.Services.ServiceErrors;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Services.Services
{
    public class UserService : IUserService
    {
        private const string cacheKeyJoiner = "_";
        private const string cacheKeyPrefix = "user";
        private const string cacheKeyListPrefix = "userList";

        private readonly IMapper mapper;

        private readonly IUserProvider userProvider;

        private readonly ICacheService cacheService;

        public UserService(
            IMapper mapper,
            IUserProvider userProvider,
            ICacheService cacheService)
        {
            this.mapper = mapper;
            this.userProvider = userProvider;
            this.cacheService = cacheService;
        }

        public async Task<User> AddUserAsync(dm.User user)
        {
            return await userProvider.AddUserAsync(user);
        }

        public async Task<ErrorOr<User>> GetUserAsync(int id)
        {
            var cacheKey = MakeCacheKey(id);
            var cacheUser = cacheService.GetData<User>(cacheKey);

            if (cacheUser != null)
                return cacheUser;

            var user = await userProvider.GetUserAsync(id);
            if (user == null)
                return Errors.User.NotFound;

            cacheService.SetData(cacheKey, user);
            cacheService.RemoveByPrefix(cacheKeyListPrefix);
            return user;
        }

        public async Task<ErrorOr<UserList>> GetUsers(UserListLoadOptions loadOptions)
        {
            var cacheKey = MakeListCacheKey(loadOptions);
            var cacheUsers = cacheService.GetData<UserList>(cacheKey);

            if (cacheUsers != null)
                return cacheUsers;

            if (loadOptions.page <= 0 || loadOptions.pageSize <= 0)
                return Errors.User.Validation;

            var users = await userProvider.GetUsers(loadOptions);
            var userList = mapper.Map<UserList>(users);
            cacheService.SetData(cacheKey, userList);
            return userList;
        }

        public async Task UpdateUserAsync(dm.User user)
        {
            await userProvider.UpdateUserAsync(user);
            cacheService.RemoveData(MakeCacheKey(MakeCacheKey(user.Id)));
            cacheService.RemoveByPrefix(cacheKeyListPrefix);
        }

        public async Task<ErrorOr<bool>> DeleteUserAsync(int id)
        {
            cacheService.RemoveData(MakeCacheKey(id));
            var deleted = await userProvider.DeleteUserAsync(id);
            if (!deleted)
                return Errors.User.NotFound;

            cacheService.RemoveByPrefix(cacheKeyListPrefix);
            return true;
        }

        public string MakeCacheKey(object id) => string.Join(
            cacheKeyJoiner,
            cacheKeyPrefix,
            id);

        public string MakeCacheKey(dm.User entity) => string.Join(
            cacheKeyJoiner,
            cacheKeyPrefix,
            entity.Id,
            entity.Username,
            entity.Email);

        public string MakeListCacheKey(UserListLoadOptions loadOptions) => string.Join(
            cacheKeyJoiner,
            cacheKeyListPrefix,
            loadOptions.page,
            loadOptions.pageSize,
            loadOptions.email);
    }
}