using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma3.Objects
{
    public class Transaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("transType")]
        public TransactionType TransType { get; set; }

        [JsonProperty("amountTransacted")]
        public decimal AmountTraded { get; set; } 
        
        [JsonProperty("userId")]
        public string UserId { get; set; } 

        [JsonProperty("securityTraded")]
        public string SecurityTraded { get; set; }

        [JsonProperty("dateTime")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("pricePerSecurity")]
        public decimal PricePerSecurity { get; set; }
    }

    public enum TransactionType
    {
        BUY,
        SELL
    }

}
