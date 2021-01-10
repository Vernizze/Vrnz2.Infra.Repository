using System;
using System.Data;

namespace Vrnz2.Infra.Repository.Interfaces.Base
{
    public interface IUnitOfWork
        : IDisposable
    {
        void OpenConnection();
        void Begin();
        void Commit();
        void Rollback();

        IDbConnection Connection { get; }

        IUnitOfWork AddRepository<IRepository, TRepository>()
            where IRepository : class, IBaseRepository
            where TRepository : Abstract.BaseRepository, IRepository;

        TRepository GetRepository<TRepository>(string table_name)
            where TRepository : class, IBaseRepository;
    }
}
