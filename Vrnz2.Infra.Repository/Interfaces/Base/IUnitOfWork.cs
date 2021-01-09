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

        void AddRepository<TRepository>()
            where TRepository : Abstract.BaseRepository, IBaseRepository;

        TRepository GetRepository<TRepository>();

        TRepository GetRepository<TRepository>(string table_name)
            where TRepository : class, IBaseRepository;
    }
}
