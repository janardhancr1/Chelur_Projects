using System;
using System.Collections.Generic;
using System.Data;

namespace PALibrary.Library.Model
{
    public class ChitsReportInfo
    {
        private string chitName;
        private string customerName;
        private string customerAddress;
        private int installmentNO;
        private decimal installmentAmount;
        private DateTime paidDate;
        private DateTime bidDate;
        private decimal paidAmount;

        public string ChitName
        {
            get { return chitName; }
            set { chitName = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string CustomerAddress
        {
            get { return customerAddress; }
            set { customerAddress = value; }
        }

        public int InstallmentNO
        {
            get { return installmentNO; }
            set { installmentNO = value; }
        }

        public decimal InstallmentAmount
        {
            get { return installmentAmount; }
            set { installmentAmount = value; }
        }

        public decimal PaidAmount
        {
            get { return paidAmount; }
            set { paidAmount = value; }
        }

        public DateTime BidDate
        {
            get { return bidDate; }
            set { bidDate = value; }
        }

        public DateTime PaidDate
        {
            get { return paidDate; }
            set { paidDate = value; }
        }
    }
}
