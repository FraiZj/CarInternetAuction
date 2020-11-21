using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public OperationDetails(bool succedeed = true, IEnumerable<ValidationResult> validationResults = null, object returnValue = null)
        {
            Succedeed = succedeed;
            ValidationResults = validationResults is null ? new List<ValidationResult>() : validationResults;
            ReturnValue = returnValue;
        }

        /// <summary>
        /// Consist the return value of the operation
        /// </summary>
        public object ReturnValue { get; private set; }

        /// <summary>
        /// Indicates the success of the operation
        /// </summary>
        public bool Succedeed { get; private set; }

        /// <summary>
        /// Operation errors
        /// </summary>
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }
    }
}
