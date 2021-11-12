using System.Net;

namespace AuctimoTravels.Models
{
    public class ResponseMessage
    {
        public ResponseMessage(bool isSuccessful, HttpStatusCode statusCode, object returnObject)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            ReturnObject = returnObject;
        }

        public bool IsSuccessful { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object ReturnObject { get; set; }
    }
}
