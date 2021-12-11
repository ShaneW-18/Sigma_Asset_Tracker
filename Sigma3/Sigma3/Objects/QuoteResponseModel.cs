using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sigma3.Objects
{
    public class QuoteResponseModel
    {
        [JsonPropertyName("result")]
        public List<SecurityModel> Result;

        [JsonPropertyName("error")]
        public object Error;
    }
}
