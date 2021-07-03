using Dapper;
using System.Data;

namespace Vrnz2.Infra.Repository.Extensions
{
    public static class DapperDynamicParameter
    {
        public static DynamicParameters AddParameter(this DynamicParameters parameters, string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null) 
        {
            parameters.Add(name, value, dbType, direction, size, precision, scale);

            return parameters;
        }
    }
}
