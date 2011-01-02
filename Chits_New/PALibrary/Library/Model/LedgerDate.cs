using System;
using System.Collections.Generic;
using System.Text;

namespace PALibrary.Library.Model
{
    public class LedgerDate
    {
        private string ledgerName;

        private DateTime fromDate;
        private DateTime toDate;

        public string LedgerName
        {
            get { return ledgerName; }
            set { ledgerName = value; }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
            set { fromDate = value; }
        }

        public DateTime ToDate
        {
            get { return toDate; }
            set { toDate = value; }
        }
    }
}
