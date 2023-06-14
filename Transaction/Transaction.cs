namespace Examples.UnitOfWork.Transaction
{
    using Newtonsoft.Json;
    using System;

    public class Transaction : BaseEntity<Transaction>
    {
        [JsonProperty("accountNumber")]
        public override string PartitionKey { get { return base.PartitionKey; } }

        [JsonProperty("amount")]
        public decimal Amount { get; }

        public Transaction(Guid uid,
                           DateTime createdOn,
                           string accountNumber, // partition key
                           decimal amount,
                           EntityState state) : base(uid, createdOn, accountNumber, state)
        {
            Amount = amount;
        }
    }
}
