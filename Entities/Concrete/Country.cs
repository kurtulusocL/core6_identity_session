using Identity_Session.Core.Entities;

namespace Identity_Session.Entities.Concrete
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
