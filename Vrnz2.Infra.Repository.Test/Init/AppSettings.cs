using Vrnz2.BaseContracts.Settings.Base;
using Vrnz2.Infra.Repository.Settings;

namespace Vrnz2.Infra.Repository.Test.Init
{
    public class AppSettings
        : BaseAppSettings
    {
        public ConnectionStringsSettings ConnectionStrings { get; set; }
    }
}
