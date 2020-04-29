using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Model
{
    public class ServiceResponse: HttpResponseMessage
    {
        public ServiceResponse() : base()
        {
            StatusCode = System.Net.HttpStatusCode.OK;
        }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
