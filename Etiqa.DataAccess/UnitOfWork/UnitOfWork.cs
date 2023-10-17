using Etiqa.Domain.Context;
using Etiqa.Repository;
using Etiqa.Repository.Contract;

namespace Etiqa.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly EtiqaDbContext etiqaDbContext;

        private IUserRepository? userRepository;
        public IUserRepository UserRepository => userRepository ?? (userRepository = new UserRepository(etiqaDbContext));

        private IUserSkillRepository? userSkillRepository;
        public IUserSkillRepository UserSkillRepository => userSkillRepository ?? (userSkillRepository = new UserSkillRepository(etiqaDbContext));

        public UnitOfWork(EtiqaDbContext dbContext)
        {
            etiqaDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CompleteAsync() => await etiqaDbContext.SaveChangesAsync();

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    await etiqaDbContext.DisposeAsync();
            }
            disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}
