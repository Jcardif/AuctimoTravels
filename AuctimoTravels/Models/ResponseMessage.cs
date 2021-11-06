using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuctimoTravels.Models
{
    public class ResponseMessage
    {
        public ResponseMessage(object returnContent, HttpStatusCode code, bool isSuccessful)
        {
            ReturnContent = returnContent;
            Code = code;
            IsSuccessful = isSuccessful;
        }

        public object ReturnContent { get; }
        public HttpStatusCode Code { get; }
        public bool IsSuccessful { get; }

    }
}
