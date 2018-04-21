using MongoDB.Driver;
using MvsMyTest.Models;

namespace MvsMyTest.Data
{
    public interface IMyDbContext
    {
        IMongoCollection<TDocument> GetDocuments<TDocument>() where TDocument : IModel;
    }
}
