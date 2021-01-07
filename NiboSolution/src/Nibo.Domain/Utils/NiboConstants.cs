
namespace Nibo.Domain.Utils
{
    public static class NiboConstants
    {
        public const string BANK_ACCOUNT_XPATH = "OFX/BANKMSGSRSV1/STMTTRNRS/TRNUID/STMTRS/CURDEF/BANKACCTFROM";
        public const string TRANSACTIONS_XPATH = "OFX/BANKMSGSRSV1/STMTTRNRS/TRNUID/STMTRS/CURDEF/BANKTRANLIST";
        public const string BANK_ID_XPATH = "//BANKID";
        public const string ACCOUNT_ID_XPATH = "//ACCTID";
        public const string ACCOUNT_TYPE_XPATH = "//ACCTTYPE";
        public const string TRANS_XPATH = "//STMTTRN";
        public const string TRANS_TYPE_XPATH = "//TRNTYPE";
        public const string TRANS_DATE_XPATH = "//DTPOSTED";
        public const string TRANS_AMOUNT_XPATH = "//TRNAMT";
        public const string TRANS_MEMO_XPATH = "//MEMO";
        public const string DOCUMENT_TYPE = "OFX";
    }
}
