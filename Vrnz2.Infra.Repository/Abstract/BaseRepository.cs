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

        private IDbTransaction _dbTransaction = null;
        private IDbConnection _dbConnection;

        #endregion  

        #region Attributes

        public string TableName { protected set; get; }

        #endregion

        #region Methods

        public virtual void Init(IDbConnection dbConnection)
            => _dbConnection = dbConnection;

        public virtual void Init(IDbTransaction dbTransaction)
        {
            this._dbTransaction = dbTransaction;

            this._dbConnection = this._dbTransaction.Connection;
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

        protected int Execute(string query, object parms = null)
        {
            if (this._dbTransaction.IsNull())
                return _dbConnection.Execute(query, parms);
            else
                return _dbConnection.Execute(query, parms, this._dbTransaction);
        }

        protected async Task<object> ExecuteScalarAsync(string query, object parms = null)
        {
            if (this._dbTransaction.IsNull())
                return await _dbConnection.ExecuteScalarAsync(query, parms);
            else
                return await _dbConnection.ExecuteScalarAsync(query, parms, this._dbTransaction);
        }

        protected async Task<int> ExecuteAsync(string query, object parms = null)
        {
            if (this._dbTransaction.IsNull())
                return await _dbConnection.ExecuteAsync(query, parms);
            else
                return await _dbConnection.ExecuteAsync(query, parms, this._dbTransaction);
        }

        #endregion
    }
}
