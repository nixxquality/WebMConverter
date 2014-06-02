using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class TextInputDialog : Form
    {
        public TextInputDialog(string text)
        {
            InitializeComponent();

            boxText.Text = text;
        }
    }
}
