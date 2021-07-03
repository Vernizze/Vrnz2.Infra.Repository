using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Vrnz2.Infra.Repository.Abstract;

namespace Vrnz2.Infra.Repository.Interfaces.Base
{
    public interface IBaseRepository
    {
        void Init(IDbConnection sqlConnection);
        void Init(IDbTransaction sqlTransaction);

        string TableName { get; }

        Task<bool> InsertAsync<T>(T value) where T : BaseDataObject;
        IEnumerable<T> Get<T>() where T : BaseDataObject;
        T GetById<T>(string Id) where T : BaseDataObject;
        T GetByRefCode<T>(int RefCode) where T : BaseDataObject;
        bool DeleteAll();
    }
}
