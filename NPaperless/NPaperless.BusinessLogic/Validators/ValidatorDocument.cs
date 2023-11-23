using FluentValidation;
using NPaperless.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Validators
{
    internal class ValidatorDocument : AbstractValidator<Document>
    {
        public ValidatorDocument()
        {
            RuleSet("RequiredData", () =>
            {
                RuleFor(DocumentType => DocumentType.Title).NotEmpty().NotNull().WithMessage("Title required");
                RuleFor(DocumentType => DocumentType.UploadDocument).NotEmpty().NotNull().WithMessage("Document required");
            });
        }
    }
}
