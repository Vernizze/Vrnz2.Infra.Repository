using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

        public virtual async Task<bool> AddAsync<T>(T obj)
        {
            var result = false;

            var properties = GetProperties<T>();

            var sql = $"INSERT INTO {this.TableName}({JoinPropertiesInsert(properties)}) VALUES ({JoinPropertiesInsert(properties, true)});";

            var res = await this.ExecuteAsync(sql, obj);

            result = (res > 0);

            return result;
        }

        public virtual async Task<bool> UpdateAsync<T>(T obj)
        {
            var properties = GetProperties<T>().Where(w => w.Name != "Id");

            var sql = $"UPDATE {this.TableName} SET {JoinPropertiesUpdate(properties)} WHERE Id = @Id;";

            await this.ExecuteAsync(sql, obj);

            return true;
        }

        public virtual async Task<bool> DeleteAsync<T>(T obj)
        {
            var properties = GetProperties<T>().Where(w => w.Name != "Id");

            var sql = $"DELETE FROM {this.TableName} WHERE Id = @Id;";

            var res = await this._dbConnection.ExecuteAsync(sql, transaction: this._dbTransaction);

            return res > 0;
        }

        #region Private Methods

        private IEnumerable<PropertyInfo> GetProperties<T>()
        {
            return typeof(T).GetProperties().Where(w => !w.PropertyType.FullName.Contains("DataObjects") && !w.CustomAttributes.Any(c => c.AttributeType.Name.Contains("NotMapped")) && !w.PropertyType.FullName.Contains("Collection"));
        }

        private string JoinPropertiesInsert(IEnumerable<PropertyInfo> propertyInfo, bool isValues = false)
        {
            var caracter = isValues ? "@" : "";
            return string.Join(",", propertyInfo.Select(s => $"{caracter}{s.Name}"));
        }

        private string JoinPropertiesUpdate(IEnumerable<PropertyInfo> propertyInfo)
        {
            var properties = string.Empty;

            foreach (var item in propertyInfo)
            {
                var str = properties == "" ? "" : ",";
                properties = $"{properties}{str} {item.Name} = @{item.Name}";
            }

            return properties;
        }

        #endregion

        #endregion
    }
}
