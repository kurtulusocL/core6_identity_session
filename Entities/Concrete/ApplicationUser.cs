using Identity_Session.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity_Session.Entities.Concrete
{
    public class ApplicationUser : IdentityUser, IEntity
    {
        public string NameSurname { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string RespondTitle { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProfilePhoto> ProfilePhotos { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public void SetConfirmed()
        {
            IsConfirmed = true;
        }

        public void SetCreatedDate()
        {
            CreatedDate = DateTime.Now.ToLocalTime();
        }

        public void SetDeleted()
        {
            IsDeleted = false;
        }
        public ApplicationUser()
        {
            SetConfirmed();
            SetCreatedDate();
            SetDeleted();
            EmailConfirmed = true;
            PhoneNumberConfirmed = true;
        }
    }
}
