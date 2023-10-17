using Etiqa.Domain.Context;
using Etiqa.Domain.DataModels;
using Etiqa.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace Etiqa.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EtiqaDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await etiqaDbContext.User.AsNoTracking()
                .Include(us => us.UserSkills)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserAsync(string? userName, string? email)
        {
            return await etiqaDbContext.User.AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    (!string.IsNullOrEmpty(x.Email) && x.Email.Equals(email)) ||
                    (!string.IsNullOrEmpty(x.Username) && x.Username.Equals(userName)));
        }
    }
}
