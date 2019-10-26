using System;

namespace CustomersDataProvider.DataAccessLayer.Entities.Enums
{
    [Flags]
    public enum TransactionStatus
    {
        Success,
        Failed,
        Canceled
    }
}
