using System.Collections.Generic;
using System.Data;

namespace Vrnz2.Infra.Repository.Interfaces.Base
{
    public interface IBaseRepository
    {
        void Init(IDbConnection sqlConnection);
        void Init(IDbTransaction sqlTransaction);

        string TableName { get; }
    }
}
