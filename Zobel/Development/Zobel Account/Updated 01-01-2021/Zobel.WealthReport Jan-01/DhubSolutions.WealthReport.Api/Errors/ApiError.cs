using Newtonsoft.Json;

namespace DhubSolutions.WealthReport.Api.Errors
{
    public class ApiError
    {
        public int Code { get; private set; }

        public string Description { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiError(int code, string description)
        {
            Code = code;
            Description = description;
        }

        public ApiError(int code, string description, string message)
            : this(code, description)
        {
            Message = message;
        }
    }

}
