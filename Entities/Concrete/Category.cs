using Identity_Session.Core.Entities;

namespace Identity_Session.Entities.Concrete
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
