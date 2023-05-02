using Identity_Session.Core.Entities;

namespace Identity_Session.Entities.Concrete
{
    public class Subcategory:BaseEntity
    {
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
