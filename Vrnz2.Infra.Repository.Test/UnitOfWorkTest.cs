using Vrnz2.Infra.Repository.Test.Init;
using Xunit;

namespace Vrnz2.Infra.Repository.Test
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void Connect()
        {
            var unitOfWork = InitData.Instance.GetUnitOfWork;

            var userRepository = unitOfWork.GetRepository<Vrnz2.Infra.Repository.Test.Data.IUserRepository>();
            var personRepository = unitOfWork.GetRepository<Vrnz2.Infra.Repository.Test.Data.IPersonRepository>();

            unitOfWork.OpenConnection();

            var result = userRepository.GetByLogin("vernizze");

            InitData.Instance.Dispose();
        }
    }
}
