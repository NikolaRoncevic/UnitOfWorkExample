namespace Examples.UnitOfWork
{
    using Microsoft.Azure.Cosmos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DatabaseContext
    {
        public Container Container { get; }

        private List<IBaseEntity> Items { get; } = new List<IBaseEntity>();


        public DatabaseContext(Container container)
        {
            Container = container;
        }

        public void Add(IBaseEntity dataObject)
        {
            if (!Items.Any(item => item.Uid == dataObject.Uid && item.PartitionKey == dataObject.PartitionKey))
            {
                Items.Add(dataObject);
            }
        }

        public async Task<IEnumerable<IBaseEntity>> Save(CancellationToken cancellationToken = default)
        {
            if (Items.Count > 100)
            {
                throw new Exception("Items limit reached");
            }

            if (Items.Select(x => x.PartitionKey).Distinct().Count() > 1)
            {
                throw new Exception("Coliding partition keys");
            }

            var partitionKey = new PartitionKey(Items[0].PartitionKey);
            var transactionalBatch = Container.CreateTransactionalBatch(partitionKey);

            try
            {
                foreach (var dataObject in Items)
                {
                    if (dataObject.State == EntityState.Created)
                    {
                        transactionalBatch.CreateItem(dataObject);
                    }
                    else if (dataObject.State == EntityState.Updated)
                    {
                        transactionalBatch.ReplaceItem(dataObject.Uid.ToString(), dataObject);
                    }
                    else if (dataObject.State == EntityState.Deleted)
                    {
                        transactionalBatch.DeleteItem(dataObject.Uid.ToString());
                    }
                }

                var tbResult = await transactionalBatch.ExecuteAsync(cancellationToken);

                if (!tbResult.IsSuccessStatusCode)
                {
                    throw new Exception("Error execution transactional batch");
                }

                List<IBaseEntity> returnDataObjects = new List<IBaseEntity>();

                var result = Items.AsEnumerable();

                Items.Clear();

                return result;
            }
            catch (Exception)
            {
                throw new Exception("There has been an error");
            }
        }
    }
}
