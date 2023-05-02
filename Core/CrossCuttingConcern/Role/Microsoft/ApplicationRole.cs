using Identity_Session.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity_Session.Core.CrossCuttingConcern.Role.Microsoft
{
    public class ApplicationRole : IdentityRole, IEntity
    {
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
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
        public ApplicationRole()
        {
            SetConfirmed();
            SetCreatedDate();
            SetDeleted();
        }
    }
}
