using Vrnz2.Infra.Repository.Interfaces.Base;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public interface IRepository
        : IBaseRepository
    {
        User GetByLogin(string login);
    }
}
