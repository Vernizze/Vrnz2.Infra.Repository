using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Vrnz2.Infra.Repository.Abstract;
using Vrnz2.Infra.Repository.Settings;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public class UnitOfWork
        : BaseUnitOfWork
    {
        private IRepository _repository;

        #region Constructors

        public UnitOfWork(ConnectionStrings connectionStrings, IRepository repository)
        {
            var connectionString = connectionStrings.ConnectionsStrings.SingleOrDefault(s => "MyDatabase".Equals(s.Name));

            _connection = new SqlConnection(connectionString.Value);
            _repositories.Add(repository.TableName, repository);
        }

        #endregion

        public override void InitReps(IDbConnection dbConnection)
            => _repositories.ToList().ForEach(r => r.Value.Init(dbConnection));

        protected override void InitReps(IDbTransaction dbTransaction)
            => _repositories.ToList().ForEach(r => r.Value.Init(dbTransaction));
    }
}
