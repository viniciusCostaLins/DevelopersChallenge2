using Nibo.Domain.Entities.Transactions;
using Nibo.Domain.Interfaces.Repositories;
using Nibo.Domain.Interfaces.Services;
using Nibo.Domain.Utils;
using Sgml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Nibo.Domain.Services
{
    public class TransactionService : BaseService, ITransactionServices
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository,
                                 INotifier notifier) : base(notifier)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Add(IEnumerable<Transaction> transactions)
        {
            await _transactionRepository.AddRange(transactions);
            return true;
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _transactionRepository?.Dispose();
        }

        public IEnumerable<Transaction> ReadOfxFile(string filePath)
        {
            var file = filePath;

            var reader = new SgmlReader
            {
                InputStream = new StringReader(ClearHeader(file)),
                DocType = NiboConstants.DOCUMENT_TYPE
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

            var transNode = doc.SelectSingleNode(NiboConstants.TRANSACTIONS_XPATH);
            return GetTransactions(transNode);
        }

        private List<Transaction> GetTransactions(XmlNode node)
        {
            var aux = new XmlDocument();
            aux.Load(new StringReader(node.OuterXml));

            var transactions = node.SelectNodes(NiboConstants.TRANS_XPATH);

            var output = new List<Transaction>();
            foreach (XmlNode item in transactions)
            {
                var trans = new Transaction
                {
                    Amount = Convert.ToDecimal(ReadNodeValue(item, NiboConstants.TRANS_AMOUNT_XPATH)) / 100,
                    DatePosted = ReadNodeValue(item, NiboConstants.TRANS_DATE_XPATH).ToDate(),
                    TransactionType = ReadNodeValue(item, NiboConstants.TRANS_TYPE_XPATH) == "DEBIT" ? TransactionType.Debit : TransactionType.Credit,
                    Memo = ReadNodeValue(item, NiboConstants.TRANS_MEMO_XPATH)
                };

                output.Add(trans);
            }

            return output;
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

        public Task<bool> Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
