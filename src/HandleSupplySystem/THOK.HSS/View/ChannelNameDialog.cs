using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.HSS.View
{
    public partial class ChannelNameDialog : Form
    {
        public ChannelNameDialog()
        {
            InitializeComponent();
        }

        public string ChannelName
        { 
           get{
              return txtChannelName.Text;
           }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtChannelName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == '\b'))
                e.Handled = true;
        }
    }
}