namespace Examples.UnitOfWork
{
    using System;

    public class BaseEntity<T> : IBaseEntity where T : class
    {
        public Guid Uid { get; }

        public DateTime CreatedOn { get; }

        public string EntityName { get; } = typeof(T).Name;

        public virtual string PartitionKey { get; }

        public EntityState State { get; }

        public BaseEntity(Guid uid, DateTime createdOn, string partitionKey, EntityState state)
        {
            Uid = uid;
            CreatedOn = createdOn;
            PartitionKey = partitionKey;
            State = state;
        }
    }
}
