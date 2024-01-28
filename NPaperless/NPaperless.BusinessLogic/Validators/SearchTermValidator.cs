using FluentValidation;
using NPaperless.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Validators
{
    public class SearchTermValidator : AbstractValidator<string>
    {
        public SearchTermValidator() 
        {
            RuleSet("RequiredData", () =>
            {
                RuleFor(term => term).NotNull().WithMessage("Search Term required");
                RuleFor(term => term).NotEmpty().WithMessage("Search Term required");
                RuleFor(term => term).NotEqual(" ").WithMessage("Search Term required");
                RuleFor(term => term).NotEqual("").WithMessage("Search Term required");
            });
        }
    }
}
