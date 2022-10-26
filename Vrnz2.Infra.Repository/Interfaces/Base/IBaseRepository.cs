using System.Data;
using System.Threading.Tasks;

namespace Vrnz2.Infra.Repository.Interfaces.Base
{
    public interface IBaseRepository
    {
        void Init(IDbConnection sqlConnection);
        void Init(IDbTransaction sqlTransaction);

        string TableName { get; }

        Task<bool> AddAsync<T>(T obj);

        Task<bool> UpdateAsync<T>(T obj);

        Task<bool> DeleteAsync<T>(T obj);
    }
}
