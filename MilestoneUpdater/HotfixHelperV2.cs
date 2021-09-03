using ConfigApi;
using HotfixHelperV2.Utils;
using MilestoneHotfixHelper.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.ConfigurationAPI;
using VideoOS.Platform.SDK.Platform;

namespace MilestoneUpdater
{
    public partial class HotfixHelperV2 : Form
    {

        // Join the dark theme 
        readonly Color TEXTBACKCOLOR = System.Drawing.ColorTranslator.FromHtml("#252526");
        readonly Color BACKCOLOR = System.Drawing.ColorTranslator.FromHtml("#2D2D30");
        readonly Color INFOCOLOR = System.Drawing.ColorTranslator.FromHtml("#1E7AD4");
        readonly Color MESSAGECOLOR = System.Drawing.ColorTranslator.FromHtml("#86A95A");
        readonly Color DEBUGCOLOR = System.Drawing.ColorTranslator.FromHtml("#DCDCAA");
        readonly Color ERRORCOLOR = System.Drawing.ColorTranslator.FromHtml("#B0572C");

        // Jsons Repositories
        private const string HOTFIXLIST = "https://download.milestonesys.com/sgiu/hotfixHelper/hotfixList.json";
        private const string DEVICEPACKLIST = "https://download.milestonesys.com/sgiu/hotfixHelper/devicePackList.json";

        // Folder locations 
        private static String LOCALFOLDER = @"C:\ProgramData\Milestone\HotfixInstaller";
        private static String REMOTEFOLDER = @"C:\MilestoneHotfix";
        private static String REMOTESHARENAME = "MilestoneHotfix";

        string ms_Version; // DO I NEED THIS GLOBAL ??
        private string downloadedDP; // DO I NEED THIS GLOBAL // this is the DP that is ready to deploy 

        public HotfixHelperV2()
        {
            InitializeComponent();

            // Apply theme 

            this.textBox_Console.BackColor = TEXTBACKCOLOR;
            this.BackColor = BACKCOLOR;
            this.groupBox1.BackColor = BACKCOLOR;
            this.groupBox2.BackColor = BACKCOLOR;
            this.groupBox3.BackColor = BACKCOLOR;
            this.groupBox4.BackColor = BACKCOLOR;

            //TestParametersLocal();          /// REMOVE IN PRODUCTION !!!!
            TestParameters();                  /// REMOVE IN PRODUCTION !!!!
        }


        protected override void OnHandleCreated(EventArgs e)
        {

        }

        private void TestParametersLocal()
        {
            textBoxMSAddress.Text = "172.28.131.235";
            textBoxMSDomain.Text = ".";
            textBoxMSUser.Text = "Administrator";
            textBoxMSPass.Text = "Milestone1$";

            textBoxAllDomain.Text = "MEX-LAB";
            textBoxAllUser.Text = "SGIU";
            textBoxAllPass.Text = "Milestone1$";
        }
        private void TestParameters()
        {
            textBoxMSAddress.Text = "10.1.0.192";
            textBoxMSDomain.Text = "MEX-LAB";
            textBoxMSUser.Text = "SGIU";
            textBoxMSPass.Text = "Milestone1$";

            textBoxAllDomain.Text = "MEX-LAB";
            textBoxAllUser.Text = "SGIU";
            textBoxAllPass.Text = "Milestone1$";
        }



        private void GetRecordingServers_Button_Click(object sender, EventArgs e)
        {

            // Clear the dataGrids 
            serversDataGridView.Rows.Clear();
            dataGridViewHotFixList.Rows.Clear();

            ServerInfo ms_Info = new ServerInfo()
            {
                Address = textBoxMSAddress.Text,
                Domain = textBoxMSDomain.Text,
                UserName = textBoxMSUser.Text,
                Password = textBoxMSPass.Text
            };

            // Build credentials
            NetworkCredential nc = new NetworkCredential(ms_Info.UserName, ms_Info.Password, ms_Info.Domain);
            Uri uri = new Uri("http://" + ms_Info.Address);
            ConfigApiClient _configApiClient = new ConfigApiClient();

            WriteInConsole("Connecting to: " + uri + ".", LogType.message);
            bool isConnected = false;
            try
            {
                // Attemp to connect
                isConnected = MilestoneApiHelper.Login(uri, nc, _configApiClient);
            }
            catch (ServerNotFoundMIPException snfe)
            {
                WriteInConsole("Server not found: " + snfe.Message + ".", LogType.error);
            }
            catch (InvalidCredentialsMIPException ice)
            {
                WriteInConsole("Invalid credentials: " + ice.ToString() + ".", LogType.error);
            }
            catch (Exception ex)
            {
                WriteInConsole("Other error connecting to " + ms_Info.Address + " : " + ex.ToString() + ".", LogType.error);
            }

            if (isConnected)
            {
                WriteInConsole("Connection to : " + uri + " established.", LogType.message);

                // If connected get the managment server 

                ConfigurationItem managmentServer = _configApiClient.GetItem("/");
                string ms_Name = Array.Find(managmentServer.Properties, ele => ele.Key == "Name").Value;
                ms_Version = Array.Find(managmentServer.Properties, ele => ele.Key == "Version").Value;

                labelMSName.Text = ms_Name;
                labelMSVer.Text = ms_Version;

                // Get recording Server folder

                ConfigurationItem recordingServerFolder = _configApiClient.GetItem("/RecordingServerFolder");
                MilestoneApiHelper.FillChildren(recordingServerFolder, _configApiClient);

                List<ServerInfo> recordingServerList = new List<ServerInfo>();

                // Extract the needed information from Recording Servers and add them to a list 
                foreach (ConfigurationItem recordingServer in recordingServerFolder.Children)
                {
                    recordingServerList.Add(new ServerInfo()
                    {
                        DisplayName = recordingServer.DisplayName,
                        Address = Array.Find(recordingServer.Properties, ele => ele.Key == "HostName").Value,
                        ServerType = HotFixType.RecordingServer.ToString(),
                        Domain = ms_Info.Domain,
                        UserName = ms_Info.UserName,
                        Password = ms_Info.Password,

                    });
                }

                // ADD RECORDING SERVERS TO THE DATAGRID
                PopulateServersDataGrid(recordingServerList);

                // GET MOBILE SERVERS 
                ConfigurationItem mipKindFolder = _configApiClient.GetItem("/MIPKindFolder");
                MilestoneApiHelper.FillChildren(mipKindFolder, _configApiClient);
                ConfigurationItem mobileServers = Array.Find(mipKindFolder.Children, ele => ele.DisplayName == "Mobile Servers");
                MilestoneApiHelper.FillChildren(mobileServers, _configApiClient);
                ConfigurationItem itemFolder = Array.Find(mobileServers.Children, ele => ele.ItemType == "MIPItemFolder");
                MilestoneApiHelper.FillChildren(itemFolder, _configApiClient);

                List<ServerInfo> mobileServerList = new List<ServerInfo>();

                // Get info and add the MoSs to the list 
                foreach (ConfigurationItem mobileServer in itemFolder.Children)
                {
                    mobileServerList.Add(new ServerInfo()
                    {
                        DisplayName = Array.Find(mobileServer.Properties, ele => ele.Key == "Name").Value,
                        Address = Array.Find(mobileServer.Properties, ele => ele.Key == "ServerIdName").Value,
                        ServerType = HotFixType.MobileServer.ToString(),
                        Domain = ms_Info.Domain,
                        UserName = ms_Info.UserName,
                        Password = ms_Info.Password,
                    });
                }

                PopulateServersDataGrid(mobileServerList);

                /// TODO DO: ADD ES 
                /// Could not find ES address with the SDK, i could easyly find it in the database, but meh 

                MilestoneApiHelper.Logout(_configApiClient);
                groupBoxHotfixes.Enabled = true;
            }
            else
            {
                {
                    WriteInConsole("Connection to : " + uri + " failed.", LogType.error);
                }
            }
        }

        /// <summary>
        /// Add the servers on a list of servers in the datagrid.
        /// </summary>
        /// <param name="serverList"></param>
        private void PopulateServersDataGrid(List<ServerInfo> serverList)
        {
            foreach (ServerInfo server in serverList)
            {
                int n = serversDataGridView.Rows.Add();
                serversDataGridView.Rows[n].Cells["DisplayName"].Value = server.DisplayName;
                serversDataGridView.Rows[n].Cells["Address"].Value = server.Address;
                serversDataGridView.Rows[n].Cells["ServerType"].Value = server.ServerType;
                serversDataGridView.Rows[n].Cells["Domain"].Value = server.Domain;
                serversDataGridView.Rows[n].Cells["User"].Value = server.UserName;
                serversDataGridView.Rows[n].Cells["Password"].Value = server.Password;
                serversDataGridView.Rows[n].Cells["Selected"].Value = true;
            }
        }


        // HIDE PASSWORD ON DATAGRID
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.Value != null)
            {
                e.Value = new String('*', e.Value.ToString().Length);
            }
        }

        /// <summary>
        /// Changes the credentials of all the servers (could be useful if the user to log in to the MS is not administrator on the servers) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Change_All_Credencials_Button_Click(object sender, EventArgs e)
        {
            string _user = textBoxAllUser.Text;
            string _pass = textBoxAllPass.Text;
            string _domain = textBoxAllDomain.Text;

            for (int i = 0; i < serversDataGridView.Rows.Count; i++)
            {
                serversDataGridView.Rows[i].Cells["Domain"].Value = _domain;
                serversDataGridView.Rows[i].Cells["User"].Value = _user;
                serversDataGridView.Rows[i].Cells["Password"].Value = _pass;
            }
        }


        /// <summary>
        /// Start hotfix installation 
        /// transverse servers dataGrid and get instalation parametes from each row. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void InstallHotfixesClick(object sender, EventArgs e)
        {
            var listOfTasks = new List<Task>();

            foreach (DataGridViewRow row in serversDataGridView.Rows)
            {
                if ((bool)row.Cells["Selected"].Value == true)
                {
                    ServerInfo remoteInfo = new ServerInfo()
                    {
                        Address = NetworkTools.ResolveHostNametoIP(row.Cells["Address"].Value.ToString()),
                        Domain = row.Cells["Domain"].Value.ToString(),
                        UserName = row.Cells["User"].Value.ToString(),
                        Password = row.Cells["Password"].Value.ToString(),
                        ServerType = row.Cells["ServerType"].Value.ToString(),
                    };

                    Cursor.Current = Cursors.WaitCursor;                               // WaitCursor

                    foreach (DataGridViewRow _row in dataGridViewHotFixList.Rows)
                    {
                        if (_row.Cells["LocalLocation"].Value != null && _row.Cells["HotfixType"].Value.ToString() == remoteInfo.ServerType) // Chose the correct hotfix according to the server type
                        {
                            String file = _row.Cells["HotfixFile"].Value.ToString();
                            String filePath = _row.Cells["LocalLocation"].Value.ToString();
                            listOfTasks.Add(new Task(() => HotfixInstallerWorker(remoteInfo, file), new CancellationToken()));
                        }
                    }
                }
            }

            int _maxDegreeOfParallelism = (int)numericUpDown_MaxDegreeOfParallelism.Value;

            Task tasks = StartAndWaitAllThrottledAsync(listOfTasks, _maxDegreeOfParallelism, -1).ContinueWith(result =>
            {
                WriteInConsole("Task(s) completed", LogType.message);
            });

            Cursor.Current = Cursors.Default;                               // Restore cursor

        }

        private void HotfixInstallerWorker(ServerInfo remoteInfo, string file)
        {
            WriteInConsole("Start updater on " + remoteInfo.Address, LogType.message);

            if (NetworkTools.IsLocalServer(remoteInfo.Address))                                                  // If is not remote just install 
            {
                ManagementScope theScope = new ManagementScope(@"\\LOCALHOST\root\cimv2");
                ExecuteFile(remoteInfo, theScope, LOCALFOLDER + "\\" + file, " /quiet /install");
            }
            else
            {
                ManagementScope theScope = NetworkTools.EstablishConnection(remoteInfo);
                CreateRemoteFolder(remoteInfo, theScope);
                ShareRemoteFolder(remoteInfo, theScope);
                CopyFile(remoteInfo, file);
                ExecuteFile(remoteInfo, theScope, REMOTEFOLDER + "\\" + file, " /quiet /install");              // RUN IT !!!! AND DO IT QUIET !!! 
                UnshareRemoteFolder(remoteInfo, theScope);
                DeleteRemoteFolder(remoteInfo, theScope);
            }
        }

        /// <summary>
        /// Get Hotfix list from online json 
        /// prerequisite: server version 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e2"></param>
        private void FindHotfixes_Click(object sender, EventArgs e2)
        {
            dataGridViewHotFixList.Rows.Clear();


            //            ms_Version = "21.1";   /// TEST VALUES REMOVE ON PRODUCTION

            //            ms_Version = "20.3";   /// TEST VALUES REMOVE ON PRODUCTION
            //            ms_Version = "20.2";   /// TEST VALUES REMOVE ON PRODUCTION
            //            ms_Version = "20.1";   /// TEST VALUES REMOVE ON PRODUCTION

            //            ms_Version = "13.3";   /// TEST VALUES REMOVE ON PRODUCTION  -- MOS and ES not pulling
            //            ms_Version = "13.2";   /// TEST VALUES REMOVE ON PRODUCTION  -- MOS and ES not pulling
            //            ms_Version = "13.1";   /// TEST VALUES REMOVE ON PRODUCTION -- MOS and ES not pulling

            //            ms_Version = "12.3";   /// TEST VALUES REMOVE ON PRODUCTION  -- ONLY RS WORKS 
            //            ms_Version = "12.2";   /// TEST VALUES REMOVE ON PRODUCTION  -- MOS and ES not pulling
            //            ms_Version = "12.1";   /// TEST VALUES REMOVE ON PRODUCTION  -- MOS and ES not pulling

            if (ms_Version == null)
            {
                WriteInConsole("Connect the server version", LogType.error);
                return;
            }

            // Hotfixes are updated all the time and the name of the file change.
            // The manifest contains only the path to the hotfix folders the name is get from the HTML page
            // exploring the page DOM i can get the links to download the latest hotfix
            using (WebClient wc = new WebClient())
            {
                try
                {
                    // download the manifest 
                    var json = wc.DownloadString(HOTFIXLIST);
                    WriteInConsole("Got hotfix list from : " + HOTFIXLIST, LogType.info);

                    // Convert json to object
                    List<Hotfix> items = JsonConvert.DeserializeObject<List<Hotfix>>(json);

                    // Find the version
                    Hotfix hotfix = items.Find(elem => ms_Version.Contains(elem.Version));


                    //// BOILERPLATE CODE HERE. NEEDS REFACTOR. 

                    // Get RS hotfix 
                    string url = hotfix.RS;
                    HotfixFile hotfixFile = IdentifyHotfix(Path.GetFileName(GetFilesFrom_HTML_DOM(url)[0]));
                    if (hotfixFile != null)
                    {
                        hotfixFile.Url = url;
                        AddHotfixFileToDataGrid(hotfixFile);
                    }

                    /// TODO:  ADD DATABASE HOTFIXES TOO
                    /// Extend GetFilesFrom_HTML_DOM

                    // Get MS hotfix
                    url = hotfix.MS;
                    foreach (var f in GetFilesFrom_HTML_DOM(url))
                    {
                        hotfixFile = IdentifyHotfix(Path.GetFileName(f));
                        if (hotfixFile != null)
                        {
                            hotfixFile.Url = url;
                            AddHotfixFileToDataGrid(hotfixFile);
                        }
                    }


                    // Get ES hotfix
                    url = hotfix.ES;
                    hotfixFile = IdentifyHotfix(Path.GetFileName(GetFilesFrom_HTML_DOM(url)[0]));
                    if (hotfixFile != null)
                    {
                        hotfixFile.Url = url;
                        AddHotfixFileToDataGrid(hotfixFile);
                    }

                    // Get MoS hotfix
                    url = hotfix.MOS;
                    hotfixFile = IdentifyHotfix(Path.GetFileName(GetFilesFrom_HTML_DOM(url)[0]));
                    if (hotfixFile != null)
                    {
                        hotfixFile.Url = url;
                        AddHotfixFileToDataGrid(hotfixFile);
                    }
                }
                catch (Exception ex)
                {
                    WriteInConsole(ex.Message, LogType.error);
                }
            }
        }


        /// <summary>
        /// Given a http address explore the DOM and returns the links of the hotfixes, 
        /// Also return the Release and ReadMe files it will be usefull for show to the user 
        /// what is going to be installed
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private String[] GetFilesFrom_HTML_DOM(string folder)
        {
            String[] result = new String[10];

            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(folder);
                WebBrowser webBrowser1 = new WebBrowser();
                webBrowser1.DocumentText = htmlCode;

                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(5);
                }

                if (webBrowser1.Document != null)
                {
                    int i = 0;
                    HtmlElementCollection elems = webBrowser1.Document.GetElementsByTagName("a");
                    foreach (HtmlElement elem in elems)
                    {
                        String hrefStr = elem.GetAttribute("href");
                        if (hrefStr.Contains("Hotfix") || hrefStr.Contains("Installer") || hrefStr.Contains("DBHotfix"))
                        {
                            if (!hrefStr.Contains("Release") && !hrefStr.Contains("ReadMe"))
                            {
                                result[i++] = GetFileName(hrefStr);
                            }
                        }
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Manualy choose a hotfix from the local storage
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectHotFixLocalStorage_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;

                    HotfixFile hotfixFile = IdentifyHotfix(Path.GetFileName(fileName));

                    string fileNameInstallationDir = LOCALFOLDER + "\\" + Path.GetFileName(fileName);           // Copy file to LOCALFOLDER
                    if (!File.Exists(fileNameInstallationDir)) File.Copy(fileName, fileNameInstallationDir);

                    hotfixFile.Url = Path.GetFullPath(fileName);
                    hotfixFile.LocalLocation = fileNameInstallationDir;

                    AddHotfixFileToDataGrid(hotfixFile);
                }
            }
        }


        /// <summary>
        /// Aff a hotfix to the hotfix datagrid
        /// </summary>
        /// <param name="hotfixFile"></param>
        private void AddHotfixFileToDataGrid(HotfixFile hotfixFile)
        {
            int n = dataGridViewHotFixList.Rows.Add();
            dataGridViewHotFixList.Rows[n].Cells["HotfixType"].Value = hotfixFile.Type;
            dataGridViewHotFixList.Rows[n].Cells["HotfixVersion"].Value = hotfixFile.Version;
            dataGridViewHotFixList.Rows[n].Cells["HotfixServerVersion"].Value = hotfixFile.ServerVersion;
            dataGridViewHotFixList.Rows[n].Cells["HotfixUrl"].Value = hotfixFile.Url;
            dataGridViewHotFixList.Rows[n].Cells["HotfixFile"].Value = hotfixFile.File;
            dataGridViewHotFixList.Rows[n].Cells["LocalLocation"].Value = hotfixFile.LocalLocation;
        }


        /// <summary>
        /// Many names, no conventions like a nightmare. 
        /// With a little effort we can identify with kind of hotfix is analysing the name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private HotfixFile IdentifyHotfix(string fileName)
        {
            if (fileName == null) return null;

            //DBHotfix399827.exe
            if (fileName.Contains("DBHotfix"))
            {
                return new HotfixFile()
                {
                    Version = "X",
                    Type = HotFixType.ManagementServer,
                    ServerVersion = "X",
                    File = fileName
                };
            }
            else

            //MilestoneXProtectMobileServerInstaller_x64.exe
            if (fileName.Contains("MobileServer"))
            {
                return new HotfixFile()
                {
                    Version = "X",
                    Type = HotFixType.MobileServer,
                    ServerVersion = "X",
                    File = fileName
                };
            }
            else
            {
                //       0       1          2      3   45
                //	Milestone.Hotfix.202107192213.RS.21.12.12177.80.exe
                //  Milestone.Hotfix.202105100826.RS.20.31.93.182.exe
                string[] slides = fileName.Split('.');

                HotFixType hotfixtype;
                switch (slides[3])
                {
                    case "RS":
                        hotfixtype = HotFixType.RecordingServer;
                        break;

                    case "MS":
                        hotfixtype = HotFixType.ManagementServer;
                        break;

                    case "ES":
                        hotfixtype = HotFixType.EventServer;
                        break;

                    default:
                        hotfixtype = HotFixType.Unknown;
                        break;
                }

                return new HotfixFile()
                {
                    Version = slides[2],
                    Type = hotfixtype,
                    ServerVersion = slides[4] + "." + slides[5],
                    File = fileName
                };
            }
        }


        /// <summary>
        /// Aux method to get the file name from a address
        /// </summary>
        /// <param name="hrefStr"></param>
        /// <returns></returns>
        private string GetFileName(string hrefStr)
        {
            return hrefStr.Substring(hrefStr.LastIndexOf("/") + 1);
        }


        /// <summary>
        /// Actually this is the method that will download the hotfixes 
        /// with the information gained from the manifest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetHotfixes_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewHotFixList.Rows)
            {

                //CHECK DOWNLOAD FOLDER 
                if (!Directory.Exists(LOCALFOLDER))
                {
                    Directory.CreateDirectory(LOCALFOLDER);
                }

                string remoteUri = row.Cells["HotfixUrl"].Value.ToString();
                string fileName = row.Cells["HotfixFile"].Value.ToString(), myStringWebResource = null;
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                // Concatenate the domain with the Web resource filename.
                myStringWebResource = remoteUri + fileName;
                WriteInConsole("Downloading File " + fileName + " from  " + myStringWebResource + "...", LogType.message);
                // Download the Web resource and save it into the current filesystem folder.
                myWebClient.DownloadFile(myStringWebResource, LOCALFOLDER + "\\" + fileName);
                WriteInConsole("Successfully Downloaded File " + fileName + " from  " + myStringWebResource + "...", LogType.message);
                row.Cells["LocalLocation"].Value = LOCALFOLDER;
            }
        }


        /// <summary>
        /// Populate the DP comboBox with the online manifest 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDevicePackButton_Click(object sender, EventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString(DEVICEPACKLIST);
                    List<DevicePack> items = JsonConvert.DeserializeObject<List<DevicePack>>(json);
                    var source = new Dictionary<string, string>();

                    foreach (var item in items)
                    {
                        source.Add(Path.GetFileName(item.link), item.link);
                    }
                    device_Pack_comboBox.Items.Clear();
                    device_Pack_comboBox.DataSource = new BindingSource(source, null);
                    device_Pack_comboBox.DisplayMember = "Key";
                    device_Pack_comboBox.ValueMember = "Value";
                }
                catch (Exception ex)
                {
                    WriteInConsole(ex.Message, LogType.error);
                }
            }
        }



        /// <summary>
        /// DP is heavy, download in background and show the progress in a progress bar 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="name"></param>
        public void DownLoadFileInBackground4(string address, string name)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(address);

            client.DownloadFileCompleted += DownloadFileCompleted(name);

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);

            client.DownloadFileAsync(uri, LOCALFOLDER + "\\" + name);
        }

        /// <summary>
        /// Download completed callback function
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AsyncCompletedEventHandler DownloadFileCompleted(string name)
        {
            Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
            {
                downloadedDP = name;
            };
            return new AsyncCompletedEventHandler(action);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DP_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            downloadedDP = null;
            progressBar1.Value = 0;
        }

        /// <summary>
        /// Progress callback function 
        /// Update the progressbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }


        /// <summary>
        /// Download the selected DP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadDP_Button_Click(object sender, EventArgs e)
        {
            string link = device_Pack_comboBox.SelectedValue.ToString();
            string name = link.Substring(link.LastIndexOf("/") + 1);
            DownLoadFileInBackground4(link, name);
        }

        /// <summary>
        /// Install the downloaded DP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Install_Device_Pack_Button_Click(object sender, EventArgs e)
        {
            var listOfTasks = new List<Task>();

            foreach (DataGridViewRow row in serversDataGridView.Rows)
            {
                if ((bool)row.Cells["Selected"].Value == true)
                {
                    ServerInfo remoteInfo = new ServerInfo()
                    {
                        Address = NetworkTools.ResolveHostNametoIP(row.Cells["Address"].Value.ToString()),
                        Domain = row.Cells["Domain"].Value.ToString(),
                        UserName = row.Cells["User"].Value.ToString(),
                        Password = row.Cells["Password"].Value.ToString(),
                        ServerType = row.Cells["ServerType"].Value.ToString(),
                    };

                    Cursor.Current = Cursors.WaitCursor;                               // Restore cursor

                    listOfTasks.Add(new Task(() => CallInstallDevicePackProcess(remoteInfo, downloadedDP)));   /// The call to start the remothe algoritm 
                }
            }

            int _maxDegreeOfParallelism = (int)numericUpDown_MaxDegreeOfParallelism.Value;

            Task tasks = StartAndWaitAllThrottledAsync(listOfTasks, _maxDegreeOfParallelism, -1).ContinueWith(result =>
            {
                WriteInConsole("Task(s) completed", LogType.message);
            });

            Cursor.Current = Cursors.Default;                               // Restore cursor
        }

        /// <summary>
        /// Select  DP from local storage 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Local_DP_Button_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {


                    string fileName = openFileDialog.FileName;
                    string fileNameInstallationDir = LOCALFOLDER + "\\" + Path.GetFileName(fileName);

                    if (!File.Exists(fileNameInstallationDir)) File.Copy(fileName, fileNameInstallationDir);


                    var source = new Dictionary<string, string>();

                    source.Add(Path.GetFileName(fileName), fileNameInstallationDir);


                    device_Pack_comboBox.DataSource = new BindingSource(source, null);
                    device_Pack_comboBox.DisplayMember = "Key";
                    device_Pack_comboBox.ValueMember = "Value";


                    progressBar1.Value = 100;
                    downloadedDP = Path.GetFileName(fileName);


                }
            }

        }


        /// <summary>
        /// To intall the Device pack the steps are
        /// Identify if the installation will be performed locally or remote
        /// If is local just execute the file
        /// If remotely:
        /// 1) establish a connection with the WMI scope.
        /// 2) Create a folder in the remote server
        /// 3) Share the remote folder (public) 
        /// 4) Copy the file (public) -> todo: use impersonating, then the folder doesn't need to be public
        /// 5) Executhe the installation file 
        /// 6) Unshare the folder
        /// 7) Delete the folder
        /// </summary>
        /// <param name="remoteInfo"></param>
        /// <param name="file"></param>
        private void CallInstallDevicePackProcess(ServerInfo remoteInfo, string file)
        {

            if (NetworkTools.IsLocalServer(remoteInfo.Address))
            {
                ManagementScope scope = new ManagementScope(@"\\LOCALHOST\root\cimv2");
                ExecuteFile(remoteInfo, scope, LOCALFOLDER + "\\" + file, " --quiet");
            }
            else
            {
                ManagementScope scope = NetworkTools.EstablishConnection(remoteInfo);
          //      scope.Connect();

                CreateRemoteFolder(remoteInfo, scope);
                ShareRemoteFolder(remoteInfo, scope);
                CopyFile(remoteInfo, file);
                ExecuteFile(remoteInfo, scope, REMOTEFOLDER + "\\" + file, " --quiet");
                UnshareRemoteFolder(remoteInfo, scope);
                DeleteRemoteFolder(remoteInfo, scope);

         
            }
        }



        /////////////////////////////////
        // REMOTE LOGIC IMPLEMENTATION //
        /////////////////////////////////


        /// <summary>
        /// Create the folder 
        /// </summary>
        /// <param name="remoteInfo"></param>
        /// <param name="theScope"></param>

        private void CreateRemoteFolder(ServerInfo remoteInfo, ManagementScope theScope)
        {
            try
            {


                WriteInConsole("Creating Share Folder " + remoteInfo.Address, LogType.info);
                var Win32_Process_Class = new ManagementClass(theScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
                object[] cmdMdTemp = { "cmd.exe /c md " + REMOTEFOLDER };  
                var mdResult = Win32_Process_Class.InvokeMethod("Create", cmdMdTemp);
                WriteInConsole("Create Share Folder " + CodeToStringConverters.ErrorCodeToString(Convert.ToInt32(mdResult)), LogType.info);
                Thread.Sleep(2000);  /// I hate this method, using cmd to create the folder is not nice, 


            }
            catch (Exception e)
            {
                WriteInConsole(e.Message, LogType.error);


            }
        }

        /// <summary>
        /// Share the folder 
        /// </summary>
        /// <param name="remoteInfo"></param>
        /// <param name="theScope"></param>
        private void ShareRemoteFolder(ServerInfo remoteInfo, ManagementScope theScope)
        {
            try
            {
                WriteInConsole("Sharing Folder " + remoteInfo.Address, LogType.info);
                var winShareClass = new ManagementClass(theScope, new ManagementPath("Win32_Share"), new ObjectGetOptions());
                ManagementBaseObject shareParams = NetworkTools.SetShareParams(winShareClass, REMOTEFOLDER, REMOTESHARENAME);
                var outParams = winShareClass.InvokeMethod("Create", shareParams, null);
                WriteInConsole("Share Folder " + CodeToStringConverters.ShareFolderErrorCodeToString(Convert.ToInt32(outParams.Properties["ReturnValue"].Value)), LogType.info);
            }
            catch (Exception e)
            {
                WriteInConsole(e.Message, LogType.error);
            }
        }

        /// <summary>
        /// Copy the hotfix
        /// </summary>
        /// <param name="remoteInfo"></param>
        /// <param name="file"></param>
        private void CopyFile(ServerInfo remoteInfo, string file)
        {
            try
            {
                string shareFolder = @"\\" + remoteInfo.Address + "\\" + REMOTESHARENAME;
                string srcFile = LOCALFOLDER + "\\" + file;

                WriteInConsole("Copying file" + srcFile + @" to " + shareFolder + "\\" + file, LogType.info);

                NetworkShare.DisconnectFromShare(shareFolder, true); //Disconnect in case we are currently connected with our credentials;
                NetworkShare.ConnectToShare(shareFolder, remoteInfo.Domain + "\\" + remoteInfo.UserName, remoteInfo.Password); //Connect with the new credentials

                File.Copy(srcFile, shareFolder + "\\" + file);

                NetworkShare.DisconnectFromShare(shareFolder, false); //Disconnect from the server.
                WriteInConsole("File Copied", LogType.info);
            }
            catch (System.IO.IOException ex)
            {
                WriteInConsole(ex.Message, LogType.error);
            }
        }

        /// <summary>
        /// Execute the file
        /// </summary>
        /// <param name="remoteInfo"></param>
        /// <param name="theScope"></param>
        /// <param name="file"></param>
        /// <param name="args"></param>
        private void ExecuteFile(ServerInfo remoteInfo, ManagementScope theScope, string file, string args)
        {
            WriteInConsole("Executing Hotfix on server " + remoteInfo.Address, LogType.info);

            object[] theProcessToRun = { file + args, null, null, 0 };

            ManagementClass theClass = new ManagementClass(theScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());

            try
            {
                var output = theClass.InvokeMethod("Create", theProcessToRun);
                Thread.Sleep(1000);

                String ProcID = theProcessToRun[3].ToString();

                WriteInConsole("Execute Hotfix on server " + remoteInfo.Address + ": " + CodeToStringConverters.ErrorCodeToString(Convert.ToInt32(output)), LogType.info);

                WriteInConsole("PID on server  " + remoteInfo.Address + ": " + ProcID, LogType.debug);

                WriteInConsole("Installing hotfix on server: " + remoteInfo.Address, LogType.message);

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
                                WriteInConsole("Update Process Finished on server: " + remoteInfo.Address, LogType.message);
                                // Upgrade Finish 
                            }
                        }
                    }
                    wWatcher.Stop();
                }
            }
            catch (Exception ex)
            {
                WriteInConsole(ex.Message, LogType.error);
            }

        }


        /// <summary>
        /// Unshare the folder
        /// </summary>
        /// <param name="remoteInfo"></param>
        /// <param name="theScope"></param>
        private void UnshareRemoteFolder(ServerInfo remoteInfo, ManagementScope theScope)
        {
            try
            {
                WriteInConsole("Unsharing Folder " + remoteInfo.Address, LogType.info);

                var win32_Share_class = new ManagementClass(theScope, new ManagementPath("Win32_Share"), new ObjectGetOptions());
                ManagementObjectCollection collection = win32_Share_class.GetInstances();

                foreach (ManagementObject item in collection)
                {
                    if (Convert.ToString(item["Name"]).Equals(REMOTESHARENAME))
                    {
                        var unshareOutParams = item.InvokeMethod("Delete", new object[] { });
                        WriteInConsole("Unshare Folder " + CodeToStringConverters.ShareFolderErrorCodeToString(Convert.ToInt32(unshareOutParams)), LogType.info);
                    }
                }

            }
            catch (Exception ex)
            {
                WriteInConsole(ex.Message, LogType.error);
            }


        }

        /// <summary>
        /// Delete the folder
        /// </summary>
        /// <param name="remoteInfo"></param>
        /// <param name="theScope"></param>
        private void DeleteRemoteFolder(ServerInfo remoteInfo, ManagementScope theScope)
        {
            try
            {
                WriteInConsole("Removing Share Folder " + remoteInfo.Address, LogType.info);
                var Win32_Process_Class = new ManagementClass(theScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
                object[] cmdRMTemp = { @"cmd.exe /c rmdir /s /q " + REMOTEFOLDER };
                var rmResult = Win32_Process_Class.InvokeMethod("Create", cmdRMTemp);
                WriteInConsole("Remove Share Folder " + CodeToStringConverters.ErrorCodeToString(Convert.ToInt32(rmResult)), LogType.info);

            }
            catch (Exception ex)
            {
                WriteInConsole(ex.Message, LogType.error);
            }

        }




        bool debug = true;
        private void WriteInConsole(string text, LogType type)
        {
            if (!debug || type != LogType.debug)
            {
                textBox_Console.Invoke((MethodInvoker)delegate
                {
                    Color _color;
                    switch (type)
                    {
                        case LogType.debug:
                            _color = DEBUGCOLOR;
                            break;
                        case LogType.message:
                            _color = MESSAGECOLOR;
                            break;
                        case LogType.info:
                            _color = INFOCOLOR;
                            break;
                        case LogType.error:
                            _color = ERRORCOLOR;
                            break;
                        default:
                            _color = Color.White;
                            break;
                    }
                    textBox_Console.SelectionStart = textBox_Console.TextLength;
                    textBox_Console.SelectionLength = 0;

                    textBox_Console.SelectionColor = _color;
                    textBox_Console.AppendText(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": " + text + Environment.NewLine);
                    textBox_Console.SelectionColor = textBox_Console.ForeColor;

                    textBox_Console.SelectionStart = textBox_Console.TextLength;
                    textBox_Console.ScrollToCaret();
                });
            }
        }


        enum LogType
        {
            debug,
            message,
            info,
            error,
        }

        /// <summary>
        /// Thread execution control, limit the parallelism 
        /// </summary>
        /// <param name="tasksToRun"></param>
        /// <param name="maxTasksToRunInParallel"></param>
        /// <param name="timeoutInMilliseconds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAndWaitAllThrottledAsync(IEnumerable<Task> tasksToRun, int maxTasksToRunInParallel, int timeoutInMilliseconds, CancellationToken cancellationToken = new CancellationToken())
        {
            List<Task> tasks = tasksToRun.ToList(); // Convert to a list of tasks so that we don't enumerate over it multiple times needlessly.
            using (var throttler = new SemaphoreSlim(maxTasksToRunInParallel))
            {
                var postTaskTasks = new List<Task>();

                // Have each task notify the throttler when it completes so that it decrements the number of tasks currently running.
                tasks.ForEach(t => postTaskTasks.Add(t.ContinueWith(tsk => throttler.Release())));

                // Start running each task.
                foreach (var task in tasks)
                {
                    // Increment the number of tasks currently running and wait if too many are running.
                    await throttler.WaitAsync(timeoutInMilliseconds, cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();
                    task.Start();
                }

                // Wait for all of the provided tasks to complete.
                // We wait on the list of "post" tasks instead of the original tasks, otherwise there is a potential race condition where the throttlers using block is exited before some Tasks have had their "post" action completed, which references the throttler, resulting in an exception due to accessing a disposed object.
                await Task.WhenAll(postTaskTasks.ToArray());
            }
        }


    }
}