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

            var repository = unitOfWork.GetRepository<Vrnz2.Infra.Repository.Test.Data.Repository>(nameof(User));

            var result = repository.GetByLogin("vernizze");

            InitData.Instance.Dispose();
        }
    }
}
