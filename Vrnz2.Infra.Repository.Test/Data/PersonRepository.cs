using System.Threading.Tasks;
using Vrnz2.Infra.Repository.Abstract;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public class PersonRepository
        : BaseRepository, IPersonRepository
    {
        public PersonRepository()
            => TableName = nameof(Person);
    }
}
