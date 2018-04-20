using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvsMyTest.Models;

namespace MvsMyTest.Services
{
    public class StuffService : IStuffService
    {
        private const string Undefined = "__undefined__";
        private readonly StuffContext _stuffContext;
        private readonly TagItemContext _tagContext;

        public StuffService(StuffContext stuffContext, TagItemContext tagContext)
        {
            _stuffContext = stuffContext;
            _tagContext = tagContext;
        }

        public Task<StuffItem> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                var item = GetItemById(id);
                if (item != null)
                    item.Tags = GetTagsByStuff(item.Id);

                return item;
            });
        }

        public Task UpdateAsync(StuffItem item)
        {
            return Task.Run(() =>
            {
                if (item.Id.HasValue) // update
                {
                    var updateitem = GetItemById(item.Id.Value);
                    if (updateitem == null)
                        return;

                    if (item.Name?.ToLower() != Undefined)
                        updateitem.Name = item.Name;
                    if (item.Description?.ToLower() != Undefined)
                        updateitem.Description = item.Description;

                    _stuffContext.StuffItems.Update(updateitem);
                }
                else //Add new
                {
                    if (item.Name?.ToLower() == Undefined)
                        item.Name = null;
                    if (item.Description?.ToLower() == Undefined)
                        item.Description = null;

                    _stuffContext.StuffItems.Add(item);
                }

                _stuffContext.SaveChanges();

                if (item.Id.HasValue && item.Tags != null && item.Tags.Count > 0)
                {
                    var tags = GetTagsByStuff(item.Id);
                    if (tags.Count > 0)
                    {
                        _tagContext.TagItems.RemoveRange(tags);
                        _tagContext.SaveChanges();
                    }

                    foreach (var tag in item.Tags)
                    {
                        tag.Id = 0;
                        tag.StuffId = item.Id.Value;
                    }

                    _tagContext.TagItems.AddRange(item.Tags);
                    _tagContext.SaveChanges();
                }
            });
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() =>
            {
                var item = GetItemById(id);
                if (item == null)
                    return;

                var tags = GetTagsByStuff(id);
                if (tags.Count > 0)
                {
                    _tagContext.TagItems.RemoveRange(tags);
                    _tagContext.SaveChanges();
                }

                _stuffContext.StuffItems.Remove(item);
                _stuffContext.SaveChanges();
            });
        }

        private StuffItem GetItemById(int id)
        {
            return _stuffContext.StuffItems?.FirstOrDefault(p => p.Id == id);
        }

        private ICollection<TagItem> GetTagsByStuff(int? stuffId)
        {
            return _tagContext.TagItems.Where(p => p.StuffId == stuffId).ToArray();
        }
    }
}
