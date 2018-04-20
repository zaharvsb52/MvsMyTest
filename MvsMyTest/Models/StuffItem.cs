using System.Collections.Generic;

namespace MvsMyTest.Models
{
    public class StuffItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<TagItem> Tags { get; set; }
    }
}
