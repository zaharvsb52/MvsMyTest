using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MvsMyTest.Models;

namespace MvsMyTest.Data
{
    public abstract class RepositoryBase<TDocument> : IRepository<TDocument> where TDocument : IModel
    {
        protected readonly IMyDbContext Context;

        protected RepositoryBase(IMyDbContext context)
        {
            Context = context;
        }

        protected IMongoCollection<TDocument> Document => Context.GetDocuments<TDocument>();

        public async Task<ICollection<TDocument>> GetAll()
        {
            var doc = Document;
            if (doc == null)
                return null;

            return await doc.Find(_ => true).ToListAsync();
        }

        public async Task<TDocument> Get(string id)
        {
            var doc = Document;
            if (doc == null)
                return default(TDocument);

            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await doc
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public virtual async Task Add(TDocument item)
        {
            var doc = Document;
            if (doc == null)
                return;

            await doc.InsertOneAsync(item);
        }

        public async Task<DeleteResult> Remove(string id)
        {
            var doc = Document;
            if (doc == null)
                return null;

            return await doc.DeleteOneAsync(
                Builders<TDocument>.Filter.Eq("Id", id));
        }

        public async Task<DeleteResult> RemoveAll()
        {
            var doc = Document;
            if (doc == null)
                return null;

            return await doc.DeleteManyAsync(Builders<TDocument>.Filter.Empty);
        }

        public abstract Task<UpdateResult> Update(TDocument item);
    }
}