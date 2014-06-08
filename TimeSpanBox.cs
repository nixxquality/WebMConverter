using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class TimeSpanBox : TextBox
    {
        public TimeSpan Value
        {
            get
            {
                return TimeSpan.ParseExact(Text, @"hh\:mm\:ss\.fff", CultureInfo.CurrentCulture);
            }
            set
            {
                Text = value.ToString(@"hh\:mm\:ss\.fff");
            }
        }
    }
}
