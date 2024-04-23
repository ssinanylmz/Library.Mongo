using MongoDB.Bson;
using MongoDB.Driver;
using Library.Mongo.Entities;
using System.Linq.Expressions;

namespace Library.Mongo
{
    public interface IMongoRepository<T> where T : IDocument
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filterExpression);
        Task<T> GetByIdAsync(ObjectId id);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);
        Task InsertAsync(T document);
        Task InsertManyAsync(IEnumerable<T> document);
        Task UpdateAsync(T document);
        Task DeleteAllAsync();
        Task DeleteAsync(ObjectId id);
        Task DropCollectionAsync();
        Task<IClientSessionHandle> StartSessionAsync();
        Task<IClientSessionHandle> StartTransactionAsync(IClientSessionHandle session);
        Task TransactionCommitAsync(IClientSessionHandle session);
        Task AbortTransactionAsync(IClientSessionHandle session);
    }
}
