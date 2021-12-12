using Newtonsoft.Json;

namespace SigmaTransactionAPI
{
    public class TransactionModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("transType")]
        public string TransType { get; set; }

        [JsonProperty("amountTransacted")]
        public decimal AmountTransacted { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("securityTraded")]
        public string SecurityTraded { get; set; }

        [JsonProperty("dateTime")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("pricePerSecurity")]
        public decimal PricePerSecurity { get; set; }
     
    }
}
