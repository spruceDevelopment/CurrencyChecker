using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.Core.Contracts
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
