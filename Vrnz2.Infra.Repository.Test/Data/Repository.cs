using Dapper;
using Vrnz2.Infra.Repository.Abstract;

namespace Vrnz2.Infra.Repository.Test.Data
{
    public class Repository
        : BaseRepository, IRepository
    {
        public Repository()
            => TableName = nameof(Entity);

        public override bool Insert<Filial>(Filial value)
        => true;

        public Entity GetByNome(string nome)
            => _dbConnection.QueryFirstOrDefault<Entity>("SELECT * FROM Entity WHERE Nome = @nome;", new { nome }, transaction: _dbTransaction);
    }
}
