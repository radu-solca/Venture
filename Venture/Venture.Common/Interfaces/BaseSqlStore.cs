using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Venture.Users.Data
{
    public class BaseSqlStore<TEntity> : IStore<TEntity> where TEntity : class, IEntity, new()
    {
        private SqlConnection _connection;

        private readonly string getByIdSql;
        private readonly string getAllSql;
        private readonly string DeleteByIdSql;

        public BaseSqlStore(SqlConnection connection)
        {
            _connection = connection;

            getAllSql =
            @"SELECT * FROM " + typeof(TEntity).Name + " entity WHERE entity.Id = @id";

            getAllSql =
            @"SELECT * FROM " + typeof(TEntity).Name;

            getAllSql =
            @"DELETE * FROM " + typeof(TEntity).Name + " entity WHERE entity.Id = @id";
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _connection.QueryAsync<TEntity>(getAllSql);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var results = (await _connection.QueryAsync<TEntity>(getAllSql, new { id })).ToList();

            if (!results.Any())
            {
                return null;
            }

            return results.FirstOrDefault();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
