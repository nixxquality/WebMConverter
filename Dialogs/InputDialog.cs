using System;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class InputDialog<T> : Form
    {
        public T Value;

        dynamic inputField;

        public InputDialog(string label, T defaultValue)
        {
            InitializeComponent();
            addInputField(defaultValue);

            label1.Text = label + ":";
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            switch (typeof(T).ToString())
            {
                case "System.String":
                    Value = inputField.Text;
                    break;
                case "System.Int32":
                    Value = int.Parse(inputField.Text);
                    break;
                case "System.TimeSpan":
                    Value = inputField.Value;
                    break;
            }
            Close();
        }

        void addInputField(T value)
        {
            switch (typeof(T).ToString())
            {
                case "System.String":
                    inputField = new TextBox();
                    inputField.Text = value;
                    break;
                case "System.Int32":
                    inputField = new TextBox();
                    inputField.Text = value.ToString();
                    (inputField as TextBox).KeyPress += delegate(object sender, KeyPressEventArgs e)
                    {
                        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                        {
                            e.Handled = true;
                        }
                    };
                    break;
                case "System.TimeSpan":
                    inputField = new TimeSpanBox();
                    inputField.Value = value;
                    break;
                default:
                    throw new ArgumentException("GoToDialog only works with <string>, <int> or <TimeSpan>");
            }

            inputField.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            inputField.Location = new System.Drawing.Point(71, 3);
            inputField.Name = "inputField";
            inputField.Size = new System.Drawing.Size(123, 20);
            inputField.TabIndex = 3;

            tableLayoutPanel.Controls.Add(inputField, 1, 0);
            tableLayoutPanel.SetColumnSpan(inputField, 2);
        }
    }
}
