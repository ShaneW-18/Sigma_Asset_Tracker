
using System.Text.Json.Serialization;

namespace Sigma3.Objects
{
    public class Root
    {
        [JsonPropertyName("quoteResponse")]
        public QuoteResponseModel QuoteResponse;
    }
}