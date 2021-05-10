using System.Collections.Generic;
using System.Security.Claims;

namespace uPromis.Service.Business
{
    public enum BusinessRuleResultSeverity
    {
        Info,
        Warning,
        Error
    }
    public interface IBusinessRules<T, TDTO>
    {
        List<BusinessRuleResult> Result { get; }
        void ApplyBusinessRules(T Record, TDTO DTORecord, ClaimsPrincipal user);
        /// <summary>
        /// Returns if the business rules application resulted in any errors
        /// </summary>
        /// <returns>True if errors</returns>
        bool HasErrors();
    /// <summary>
    /// Returns if the business rules application restuled in errors or warnings
    /// </summary>
    /// <returns>True if errors or warnings</returns>
        bool HasWarnings();
        /// <summary>
        /// Returns if the business rules application has run ok
        /// </summary>
        /// <returns>True if no errors or warnings. Information feedback may exist</returns>
        bool AllOk();

    }
}
