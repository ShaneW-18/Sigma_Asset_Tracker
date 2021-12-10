namespace SigmaTransactionAPI
{
    public class TransactionModel
    {
        public string Id { get; set; }

        public TransactionType TransType { get; set; }
        public decimal AmountTransacted { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        public enum TransactionType
        {
            BUY,
            SELL
        }
    }
}
