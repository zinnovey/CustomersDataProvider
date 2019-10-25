using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicLayer.DataTransferObjects
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CustomerDTO
    {
        #region Properties

        public Int32 CustomerID { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String Mobile { get; set; }

        public ICollection<TransactionDTO> Transactions { get; set; }

        #endregion

        #region Constructors

        public CustomerDTO()
        {
            Transactions = new List<TransactionDTO>();
        }
        
        #endregion

    }
}
