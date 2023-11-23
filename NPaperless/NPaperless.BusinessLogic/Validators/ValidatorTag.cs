using FluentValidation;
using NPaperless.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Validators
{
    internal class ValidatorTag : AbstractValidator<Tag>
    {
        public ValidatorTag() {
            RuleSet("RequiredData", () =>
            {
                RuleFor(Tag => Tag.Id).NotEmpty().NotNull().WithMessage("ID required");
                RuleFor(Tag => Tag.Name).NotEmpty().NotNull().WithMessage("Name required");
                RuleFor(Tag => Tag.Color).NotEmpty().NotNull().WithMessage("Color required for Tag");
                RuleFor(Tag => Tag.IsInboxTag).NotEmpty().NotNull().WithMessage("IsInboxTag must be set");
            });
            RuleSet("ValidateAlgorithmPresentAndDocCount", () =>
            {
                RuleFor(Tag => Tag.MatchingAlgorithm).GreaterThan(0).WithMessage("MatchingAlgorithm must be greater than 0");
            });
        }
    }
}
