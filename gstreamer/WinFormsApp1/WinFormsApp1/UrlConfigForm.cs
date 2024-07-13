using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class UrlConfigForm : Form
    {
        public string UrlStreaming { get; private set; }

        public UrlConfigForm()
        {
            InitializeComponent();
        }

        private void btnSetSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrlStreaming.Text))
            {
                MessageBox.Show("Please enter a valid port number.");
                return; 
            }
            UrlStreaming = txtUrlStreaming.Text;
            DialogResult = DialogResult.OK;
            Close();
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUrlStreaming.Text = string.Empty;    
        }
    }
}
