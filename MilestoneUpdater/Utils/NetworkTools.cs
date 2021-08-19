
using MilestoneHotfixHelper.Utils;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Principal;

namespace HotfixHelperV2.Utils
{
    class NetworkTools
    {

        internal static bool IsLocalServer(string address)
        {
            IPHostEntry remoteHostEntry = Dns.GetHostEntry(address);
            IPHostEntry localHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            return remoteHostEntry.AddressList.Contains(localHostEntry.AddressList.FirstOrDefault());
        }



        internal static ManagementScope EstablishConnection(ServerInfo remoteInfo)
        {
            ConnectionOptions theConnection = new ConnectionOptions();
            theConnection.Authority = "ntlmdomain:" + remoteInfo.Domain;
            theConnection.Username = remoteInfo.UserName;
            theConnection.Password = remoteInfo.Password;
            return new ManagementScope("\\\\" + remoteInfo.Address + "\\root\\cimv2", theConnection);
        }


        internal static ManagementBaseObject SetShareParams(ManagementClass winShareClass, string filepath, string sharename)
        {
            var shareParams = winShareClass.GetMethodParameters("Create");
            shareParams["Path"] = filepath;
            shareParams["Name"] = sharename;
            shareParams["Type"] = 0;
            shareParams["Description"] = "Milestone HotFix";
            shareParams["MaximumAllowed"] = 10;
            shareParams["Password"] = null;

            NTAccount everyoneAccount = new NTAccount(null, "EVERYONE");
            SecurityIdentifier sid = (SecurityIdentifier)everyoneAccount.Translate(typeof(SecurityIdentifier));
            byte[] sidArray = new byte[sid.BinaryLength];
            sid.GetBinaryForm(sidArray, 0);

            ManagementObject everyone = new ManagementClass("Win32_Trustee");
            everyone["Domain"] = null;
            everyone["Name"] = "EVERYONE";
            everyone["SID"] = sidArray;

            ManagementObject dacl = new ManagementClass("Win32_Ace");
            dacl["AccessMask"] = 2032127;
            dacl["AceFlags"] = 3;
            dacl["AceType"] = 0;
            dacl["Trustee"] = everyone;

            ManagementObject securityDescriptor = new ManagementClass("Win32_SecurityDescriptor");
            securityDescriptor["ControlFlags"] = 4; //SE_DACL_PRESENT 
            securityDescriptor["DACL"] = new object[] { dacl };

            shareParams["Access"] = securityDescriptor;
            return shareParams;
        }


        internal static string ResolveHostNametoIP(string host)
        {
            IPHostEntry hostEntry;
            hostEntry = Dns.GetHostEntry(host);
            return hostEntry.AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
        }
    }
}
