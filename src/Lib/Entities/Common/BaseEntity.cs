namespace Entities.Common
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public BaseEntity()
        {
            //CreateUserId = 0;
            //CreateDate = DateTime.Now;
            //IsDeleted = false;
        }
        public TKey Id { get; set; }
        //public bool IsDeleted { get; set; }
        //public int CreateUserId { get; set; }
        //public DateTime CreateDate { get; set; }
        //public int DeleteUserId { get; set; }
        //public DateTime DeleteDate { get; set; }

        //public int ModifiedUserId { get; set; }
        //public DateTime ModifiedDate { get; set; }
        public int VersionStatus { get; set; }
        public int Version { get; set; }

    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
