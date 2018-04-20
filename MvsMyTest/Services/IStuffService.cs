using System.Threading.Tasks;
using MvsMyTest.Models;

namespace MvsMyTest.Services
{
    public interface IStuffService
    {
        Task<StuffItem> GetByIdAsync(int id);
        Task UpdateAsync(StuffItem item);
        Task DeleteAsync(int id);
    }
}
