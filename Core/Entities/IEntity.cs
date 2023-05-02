namespace Identity_Session.Core.Entities
{
    public interface IEntity
    {
        void SetCreatedDate();
        void SetConfirmed();
        void SetDeleted();
    }
}
