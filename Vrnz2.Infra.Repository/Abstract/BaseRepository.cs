using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Vrnz2.Infra.CrossCutting.Extensions;
using Vrnz2.Infra.Repository.Interfaces.Base;

namespace Vrnz2.Infra.Repository.Abstract
{
    public abstract class BaseRepository
        : IBaseRepository
    {
        #region Variables

        protected IDbTransaction _dbTransaction = null;
        protected IDbConnection _dbConnection;

        #endregion  

        #region Attributes

        public string TableName { protected set; get; }

        #endregion

        #region Methods

        public abstract bool Insert<T>(T value) where T : BaseDataObject;

        public virtual void Init(IDbConnection dbConnection)
            => _dbConnection = dbConnection;

        public virtual void Init(IDbTransaction dbTransaction)
        {
            this._dbTransaction = dbTransaction;

            this._dbConnection = this._dbTransaction.Connection;
        }

        public virtual IEnumerable<T> Get<T>()
            where T : BaseDataObject
        {
            var sql = $"SELECT * FROM {TableName} WHERE Deleted = 0;";

            return this.Query<T>(sql);
        }

        public virtual T GetById<T>(string Id)
            where T : BaseDataObject
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id;";

            return this.QueryFirstOrDefault<T>(sql, new { Id });
        }

        public virtual T GetByRefCode<T>(int RefCode)
            where T : BaseDataObject
        {
            var sql = $"SELECT * FROM {TableName} WHERE RefCode = @RefCode;";

            return this.QueryFirstOrDefault<T>(sql, new { RefCode });
        }

        public virtual int GetTableLines()
            => GetTableLines(TableName);


        public virtual bool DeleteAll()
        {
            var sql = $"DELETE FROM {TableName};";

            var res = this._dbConnection.Execute(sql, transaction: this._dbTransaction);

            return res > 0;
        }

        protected virtual int GetTableLines(string table_name)
        {
            var sql = $"SELECT dbo.fn_GetTableLines('{table_name}') AS Count";

            return this._dbConnection.QueryFirstOrDefault<int>(sql, new { table_name }, this._dbTransaction);
        }

        protected T QueryFirstOrDefault<T>(string query, object parms = null)
        {
            if (this._dbTransaction.IsNull())
                return _dbConnection.QueryFirstOrDefault<T>(query, parms);
            else
                return _dbConnection.QueryFirstOrDefault<T>(query, parms, this._dbTransaction);
        }

        protected async Task<T> QueryFirstOrDefaultAsync<T>(string query, object parms = null)
        {
            if (this._dbTransaction.IsNull())
                return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, parms);
            else
                return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, parms, this._dbTransaction);
        }

        protected IEnumerable<T> Query<T>(string query, object parms = null)
        {
            if (this._dbTransaction.IsNull())
                return _dbConnection.Query<T>(query, parms);
            else
                return _dbConnection.Query<T>(query, parms, this._dbTransaction);
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string query, object parms = null)
        {
            if (this._dbTransaction.IsNull())
                return await _dbConnection.QueryAsync<T>(query, parms);
            else
                return await _dbConnection.QueryAsync<T>(query, parms, this._dbTransaction);
        }

        #endregion
    }
}
