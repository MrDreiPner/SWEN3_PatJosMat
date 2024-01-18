using FluentValidation;
using NPaperless.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Validators
{
    public class DocumentValidator : AbstractValidator<DocumentBL>
    {
        public DocumentValidator()
        {
            RuleSet("RequiredData", () =>
            {
                RuleFor(document => document.Title).NotEmpty().WithMessage("Title required");
            });
        }
    }
}
