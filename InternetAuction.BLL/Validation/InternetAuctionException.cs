using System;
using System.Runtime.Serialization;

namespace InternetAuction.BLL.Validation
{
    [Serializable]
    public class InternetAuctionException : Exception
    {
        public InternetAuctionException()
        {
        }

        public InternetAuctionException(string message) : base(message)
        {
        }

        public InternetAuctionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InternetAuctionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
