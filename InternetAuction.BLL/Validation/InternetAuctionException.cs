using System;
using System.Runtime.Serialization;

namespace InternetAuction.BLL.Validation
{
    /// <summary>
    /// Represents errors that occur during Internet Auction application execution
    /// </summary>
    [Serializable]
    public class InternetAuctionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InternetAuctionException class
        /// </summary>
        public InternetAuctionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the InternetAuctionException class with a specified error message
        /// </summary>
        /// <param name="message"></param>
        public InternetAuctionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InternetAuctionException class with a specified error 
        /// message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InternetAuctionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InternetAuctionException class with serialized data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected InternetAuctionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
