using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MvsMyTest.Models;

namespace MvsMyTest.Data
{
    public class MyDbContext : IMyDbContext
    {
        private readonly IMongoDatabase _database;

        public MyDbContext(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _database = client.GetDatabase(settings.Database);
        }

        public MyDbContext(IOptions<Settings> settings) : this(settings.Value)
        {
        }

        public MyDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<TDocument> GetDocuments<TDocument>() where TDocument : IModel
        {
            if (typeof(TDocument) == typeof(Note))
                return _database.GetCollection<TDocument>("Note");
            if (typeof(TDocument) == typeof(StuffItem))
                return _database.GetCollection<TDocument>("Stuff");
            if (typeof(TDocument) == typeof(TagItem))
                return _database.GetCollection<TDocument>("Tag");

            throw new NotImplementedException();
        }

        //public IMongoCollection<Note> Notes => _database.GetCollection<Note>("Note");
        //public IMongoCollection<StuffItem> StuffItems => _database.GetCollection<StuffItem>("Stuff");
        //public IMongoCollection<TagItem> TagItems => _database.GetCollection<TagItem>("Tag");
    }
}
