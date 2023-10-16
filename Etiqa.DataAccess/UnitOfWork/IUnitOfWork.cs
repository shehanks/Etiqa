using Etiqa.Repository.Contract;

namespace Etiqa.DataAccess
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        #region Properties
        IUserRepository UserRepository { get; }
        IUserSkillRepository UserSkillRepository { get; }
        #endregion

        Task CompleteAsync();
    }
}
