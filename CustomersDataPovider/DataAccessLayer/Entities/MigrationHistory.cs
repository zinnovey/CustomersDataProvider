using System;

namespace CustomersDataProvider.DataAccessLayer.Entities
{
    public class MigrationHistory
    {
        #region Properties

        public String MigrationId { get; set; }

        public String ContextKey { get; set; }

        public Byte[] Model { get; set; }

        public String ProductVersion { get; set; }

        #endregion

    }
}
