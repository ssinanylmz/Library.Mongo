using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Library.Mongo.Entities;
using Library.Mongo.Models;
using System.Linq.Expressions;

namespace Library.Mongo
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoRepository(IMongoClient client, IOptions<MongoDbSettings> settingsOptions)
        {
            var mongoSettings = settingsOptions.Value;
            _database = client.GetDatabase(mongoSettings.DatabaseName);
            _collection = _database.GetCollection<TDocument>(typeof(TDocument).Name);
            _client = client;
        }

        public async Task<IEnumerable<TDocument>> GetAllAsync()
        {
            return await _collection.Find(doc => true).ToListAsync();
        }

        public async Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument,bool>> filterExpression)
        {
            return await _collection.Find<TDocument>(filterExpression).ToListAsync();
        }

        public async Task<TDocument> GetByIdAsync(ObjectId id)
        {
            return await _collection.Find<TDocument>(doc => doc.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await _collection.Find<TDocument>(filterExpression).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task InsertManyAsync(IEnumerable<TDocument> documents)
        {
            int batchSize = 10000;
            var documentList = documents.ToList(); // IEnumerable'ı List'e çeviriyoruz 

            for (int i = 0; i < documentList.Count; i += batchSize)
            {
                var batch = documentList.Skip(i).Take(batchSize);
                await _collection.InsertManyAsync(batch);
            }

            //await _collection.InsertManyAsync(documents);
        }

        public async Task UpdateAsync(TDocument document)
        {
            await _collection.ReplaceOneAsync(doc => doc.Id == document.Id, document);
        }

        public async Task DeleteAllAsync()
        {
            await _collection.DeleteManyAsync(FilterDefinition<TDocument>.Empty);
        }
        public async Task DropCollectionAsync()
        {
            await _database.DropCollectionAsync(typeof(TDocument).Name);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _collection.DeleteOneAsync(doc => doc.Id == id);
        }

        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            var session = await _client.StartSessionAsync();

            return session;
        }

        public async Task<IClientSessionHandle> StartTransactionAsync(IClientSessionHandle session)
        {
            session.StartTransaction();

            return session;
        }

        public async Task TransactionCommitAsync(IClientSessionHandle session)
        {
            await session.CommitTransactionAsync();
        }

        public async Task AbortTransactionAsync(IClientSessionHandle session)
        {
            await session.AbortTransactionAsync();
        }

        public void SessionDispose(IClientSessionHandle session)
        {
            session.Dispose();
        }
    }
}
