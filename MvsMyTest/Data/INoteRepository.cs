using System.Threading.Tasks;
using MongoDB.Driver;
using MvsMyTest.Models;

namespace MvsMyTest.Data
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<UpdateResult> Update(string id, string body);
    }
}
