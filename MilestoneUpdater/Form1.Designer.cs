
namespace MilestoneUpdater
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelMSVer = new System.Windows.Forms.Label();
            this.labelMSName = new System.Windows.Forms.Label();
            this.label_ms_status = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMSAddress = new System.Windows.Forms.TextBox();
            this.textBoxMSPass = new System.Windows.Forms.TextBox();
            this.textBoxMSDomain = new System.Windows.Forms.TextBox();
            this.textBoxMSUser = new System.Windows.Forms.TextBox();
            this.buttonMSConnect = new System.Windows.Forms.Button();
            this.textBox_Console = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Domain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAllCredentials = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxAllPass = new System.Windows.Forms.TextBox();
            this.textBoxAllDomain = new System.Windows.Forms.TextBox();
            this.textBoxAllUser = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridViewHotFixList = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.HotfixType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HotfixUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HotfixFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHotFixList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelMSVer);
            this.groupBox1.Controls.Add(this.labelMSName);
            this.groupBox1.Controls.Add(this.label_ms_status);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxMSAddress);
            this.groupBox1.Controls.Add(this.textBoxMSPass);
            this.groupBox1.Controls.Add(this.textBoxMSDomain);
            this.groupBox1.Controls.Add(this.textBoxMSUser);
            this.groupBox1.Controls.Add(this.buttonMSConnect);
            this.groupBox1.ForeColor = System.Drawing.Color.Snow;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(854, 162);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Management Server";
            // 
            // labelMSVer
            // 
            this.labelMSVer.AutoSize = true;
            this.labelMSVer.Location = new System.Drawing.Point(598, 22);
            this.labelMSVer.Name = "labelMSVer";
            this.labelMSVer.Size = new System.Drawing.Size(35, 13);
            this.labelMSVer.TabIndex = 13;
            this.labelMSVer.Text = "label9";
            // 
            // labelMSName
            // 
            this.labelMSName.AutoSize = true;
            this.labelMSName.Location = new System.Drawing.Point(355, 22);
            this.labelMSName.Name = "labelMSName";
            this.labelMSName.Size = new System.Drawing.Size(35, 13);
            this.labelMSName.TabIndex = 13;
            this.labelMSName.Text = "label9";
            // 
            // label_ms_status
            // 
            this.label_ms_status.AutoSize = true;
            this.label_ms_status.Location = new System.Drawing.Point(235, 115);
            this.label_ms_status.Name = "label_ms_status";
            this.label_ms_status.Size = new System.Drawing.Size(50, 13);
            this.label_ms_status.TabIndex = 12;
            this.label_ms_status.Text = "STATUS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Pass";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Domain";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "User";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "IP";
            // 
            // textBoxMSAddress
            // 
            this.textBoxMSAddress.Location = new System.Drawing.Point(72, 22);
            this.textBoxMSAddress.Name = "textBoxMSAddress";
            this.textBoxMSAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxMSAddress.TabIndex = 4;
            // 
            // textBoxMSPass
            // 
            this.textBoxMSPass.Location = new System.Drawing.Point(72, 127);
            this.textBoxMSPass.Name = "textBoxMSPass";
            this.textBoxMSPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxMSPass.TabIndex = 5;
            // 
            // textBoxMSDomain
            // 
            this.textBoxMSDomain.Location = new System.Drawing.Point(72, 57);
            this.textBoxMSDomain.Name = "textBoxMSDomain";
            this.textBoxMSDomain.Size = new System.Drawing.Size(100, 20);
            this.textBoxMSDomain.TabIndex = 6;
            // 
            // textBoxMSUser
            // 
            this.textBoxMSUser.Location = new System.Drawing.Point(72, 92);
            this.textBoxMSUser.Name = "textBoxMSUser";
            this.textBoxMSUser.Size = new System.Drawing.Size(100, 20);
            this.textBoxMSUser.TabIndex = 7;
            // 
            // buttonMSConnect
            // 
            this.buttonMSConnect.ForeColor = System.Drawing.Color.Black;
            this.buttonMSConnect.Location = new System.Drawing.Point(204, 19);
            this.buttonMSConnect.Name = "buttonMSConnect";
            this.buttonMSConnect.Size = new System.Drawing.Size(119, 68);
            this.buttonMSConnect.TabIndex = 3;
            this.buttonMSConnect.Text = "Get Servers";
            this.buttonMSConnect.UseVisualStyleBackColor = true;
            this.buttonMSConnect.Click += new System.EventHandler(this.ButtonMSConnect_Click);
            // 
            // textBox_Console
            // 
            this.textBox_Console.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox_Console.Location = new System.Drawing.Point(9, 779);
            this.textBox_Console.Name = "textBox_Console";
            this.textBox_Console.Size = new System.Drawing.Size(1247, 188);
            this.textBox_Console.TabIndex = 11;
            this.textBox_Console.TabStop = false;
            this.textBox_Console.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.ForeColor = System.Drawing.Color.Snow;
            this.groupBox2.Location = new System.Drawing.Point(12, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1244, 354);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Servers List";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DisplayName,
            this.ServerType,
            this.Address,
            this.Domain,
            this.User,
            this.Password,
            this.Selected});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Size = new System.Drawing.Size(1238, 335);
            this.dataGridView1.TabIndex = 0;
            // 
            // DisplayName
            // 
            this.DisplayName.HeaderText = "DisplayName";
            this.DisplayName.Name = "DisplayName";
            // 
            // ServerType
            // 
            this.ServerType.HeaderText = "ServerType";
            this.ServerType.Name = "ServerType";
            this.ServerType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // Domain
            // 
            this.Domain.HeaderText = "Domain";
            this.Domain.Name = "Domain";
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            // 
            // Password
            // 
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            // 
            // Selected
            // 
            this.Selected.HeaderText = "Selected";
            this.Selected.Name = "Selected";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonAllCredentials);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textBoxAllPass);
            this.groupBox3.Controls.Add(this.textBoxAllDomain);
            this.groupBox3.Controls.Add(this.textBoxAllUser);
            this.groupBox3.ForeColor = System.Drawing.Color.Snow;
            this.groupBox3.Location = new System.Drawing.Point(889, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 163);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Change All Credentials";
            // 
            // buttonAllCredentials
            // 
            this.buttonAllCredentials.ForeColor = System.Drawing.Color.Black;
            this.buttonAllCredentials.Location = new System.Drawing.Point(201, 44);
            this.buttonAllCredentials.Name = "buttonAllCredentials";
            this.buttonAllCredentials.Size = new System.Drawing.Size(119, 51);
            this.buttonAllCredentials.TabIndex = 11;
            this.buttonAllCredentials.Text = "Change Credentials";
            this.buttonAllCredentials.UseVisualStyleBackColor = true;
            this.buttonAllCredentials.Click += new System.EventHandler(this.buttonAllCredentials_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Pass";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Domain";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(37, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "User";
            // 
            // textBoxAllPass
            // 
            this.textBoxAllPass.Location = new System.Drawing.Point(74, 86);
            this.textBoxAllPass.Name = "textBoxAllPass";
            this.textBoxAllPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxAllPass.TabIndex = 4;
            // 
            // textBoxAllDomain
            // 
            this.textBoxAllDomain.Location = new System.Drawing.Point(74, 34);
            this.textBoxAllDomain.Name = "textBoxAllDomain";
            this.textBoxAllDomain.Size = new System.Drawing.Size(100, 20);
            this.textBoxAllDomain.TabIndex = 5;
            // 
            // textBoxAllUser
            // 
            this.textBoxAllUser.Location = new System.Drawing.Point(74, 60);
            this.textBoxAllUser.Name = "textBoxAllUser";
            this.textBoxAllUser.Size = new System.Drawing.Size(100, 20);
            this.textBoxAllUser.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1134, 704);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 60);
            this.button2.TabIndex = 0;
            this.button2.Text = "Install Hotfixes";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.RUN_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridViewHotFixList);
            this.groupBox4.ForeColor = System.Drawing.Color.Snow;
            this.groupBox4.Location = new System.Drawing.Point(12, 565);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1103, 199);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Hotfix List";
            // 
            // dataGridViewHotFixList
            // 
            this.dataGridViewHotFixList.AllowUserToAddRows = false;
            this.dataGridViewHotFixList.AllowUserToDeleteRows = false;
            this.dataGridViewHotFixList.AllowUserToOrderColumns = true;
            this.dataGridViewHotFixList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHotFixList.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridViewHotFixList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHotFixList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HotfixType,
            this.HotfixUrl,
            this.HotfixFile,
            this.LocalLocation});
            this.dataGridViewHotFixList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewHotFixList.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewHotFixList.Name = "dataGridViewHotFixList";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.dataGridViewHotFixList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewHotFixList.Size = new System.Drawing.Size(1097, 180);
            this.dataGridViewHotFixList.TabIndex = 0;
             // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1134, 565);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 60);
            this.button1.TabIndex = 15;
            this.button1.Text = "Find Hotfixes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1134, 635);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 60);
            this.button3.TabIndex = 15;
            this.button3.Text = "Get Hotfixes";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // HotfixType
            // 
            this.HotfixType.HeaderText = "HotfixType";
            this.HotfixType.Name = "HotfixType";
            this.HotfixType.ReadOnly = true;
            // 
            // HotfixUrl
            // 
            this.HotfixUrl.HeaderText = "HotfixUrl";
            this.HotfixUrl.Name = "HotfixUrl";
            this.HotfixUrl.ReadOnly = true;
            // 
            // HotfixFile
            // 
            this.HotfixFile.HeaderText = "HotfixFile";
            this.HotfixFile.Name = "HotfixFile";
            this.HotfixFile.ReadOnly = true;
            // 
            // LocalLocation
            // 
            this.LocalLocation.HeaderText = "LocalLocation";
            this.LocalLocation.Name = "LocalLocation";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1277, 979);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox_Console);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHotFixList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMSAddress;
        private System.Windows.Forms.TextBox textBoxMSPass;
        private System.Windows.Forms.TextBox textBoxMSDomain;
        private System.Windows.Forms.TextBox textBoxMSUser;
        private System.Windows.Forms.Button buttonMSConnect;
        private System.Windows.Forms.RichTextBox textBox_Console;
        private System.Windows.Forms.Label label_ms_status;
        private System.Windows.Forms.Label labelMSName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonAllCredentials;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxAllPass;
        private System.Windows.Forms.TextBox textBoxAllDomain;
        private System.Windows.Forms.TextBox textBoxAllUser;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelMSVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Domain;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridViewHotFixList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotfixType;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotfixUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotfixFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalLocation;
    }
}

