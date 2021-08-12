using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dummyUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread.Sleep(1000);
            System.Environment.Exit(0);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
