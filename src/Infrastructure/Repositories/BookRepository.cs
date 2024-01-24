using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    public BookRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }   

    public async Task<IEnumerable<Book>> BooksSearch(string serachKey, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {        
        await using SqlConnection sqlConnection = _connectionFactory
            .CreateSqlConnection();

        const string StoredProcedureName = "BooksSearch";
        var procedureParametars = new { 
            SearchKey = serachKey,
            PageNumber = pageNumber,
            PageSize = pageSize
        };        

        var results = await sqlConnection.QueryAsync<Book>(StoredProcedureName, procedureParametars, 
            commandType: CommandType.StoredProcedure);

        return results;             
    }
}
