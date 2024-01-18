using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Interfaces
{
    public interface IOcrClient
    {
        string OcrPdf(Stream pdfStream);
    }
}
