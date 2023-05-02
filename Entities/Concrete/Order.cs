using Identity_Session.Core.Entities;
using Identity_Session.Entities.Abstract;

namespace Identity_Session.Entities.Concrete
{
    public class Order : BaseEntity/*, IOrder*/
    {
        public string City{ get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public bool IsSend { get; set; } = false;
        public DateTime SendDate { get; set; }

        public int? ProductId { get; set; }
        public int CountryId { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Country Country { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        //public void SetSend()
        //{
        //    IsSend = false;
        //}

    }
}
