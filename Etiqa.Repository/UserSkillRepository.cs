using Etiqa.Domain.Context;
using Etiqa.Domain.DataModels;
using Etiqa.Repository.Contract;

namespace Etiqa.Repository
{
    public class UserSkillRepository : RepositoryBase<UserSkill>, IUserSkillRepository
    {
        public UserSkillRepository(EtiqaDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
