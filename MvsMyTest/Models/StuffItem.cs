using System.Collections.Generic;

namespace MvsMyTest.Models
{
    public class StuffItem
    {
        public const string Undefined = "__undefined__";

        public int? Id { get; set; }
        public string Name { get; set; } = Undefined;
        public string Description { get; set; } = Undefined;
        
        public ICollection<TagItem> Tags { get; set; }
    }
}
