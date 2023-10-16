using Etiqa.Domain.Context;
using Etiqa.Domain.DataModels;
using Etiqa.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using dm = Etiqa.Domain.DataModels;

namespace Etiqa.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EtiqaDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<dm.User?> GetUserByIdAsync(int id)
        {
            return await etiqaDbContext.User.AsNoTracking()
                .Include(us => us.UserSkills)
                .FirstOrDefaultAsync(x => x.Id == (int)id);
        }
    }
}
