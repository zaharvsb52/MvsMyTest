using MongoDB.Bson.Serialization.Attributes;

namespace MvsMyTest.Models
{
    public class TagItem : IModel
    {
        //[NotMapped]
        [BsonId]
        public int Id { get; set; }

        public string Value { get; set; }

        //[NotMapped]
        public int StuffId { get; set; }

        //public StuffItem Stuff { get; set; }
    }
}
