using Anonymized.Assessment.Api.Models.Requests;
using FluentValidation;

namespace Anonymized.Assessment.Api.Validators
{
    /// <summary>
    /// Validator for <see cref="CreateAccountRequestModel"/>.
    /// </summary>
    public class AccountManagementValidator : AbstractValidator<CreateAccountRequestModel>
    {
        /// <summary>
        /// Initialize the <see cref="AccountManagementValidator"/>.
        /// </summary>
        public AccountManagementValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.InitialCredit)
                .GreaterThanOrEqualTo(0);
        }
    }
}