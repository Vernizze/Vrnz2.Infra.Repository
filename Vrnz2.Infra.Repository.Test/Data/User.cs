using Vrnz2.Infra.Repository.Abstract;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public class User
        : BaseDataObject
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public bool Confirmed { get; set; }
        public bool Blocked { get; set; }
    }
}
