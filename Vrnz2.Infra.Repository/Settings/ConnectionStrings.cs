using System.Collections.Generic;
using Vrnz2.BaseContracts.Settings.Base;

namespace Vrnz2.Infra.Repository.Settings
{
    public class ConnectionStringsSettings
        : BaseAppSettings
    {
        public List<ConnectionString> ConnectionsStrings { get; set; }
    }

    public class ConnectionString
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
