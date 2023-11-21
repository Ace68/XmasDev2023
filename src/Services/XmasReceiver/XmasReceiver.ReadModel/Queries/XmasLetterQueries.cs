using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Entities;

namespace XmasReceiver.ReadModel.Queries;

public sealed class XmasLetterQueries : IQueries<XmasLetter>
{
    private IMongoDatabase _database;
    
    public string DatabaseName { get; private set; }

    public XmasLetterQueries(IMongoDatabase mongoDatabase)
    {
        _database = mongoDatabase;
    }

    public async Task<XmasLetter> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<XmasLetter>(nameof(XmasLetter));
        var filter = Builders<XmasLetter>.Filter.Eq("_id", id);
        return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
            ? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
            : null)!;
    }

    public async Task<PagedResult<XmasLetter>> GetByFilterAsync(Expression<Func<XmasLetter, bool>>? query, int page,
        int pageSize, CancellationToken cancellationToken)
    {
        if (--page < 0)
            page = 0;

        var collection = _database.GetCollection<XmasLetter>(nameof(XmasLetter));
        var queryable = query != null
            ? collection.AsQueryable().Where(query)
            : collection.AsQueryable();

        var count = await queryable.CountAsync(cancellationToken: cancellationToken);
        var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

        return new PagedResult<XmasLetter>(results, page, pageSize, count);
    }
}