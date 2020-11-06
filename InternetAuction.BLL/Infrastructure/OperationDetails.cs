using System.Collections.Generic;

namespace InternetAuction.BLL.Infrastructure
{
    /// <summary>
    /// Represents an operation details class
    /// </summary>
    public class OperationDetails
    {
        /// <summary>
        /// Initializes an instance of the operation details class
        /// </summary>
        /// <param name="succedeed"></param>
        /// <param name="errors"></param>
        public OperationDetails(bool succedeed = true, IEnumerable<string> errors = null)
        {
            Succedeed = succedeed;
            Errors = errors is null ? new List<string>() : errors;
        }

        /// <summary>
        /// Is operation succedeed
        /// </summary>
        public bool Succedeed { get; private set; }

        /// <summary>
        /// Operation errors
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }
    }
}
