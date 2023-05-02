using Identity_Session.Core.Entities;
using Identity_Session.Entities.Abstract;

namespace Identity_Session.Entities.Concrete
{
    public class Product : BaseEntity, IProduct
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int UnitInStock { get; set; }
        public decimal Price { get; set; }
        public int? Hit { get; set; }
        public int? Like { get; set; }
        public int? Dislike { get; set; }

        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        public int? SubcategoryId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Category Category { get; set; }
        public virtual Subcategory Subcategory { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

        public void SetHit()
        {
            Hit = 0;
        }

        public void SetLike()
        {
            Like = 0;
        }
        public void SetDislike()
        {
            Dislike = 0;
        }
        public Product()
        {
            SetHit();
            SetLike();
            SetDislike();
        }
    }
}
