using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPaperless.BusinessLogic.Interfaces;

namespace NPaperless.BusinessLogic.Services
{
    public class CorrespondentService : ICorrespondentService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CorrespondentService));
    }
}
