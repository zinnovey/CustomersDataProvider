using System;

namespace DataAccessLayer.Entities.Enums
{
    [Flags]
    public enum TransactionStatus
    {
        Success,
        Failed,
        Canceled
    }
}
