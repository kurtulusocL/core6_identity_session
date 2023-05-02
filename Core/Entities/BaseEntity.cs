using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace Identity_Session.Core.Entities
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public void SetCreatedDate()
        {
            CreatedDate = DateTime.Now.ToLocalTime();
        }

        public void SetConfirmed()
        {
            IsConfirmed = true;
        }

        public void SetDeleted()
        {
            IsDeleted = false;
        }

        public BaseEntity()
        {
            SetCreatedDate();
            SetConfirmed();
            SetDeleted();
        }
    }
}
