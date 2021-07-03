using System.Threading.Tasks;
using Vrnz2.Infra.Repository.Abstract;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public class UserRepository
        : BaseRepository, IUserRepository
    {
        public UserRepository()
            => TableName = nameof(User);

        public override Task<bool> InsertAsync<TEntity>(TEntity value)
            => Task.FromResult(true);

        public User GetByLogin(string login)
            => QueryFirstOrDefault<User>("SELECT * FROM User WHERE Login = @login;", new { login });
    }
}
