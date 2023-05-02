using Identity_Session.Core.Entities;

namespace Identity_Session.Entities.Concrete
{
    public class Comment : BaseEntity
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int? Hit { get; set; } = 0;

        public int? ProductId { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
