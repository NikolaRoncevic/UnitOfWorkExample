namespace Examples.UnitOfWork
{
    using System;
    using Newtonsoft.Json;

    public interface IBaseEntity
    {
        Guid Uid { get; }

        DateTime CreatedOn { get; }

        string PartitionKey { get; }

        string EntityName { get; }

        [JsonIgnore]
        EntityState State { get; }
    }
}
