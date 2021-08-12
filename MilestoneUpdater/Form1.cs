using System;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Security.Principal;
using System.Windows.Forms;

namespace MilestoneUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            TestParameters();                    /// REMOVE ON PRODUCTION !!!!
        }

        private void TestParameters()
        {
            textBoxRemoteIP.Text = "172.18.190.238";
            textBoxDomain.Text = ".";
            textBoxUserName.Text = "Administrator";
            textBoxPassword.Text = "Milestone1$";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoteServerInfo remoteInfo = GetRemoteInfo();
            CallProcess(remoteInfo);
        }

        private int CallProcess(RemoteServerInfo remoteInfo)
        {
            // Open Server Connection 
            String filepath = "c:\\Temp";
            String sharename = "Temp";
            //String file = "Milestone.Hotfix.202108031030.MS.21.12.12177.91.exe";
            String file = "dummyUpdater.exe";

            ConnectionOptions theConnection = new ConnectionOptions();
            theConnection.Username = remoteInfo.Domain + "\\" + remoteInfo.UserName;
            theConnection.Password = remoteInfo.Password;
            ManagementScope theScope = new ManagementScope("\\\\" + remoteInfo.Address + "\\root\\cimv2", theConnection);

            // create a share folder 
            var Win32_Process_Class = new ManagementClass(theScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
            object[] cmdMdTemp = { "cmd.exe /c md c:\\Temp" };
            var mdResult = Win32_Process_Class.InvokeMethod("Create", cmdMdTemp);
            Console.WriteLine(mdResult);

            // share the folder 

            var winShareClass = new ManagementClass(theScope, new ManagementPath("Win32_Share"), new ObjectGetOptions());
            ManagementBaseObject shareParams = SetShareParams(winShareClass, filepath, sharename);

            var outParams = winShareClass.InvokeMethod("Create", shareParams, null);

            Console.WriteLine(outParams.Properties["ReturnValue"].Value);

            // Copy the hotfix 
            try
            {
                File.Copy(
                @"C:\Temp\" + file,
                @"\\" + remoteInfo.Address + "\\" + sharename + "\\" + file);

            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine("Already exists");
                // should i check the chechsum date and all? yes, yes i should!!!!. don't be lazzy 

            }


            // RUN IT !!!! AND DO IT QUIET !!! 
            //String quiet_and_silent = " /quiet /install";
            String quiet_and_silent = "";

            object[] theProcessToRun = { "C:\\" + sharename + "\\" + file + quiet_and_silent, null, null, 0 };

            ManagementClass theClass = new ManagementClass(theScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
            var output = theClass.InvokeMethod("Create", theProcessToRun);
            String ProcID = theProcessToRun[3].ToString();

            Console.WriteLine(ErrorCodeToString(Convert.ToInt32(output)));
            Console.WriteLine(theProcessToRun[3]);

            WqlEventQuery wQuery = new WqlEventQuery("Select * From __InstanceDeletionEvent Within 1 Where TargetInstance ISA 'Win32_Process'");

            using (ManagementEventWatcher wWatcher = new ManagementEventWatcher(theScope, wQuery))
            {
                bool stopped = false;

                while (stopped == false)
                {
                    using (ManagementBaseObject MBOobj = wWatcher.WaitForNextEvent())
                    {
                        if (((ManagementBaseObject)MBOobj["TargetInstance"])["ProcessID"].ToString() == ProcID)
                        {
                            // the process has stopped
                            stopped = true;
                            Console.WriteLine("Update Process Finished");

                            // Upgrade Finish Check status 
                        }
                    }
                }

                wWatcher.Stop();
            }


            return 0;
        }

        private ManagementBaseObject SetShareParams(ManagementClass winShareClass, string filepath, string sharename)
        {
            var shareParams = winShareClass.GetMethodParameters("Create");
            shareParams["Path"] = filepath;
            shareParams["Name"] = sharename;
            shareParams["Type"] = 0;
            shareParams["Description"] = "CMC Bootstrap Share";
            //shareParams["MaximumAllowed"] = 2;
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

        private RemoteServerInfo GetRemoteInfo()
        {
            return new RemoteServerInfo()
            {
                Address = textBoxRemoteIP.Text,
                Domain = textBoxDomain.Text,
                UserName = textBoxUserName.Text,
                Password = textBoxPassword.Text
            };
        }


        private class RemoteServerInfo
        {
            public String Address { get; set; }
            public String Domain { get; set; }
            public String UserName { get; set; }
            public String Password { get; set; }
        }


        private String ErrorCodeToString(int errorCode)
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

    }

}
