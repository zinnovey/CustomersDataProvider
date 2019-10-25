using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicLayer.DataTransferObjects
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class TransactionDTO
    {
        #region Properties

        public Int32 Id { get; set; }

        public DateTime Date { get; set; }

        public Double Amount { get; set; }

        public String Currency { get; set; }

        public String Status { get; set; }

        #endregion

    }
}
