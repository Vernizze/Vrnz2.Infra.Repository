using Dapper;
using Vrnz2.Infra.Repository.Abstract;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public class Repository
        : BaseRepository, IRepository
    {
        public Repository()
            => TableName = nameof(User);

        public override bool Insert<Filial>(Filial value)
        => true;

        public User GetByLogin(string login)
            => QueryFirstOrDefault<User>("SELECT * FROM User WHERE Login = @login;", new { login });
    }
}
