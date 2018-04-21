using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MvsMyTest.Models;

namespace MvsMyTest.Data
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(IMyDbContext context) : base(context)
        {
        }

        public override Task<UpdateResult> Update(Note item)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateResult> Update(string id, string body)
        {
            var filter = Builders<Note>.Filter.Eq(s => s.Id, id);
            var update = Builders<Note>.Update
                .Set(s => s.Body, body)
                .CurrentDate(s => s.UpdatedOn);

            var doc = Document;
            if (doc == null)
                return null;

            return await doc.UpdateOneAsync(filter, update);
        }
    }
}
