using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MvsMyTest.Models
{
    public class Note : IModel
    {
        [BsonId]
        public string Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int UserId { get; set; } = 0;
    }
}
