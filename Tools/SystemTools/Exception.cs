using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahmadalli.Tools.SystemTools
{
    /// <summary>
    /// General tools for System.Exception class
    /// </summary>
    public static class ExceptionTools
    {
        /// <summary>
        /// Gives "Writer" Exception.ToString() and all it's InnerException's .ToString() result
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="Writer">The method that will recive the message.</param>
        public static void GetAllInnerExceptions(this Exception ex, Action<string> Writer)
        {
            if (ex == null || Writer == null)
            {
                return;
            }
            Writer(ex.ToString());
            GetAllInnerExceptions(ex.InnerException, Writer);
        }
    }
}
