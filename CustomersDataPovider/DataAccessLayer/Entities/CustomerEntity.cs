using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class CustomerEntity : BaseEntity
    {
        #region Properties
        
        public string Name { get; set; }

        public string ContactEmail { get; set; }

        public decimal MobileNumber { get; set; }

        public virtual ICollection<TransactionEntity> Transactions { get; set; }

        #endregion

        #region Constructors

        public CustomerEntity()
        {
            Transactions = new HashSet<TransactionEntity>();
        }

        #endregion

    }
}
