using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;

namespace Test.Infrastructure.Common.Database
{
    public class FluentDbAssertion
    {
        private readonly IDbConnection _connection;

        public FluentDbAssertion(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task ShouldExists(string table, params Guid[] ids)
        {
            var count = await Count(table, ids);
            count.Should().Be(ids.Length);
        }

        public async Task ShouldNotExists(string table, params Guid[] ids)
        {
            var count = await Count(table, ids);
            count.Should().Be(0);
        }

        public async Task<T> Get<T>(string table, Guid id)
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];

            var parameters = ctor.GetParameters().Select(p => p.Name).ToList();
            parameters[0] = $"ID as {parameters[0]}";
            var selParameters = string.Join(",", parameters);

            return await _connection.QuerySingleAsync<T>(
                $"SELECT {selParameters} FROM {table} WHERE [Id] = @id AND [IsDeleted] = 0",
                new { id });
        }

        public async Task<IEnumerable<T>> Gets<T>(string table)
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];

            var parameters = ctor.GetParameters().Select(p => p.Name).ToList();
            parameters[0] = $"ID as {parameters[0]}";
            var selParameters = string.Join(",", parameters);

            return await _connection.QueryAsync<T>(
                $"SELECT {selParameters} FROM {table} WHERE [IsDeleted] = 0");
        }

        public async Task<IEnumerable<T>> GetsNotHaveColumnIsDeleted<T>(string table)
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];

            var parameters = ctor.GetParameters().Select(p => p.Name).ToList();
            parameters[0] = $"ID as {parameters[0]}";
            var selParameters = string.Join(",", parameters);

            return await _connection.QueryAsync<T>(
                $"SELECT {selParameters} FROM {table}");
        }

        private async Task<int> Count(string table, Guid[] ids)
        {
            return await _connection.ExecuteScalarAsync<int>(
                $"SELECT COUNT(id) FROM {table} WHERE id IN (@ids)", new { ids });
        }
    }
}
