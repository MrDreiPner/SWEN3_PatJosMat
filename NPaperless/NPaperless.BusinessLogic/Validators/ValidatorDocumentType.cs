using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPaperless.BusinessLogic.Entities;
using FluentValidation;

namespace NPaperless.BusinessLogic.Validators
{
    internal class ValidatorDocumentType : AbstractValidator<DocumentType>
    {
        public ValidatorDocumentType() {
            RuleSet("RequiredData", () =>
            {
                RuleFor(DocumentType => DocumentType.Id).NotEmpty().NotNull().WithMessage("ID required");
                RuleFor(DocumentType => DocumentType.Name).NotEmpty().NotNull().WithMessage("Name required");
            });
            RuleSet("ValidateAlgorithmPresentAndDocCount", () =>
            {
                RuleFor(DocumentType => DocumentType.MatchingAlgorithm).GreaterThan(0).WithMessage("MatchingAlgorithm must be greater than 0");
                RuleFor(DocumentType => DocumentType.DocumentCount).GreaterThanOrEqualTo(0).WithMessage("DocumentCount must be greater than or equal to 0");
            });
        }

    }
}
