using Microsoft.Extensions.DependencyInjection;
using System;
using Vrnz2.Infra.Repository.Interfaces.Base;
using Vrnz2.Infra.Repository.Test.Init;
using Xunit;

namespace Vrnz2.Infra.Repository.Test
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void Connect()
        {
            var repository = InitData.Instance.GetRepository;

            repository.GetByNome("");

            InitData.Instance.Dispose();
        }
    }
}
