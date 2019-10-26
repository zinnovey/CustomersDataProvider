using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicLayer.DataTransferObjects
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CustomerInfoCriteriaDTO
    {
        #region Properties

        public String CustomerID{ get; set; }

        public String Email { get; set; }

        #endregion

    }
}
