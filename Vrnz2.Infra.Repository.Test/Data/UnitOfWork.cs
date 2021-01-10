using Microsoft.Data.Sqlite;
using System.Data;
using System.Linq;
using Vrnz2.Infra.Repository.Abstract;
using Vrnz2.Infra.Repository.Settings;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public class UnitOfWork
        : BaseUnitOfWork
    {
        #region Constructors

        public UnitOfWork(ConnectionStrings connectionStrings)
        {
            var connectionString = connectionStrings.ConnectionsStrings.SingleOrDefault(s => "AuthDb".Equals(s.Name));

            _connection = new SqliteConnection(connectionString.Value);

            AddRepository<IUserRepository, UserRepository>().
            AddRepository<IPersonRepository, PersonRepository>();
        }

        #endregion

        public override void InitReps(IDbConnection dbConnection)
            => _repositories.ToList().ForEach(r => r.Value.Init(dbConnection));

        protected override void InitReps(IDbTransaction dbTransaction)
            => _repositories.ToList().ForEach(r => r.Value.Init(dbTransaction));
    }
}
