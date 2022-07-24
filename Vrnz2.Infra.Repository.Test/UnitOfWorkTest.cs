using FluentAssertions;
using Vrnz2.Infra.Repository.Test.Data;
using Vrnz2.Infra.Repository.Test.Init;
using Vrnz2.TestUtils;
using Xunit;

namespace Vrnz2.Infra.Repository.Test
{
    public class UnitOfWorkTest
        : AbstractTest
    {
        [Fact]
        public void Connect()
        {
            // Arrange
            var login = "vernizze";

            var unitOfWork = InitData.Instance.GetUnitOfWork;

            var userRepository = unitOfWork.GetRepository<Vrnz2.Infra.Repository.Test.Data.IUserRepository>(nameof(User));
            var personRepository = unitOfWork.GetRepository<Vrnz2.Infra.Repository.Test.Data.IPersonRepository>(nameof(Person));

            unitOfWork.OpenConnection();

            // Act
            var result = userRepository.GetByLogin(login);

            InitData.Instance.Dispose();

            // Assert
            result.Should().NotBeNull();
            result.Login.Should().Be(login);
        }
    }
}
