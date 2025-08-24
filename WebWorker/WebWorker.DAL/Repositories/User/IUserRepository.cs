using WebWorker.Data.Entities.Identity;

namespace WebWorker.DAL.Repositories.User
{
    public interface IUserRepository
    {
        IQueryable<UserEntity> GetAll();
    }
}
