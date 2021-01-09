using Microsoft.Extensions.DependencyInjection;
using System;
using Vrnz2.Infra.Repository.Interfaces.Base;
using Vrnz2.Infra.Repository.Test.Data;
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

            var userRepository = unitOfWork.GetRepository<Vrnz2.Infra.Repository.Test.Data.UserRepository>();
            var personRepository = unitOfWork.GetRepository<Vrnz2.Infra.Repository.Test.Data.PersonRepository>();

            unitOfWork.OpenConnection();

            var result = userRepository.GetByLogin("vernizze");

            InitData.Instance.Dispose();
        }
    }
}
