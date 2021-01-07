using Sgml;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Xunit;

namespace Nibo.Test
{
    public class AccountTest
    {
        private const string BANK_ACCOUNT_XPATH = "OFX/BANKMSGSRSV1/STMTTRNRS/TRNUID/STMTRS/CURDEF/BANKACCTFROM";
        private const string TRANSACTIONS_XPATH = "OFX/BANKMSGSRSV1/STMTTRNRS/TRNUID/STMTRS/CURDEF/BANKTRANLIST";
        private const string BANK_ID_XPATH = "//BANKID";
        private const string ACCOUNT_ID_XPATH = "//ACCTID";
        private const string ACCOUNT_TYPE_XPATH = "//ACCTTYPE";
        private const string TRANS_XPATH = "//STMTTRN";
        private const string TRANS_TYPE_XPATH = "//TRNTYPE";
        private const string TRANS_DATE_XPATH = "//DTPOSTED";
        private const string TRANS_AMOUNT_XPATH = "//TRNAMT";
        private const string TRANS_MEMO_XPATH = "//MEMO";
        private const string DOCUMENT_TYPE = "OFX";

        [Fact] 
        public void ReadAccountOFX()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "resources", "extrato1.ofx");

            var reader = new SgmlReader
            {
                InputStream = new StringReader(ClearHeader(file)),
                DocType = DOCUMENT_TYPE
            };

            var sw = new StringWriter();
            var xml = new XmlTextWriter(sw);
           
            while (!reader.EOF)
                xml.WriteNode(reader, true);

            xml.Flush();
            xml.Close();

            var temp = sw.ToString().TrimStart().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            
            var doc = new XmlDocument();
            doc.Load(new StringReader(String.Join("", temp)));

            var transNode = doc.SelectSingleNode(TRANSACTIONS_XPATH);
            GetTransactions(transNode);            
        }

        private void GetTransactions(XmlNode node)
        {
            var aux = new XmlDocument();
            aux.Load(new StringReader(node.OuterXml));

            var transactions = node.SelectNodes(TRANS_XPATH);

            foreach (XmlNode item in transactions)
            {
                var transtype = ReadNodeValue(item, TRANS_TYPE_XPATH);
                var transdate = ReadNodeValue(item, TRANS_DATE_XPATH);
                var transamou = ReadNodeValue(item, TRANS_AMOUNT_XPATH);
                var transmemo = ReadNodeValue(item, TRANS_MEMO_XPATH);

            }            
        }

        private string ReadNodeValue(XmlNode node, string xpath)
        {            
            var aux = new XmlDocument();
            aux.Load(new StringReader(node.OuterXml));

            var value = aux.SelectSingleNode(xpath);
            return value != null ? value.FirstChild.Value : string.Empty;
        }             

        private string ClearHeader(string file)
        {
            

            using var txtReader = new StreamReader(file);
            var content = txtReader.ReadToEnd();
            content = Regex.Replace(content, "OFXHEADER:100", "");
            content = Regex.Replace(content, "DATA:OFXSGML", "");
            content = Regex.Replace(content, "VERSION:102", "");
            content = Regex.Replace(content, "SECURITY:NONE", "");
            content = Regex.Replace(content, "ENCODING:USASCII", "");
            content = Regex.Replace(content, "CHARSET:1252", "");
            content = Regex.Replace(content, "COMPRESSION:NONE", "");
            content = Regex.Replace(content, "OLDFILEUID:NONE", "");
            content = Regex.Replace(content, "NEWFILEUID:NONE", "");
            txtReader.Close();

            return content;
        }
    }
}
