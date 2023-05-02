using Identity_Session.Core.Entities;

namespace Identity_Session.Entities.Concrete
{
    public class ExceptionLogger : BaseEntity
    {
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
