using Identity_Session.Core.Entities;

namespace Identity_Session.Entities.Concrete
{
    public class ProfilePhoto : BaseEntity
    {
        public string Image { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
