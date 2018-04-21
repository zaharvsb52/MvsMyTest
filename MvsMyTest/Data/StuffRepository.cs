using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MvsMyTest.Models;

namespace MvsMyTest.Data
{
    public class StuffRepository : RepositoryBase<StuffItem>, IStuffRepository
    {
        public StuffRepository(IMyDbContext context) : base(context)
        {
        }

        public override async Task Add(StuffItem item)
        {
            //TODO: use autoincrement
            if (!item.Id.HasValue)
            {
                var items = await GetAll();
                if (items?.Count() > 0)
                {
                    var maxid = items.Max(p => p.Id);
                    item.Id = maxid + 1;
                }
                else
                {
                    item.Id = 1;
                }
            }

            await base.Add(item);
        }

        public override async Task<UpdateResult> Update(StuffItem item)
        {
            if (item.Id.HasValue) // update
            {
                var updateitem = await Get(item.Id.Value.ToString());
                if (updateitem != null)
                {
                    var builder = Builders<StuffItem>.Update;
                    UpdateDefinition<StuffItem> update = null;

                    // обновляем только те поля, которые изменились
                    if (item.Name?.ToLower() != StuffItem.Undefined)
                        update = builder.Set(p => p.Name, item.Name);

                    if (item.Description?.ToLower() != StuffItem.Undefined)
                    {
                        update = update == null
                            ? builder.Set(p => p.Description, item.Description)
                            : update.Set(p => p.Description, item.Description);
                    }

                    if (item.Tags != null && item.Tags.Count > 0)
                        update = update == null
                            ? builder.Set(p => p.Tags, item.Tags)
                            : update.Set(p => p.Tags, item.Tags);

                    if (update != null) //есть, что обновить
                    {
                        var doc = Document;
                        if (doc != null)
                            return await doc.UpdateOneAsync(Builders<StuffItem>.Filter.Eq(p => p.Id, item.Id), update);
                    }
                }
            }
            else //Add new
            {
                if (item.Name?.ToLower() == StuffItem.Undefined)
                    item.Name = null;
                if (item.Description?.ToLower() == StuffItem.Undefined)
                    item.Description = null;

               await Add(item);
            }

            return null;
        }
    }
}