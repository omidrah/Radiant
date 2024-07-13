using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class frmNetwork : Form
    {
        public string IpAddress { get; private set; }
        public int Port { get; private set; }
        public frmNetwork()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPort.Text=txtIp.Text= string.Empty;
        }

        private void btnSetSave_Click(object sender, EventArgs e)
        {
            IpAddress = txtIp.Text;
            if (int.TryParse(txtPort.Text, out int port))
            {
                Port = port;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid port number.");
            }
        }
    }
}
