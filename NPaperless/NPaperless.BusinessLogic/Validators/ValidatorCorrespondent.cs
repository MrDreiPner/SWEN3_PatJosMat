using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPaperless.BusinessLogic.Entities;
using FluentValidation;

namespace NPaperless.BusinessLogic.Validators
{
    public class ValidatorCorrespondent : AbstractValidator<CorrespondentBL>
    {
        public ValidatorCorrespondent() {
            RuleSet("RequiredData", () =>
            {
                RuleFor(Correspondent => Correspondent.Id).NotEmpty().NotNull().WithMessage("ID required");
                RuleFor(Correspondent => Correspondent.Name).NotEmpty().NotNull().WithMessage("Name required");
            });
            RuleSet("ValidateAlgorithmPresentAndDocCount", () =>
            {
                RuleFor(correspondent => correspondent.MatchingAlgorithm).GreaterThan(0).WithMessage("MatchingAlgorithm must be greater than 0");
                RuleFor(correspondent => correspondent.DocumentCount).GreaterThanOrEqualTo(0).WithMessage("DocumentCount must be greater than or equal to 0");
            });
        }
    }
}
