using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using Vrnz2.Infra.Repository.Interfaces.Base;
using Vrnz2.Infra.Repository.Test.Data;

namespace Vrnz2.Infra.Repository.Test.Init
{
    public class InitData
        : IDisposable
    {
        private static InitData _instance;

        private InitData() { Init(); }

        public static InitData Instance
        {
            get 
            {
                _instance = _instance ?? new InitData();

                return _instance;
            }
        }

        public IUnitOfWork GetUnitOfWork { get; private set; }

        public void Dispose()
            => GetUnitOfWork.Dispose();

        private void Init() 
        {
            var fileContent = File.ReadAllText("appsettings.json");

            var appSettings = JsonConvert.DeserializeObject<AppSettings>(fileContent);

            var services = new ServiceCollection()
                .AddSingleton(_ => appSettings)
                .AddSingleton(_ => appSettings.ConnectionStrings)
                //.AddTransient<IUserRepository, Vrnz2.Infra.Repository.Test.Data.UserRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            var provider = services.BuildServiceProvider();

            GetUnitOfWork = provider.GetService<IUnitOfWork>();
        }
    }
}
