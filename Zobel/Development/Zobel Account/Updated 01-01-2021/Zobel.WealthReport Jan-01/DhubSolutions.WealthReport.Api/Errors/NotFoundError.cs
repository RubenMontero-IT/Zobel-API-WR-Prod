using System.Net;

namespace DhubSolutions.WealthReport.Api.Errors
{
    public class NotFoundError : ApiError
    {
        public NotFoundError()
            : base(404, $"{HttpStatusCode.NotFound}")
        { }


        public NotFoundError(string message)
            : base(404, $"{HttpStatusCode.NotFound}", message)
        { }
    }
}
