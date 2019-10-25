using System;
using DataAccessLayer.Entities.Enums;

namespace DataAccessLayer.Entities
{
    public class TransactionEntity : BaseEntity
    {
        #region Properties

        public int CustomerId { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public TransactionStatus Status { get; set; }

        public virtual CustomerEntity Customer { get; set; }

        #endregion

    }
}
