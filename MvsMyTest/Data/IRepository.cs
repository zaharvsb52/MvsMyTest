using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MvsMyTest.Data
{
    public interface IRepository<TDocument>
    {
        Task<ICollection<TDocument>> GetAll();
        Task<TDocument> Get(string id);
        Task Add(TDocument item);
        Task<UpdateResult> Update(TDocument item);
        Task<DeleteResult> Remove(string id);
        Task<DeleteResult> RemoveAll();
    }
}
