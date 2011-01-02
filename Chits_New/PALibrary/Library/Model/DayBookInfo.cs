using System;

namespace PALibrary.Library.Model
{
    public class DayBookInfo
    {
        private DateTime currentDate;
        private string particulars;
        private string voucherType;
        private int voucherNo;
        private decimal debit;
        private decimal credit;
        private string narration;

        private string fromLedger;
        private string toLedger;

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }

        public string Particulars
        {
            get { return particulars; }
            set { particulars = value; }
        }

        public string VoucherType
        {
            get { return voucherType; }
            set { voucherType = value; }
        }

        public int VoucherNo
        {
            get { return voucherNo; }
            set { voucherNo = value; }
        }

        public decimal Debit
        {
            get { return debit; }
            set { debit = value; }
        }

        public decimal Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        public string Narration
        {
            get { return narration; }
            set { narration = value; }
        }

        public string FromLedger
        {
            get { return fromLedger; }
            set { fromLedger = value; }
        }

        public string ToLedger
        {
            get { return toLedger; }
            set { toLedger = value; }
        }

        public void SwapOpeningBalance()
        {
            decimal temp = debit;
            debit = credit;
            credit = temp;
        }
    }
}
