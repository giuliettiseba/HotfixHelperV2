using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotfixHelperV2.Utils
{
    class CodeToStringConverters
    {


        internal static String ErrorCodeToString(int errorCode)
        {
            switch (errorCode)
            {
                case 0: return "Successful Completion";
                case 2: return "Access Denied";
                case 3: return "Insufficient Privilege";
                case 8: return "Unknown Failure";
                case 9: return "Path not found";
                case 21: return "Invalid parameter";
                default: return "ERROR";
            }
        }

        internal static String ShareFolderErrorCodeToString(int errorCode)
        {
            switch (errorCode)
            {
                case 0: return "Successful Completion";
                case 2: return "Access Denied";
                case 8: return "Unknown failure";
                case 9: return "Invalid name";
                case 10: return "Invalid level";
                case 21: return "Invalid parameter";
                case 22: return "Duplicate share";
                case 23: return "Redirected path";
                case 24: return "Unknown device or directory";
                case 25: return "Net name not found";
                default: return "ERROR";
            }
        }

    }
}
