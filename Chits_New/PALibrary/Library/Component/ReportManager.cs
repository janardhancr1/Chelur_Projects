using System;
using System.Collections.Generic;
using System.Text;
using PALibrary.Library.Model;

namespace PALibrary.Library.Component
{
    public class ReportManager
    {
        public List<LedgerDate> GetLedgerDate(DateTime fromDate, DateTime toDate, string ledgerName)
        {
            List<LedgerDate> details = new List<LedgerDate>();

            LedgerDate detail = new LedgerDate();
            detail.FromDate = fromDate;
            detail.ToDate = toDate;
            detail.LedgerName = ledgerName;

            details.Add(detail);
            return details;
        }
    }
}
