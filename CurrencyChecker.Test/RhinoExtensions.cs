using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Test
{
    public static class RhinoExtensions
    {
        public static void ClearBehavior<T>(this T stub)
        {
            stub.BackToRecord(BackToRecordOptions.All);
            stub.Replay();
        }
    }
}
