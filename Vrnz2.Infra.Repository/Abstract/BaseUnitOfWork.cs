using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Vrnz2.Infra.CrossCutting.Extensions;
using Vrnz2.Infra.Repository.Interfaces.Base;

namespace Vrnz2.Infra.Repository.Abstract
{
    public abstract class BaseUnitOfWork
        : IUnitOfWork
    {
        #region Variables

        protected IDbConnection _connection;
        protected IDbTransaction _transaction;

        protected Dictionary<string, IBaseRepository> _repositories = new Dictionary<string, IBaseRepository>();

        protected IServiceCollection _services;

        #endregion

        #region Attributes

        public IDbConnection Connection { get; }

        #endregion

        #region Constructors

        public BaseUnitOfWork() 
        {
            _services = new ServiceCollection();
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (!this._transaction.IsNull())
                this._transaction.Rollback();

            if (!this._connection.IsNull())
                this._connection.Close();
        }

        public IUnitOfWork AddRepository<IRepository, TRepository>()
            where IRepository : class, IBaseRepository
            where TRepository : BaseRepository, IRepository
        {
            var repository = _services
                .AddScoped<IRepository, TRepository>()
                .BuildServiceProvider()
                .GetService<IRepository>();

            _repositories.Add(repository.TableName, repository);

            return this;
        }

        public virtual void InitReps(IDbConnection dbConnection)
            => _repositories.ToList().ForEach(r => r.Value.Init(dbConnection));

        protected virtual void InitReps(IDbTransaction dbTransaction)
            => _repositories.ToList().ForEach(r => r.Value.Init(dbTransaction));

        public TRepository GetRepository<TRepository>(string table_name)
            where TRepository : class, IBaseRepository
        {
            this._repositories.TryGetValue(table_name, out IBaseRepository result);

            return result as TRepository;
        }

        public void OpenConnection()
        {
            this._connection.Open();

            this.InitReps(this._connection);
        }

        public void Begin()
        {
            this._transaction = this._connection.BeginTransaction();

            this.InitReps(this._transaction);
        }

        public void Commit()
        {
            try
            {
                if (this._connection.State.Equals(ConnectionState.Open))
                {
                    try
                    {
                        this._transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        this._transaction.Rollback();

                        Trace.WriteLine(ex);

                        throw;
                    }
                    finally
                    {
                        this._transaction.Dispose();
                        this._transaction = null;
                    }
                }
                else
                {
                    if (!this._transaction.IsNull())
                        this._transaction.Dispose();

                    this._transaction = null;
                }
            }
            catch (Exception ex)
            {
                if (!this._transaction.IsNull())
                    this._transaction.Dispose();

                this._transaction = null;

                Trace.WriteLine(ex);

                throw;
            }
        }

        public void Rollback()
        {
            try
            {
                if (this._connection.State.Equals(ConnectionState.Open))
                {
                    try
                    {
                        this._transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        this._transaction.Rollback();

                        Trace.WriteLine(ex);

                        throw;
                    }
                    finally
                    {
                        this._transaction.Dispose();
                        this._transaction = null;
                    }
                }
                else
                {
                    if (!this._transaction.IsNull())
                        this._transaction.Dispose();

                    this._transaction = null;
                }
            }
            catch (Exception ex)
            {
                if (!this._transaction.IsNull())
                    this._transaction.Dispose();

                this._transaction = null;

                Trace.WriteLine(ex);

                throw;
            }
        }

        #endregion
    }
}
