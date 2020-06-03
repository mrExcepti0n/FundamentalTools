using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider.Exceptions
{
    [DataContract]
    public class CustomException
    {
        public CustomException(string msg, Guid errorId)
        {
            Message = msg;
            ErrorId = errorId;

        }

        [DataMember]
        public string Message;


        [DataMember]
        public Guid ErrorId { get; set; }
    }
}
