using Identity_Session.Core.Entities;

namespace Identity_Session.Entities.Concrete
{
    public class Picture:BaseEntity
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public int? ProductId { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
