using System;

namespace PALibrary.Library.Utils
{
	public class DBConstant
    {
        public const string DB_PARAM = "?";

        public const int PAY_MODE_CASH = 1;
        public const int PAY_MODE_CHEQUE = 2;

        public const int MODE_ADD = 1;
        public const int MODE_UPDATE = 2;

        public const string TYPE_CLOSED = "y";
        public const string TYPE_PENDING = "n";
        public const string TYPE_ALL = "a";

        public const int PAGE_SIZE = 25;

	    public const int VOUCHER_PAYMENT = 1;
	    public const int VOUCHER_RECEIPT = 2;
	    public const int VOUCHER_CONTRA = 3;

        public const string HUNDI_LOAN = "Hundi Loan";

        public const string CASH_LEDGERS = "CASH-IN-HAND";
        public const string BANK_LEDGERS = "BANK ACCOUNTS";
        
        public const string CASH_LEDGER = "Cash";
        public const string INTEREST_LEDGER = "Interest";
        public const string INTEREST_PAID_LEDGER = "Interest Paid";
        public const string CHIT_COMMISSION_LEDGER = "Chits Commission";

        public const string PARTICULARS_BY = "By ";
        public const string PARTICULARS_TO = "To ";

        public const string NARATION_FROM = "From - ";
        public const string NARATION_TOWARDS = "Towards - ";

	    public const string ACCOUNT_DR = "Dr";
	    public const string ACCOUNT_CR = "Cr";

        public const int ACCOUNT_PERIOD = 1;
        public const int ACCOUNT_LEDGER = 2;

        public const int ACCOUNT_OPENING = 1;
        public const int ACCOUNT_OPENING_CUSTOMER = 2;
        public const int ACCOUNT_OPENING_CASH = 3;
        public const int ACCOUNT_OPENING_BANK = 4;

        public const string VOUCHER_HLOAN = "HUNDILOAN";
        public const string VOUCHER_HLRECEIPT = "HLRECEIPT";
        public const string VOUCHER_HLINTEREST = "HLINTEREST";

        public const string VOUCHER_FIXEDDESPOSIT = "FIXEDDEPOSIT";
        public const string VOUCHER_FDPAYMENT = "FDPAYMENT";
        public const string VOUCHER_FDINTEREST = "FDINTEREST";

	    public const string ATKT_PAY = "Payment";
        public const string ATKT_RECP = "Receipt";

	    public const string VOUCHER_ATKTPAY = "ATKTPAY";
	    public const string VOUCHER_ATKTRECP = "ATKTRECP";

        public const string CHITS_INSTALLMENT = "CHITINSTAL";
        public const string CHITS_BIDDING = "CHITBID";
        public const string CHITS_COMMISSION = "CHITCOMM";

        public const int START_YEAR = 2000;

	    private int userID;
	    private string userName;
	    private DateTime currentDate;
        private DateTime finYearStart;
        private DateTime finYearEnd;

	    public int UserID
	    {
            get { return userID; }
            set { userID = value; }
	    }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }

        public DateTime FinYearStart
        {
            get { return finYearStart; }
            set { finYearStart = value; }
        }

        public DateTime FinYearEnd
        {
            get { return finYearEnd; }
            set { finYearEnd = value; }
        }
    }
}