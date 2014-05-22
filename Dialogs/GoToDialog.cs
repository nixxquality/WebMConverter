using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class GoToDialog : Form
    {
        public string Result;

        public GoToDialog()
        {
            InitializeComponent();
        }
        public GoToDialog(string label, string value) : this()
        {
            label1.Text = label + ":";
            textBox1.Text = value;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            Result = textBox1.Text;
            Close();
        }
    }
}
