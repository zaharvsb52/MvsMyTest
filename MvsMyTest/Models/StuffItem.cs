using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MvsMyTest.Models
{
    public class StuffItem : IModel
    {
        public const string Undefined = "__undefined__";

        [BsonId]
        public int? Id { get; set; }
        public string Name { get; set; } = Undefined;
        public string Description { get; set; } = Undefined;
        
        public ICollection<TagItem> Tags { get; set; }
    }
}
