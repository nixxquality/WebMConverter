using System.Collections.Generic;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class TrackSelectDialog : Form
    {
        public int SelectedTrack
        { get { return (int)comboBoxTracks.SelectedValue; } }

        public TrackSelectDialog(string tracktype, IEnumerable<int> tracks)
        {
            InitializeComponent();

            labelSelect.Text = string.Format("{0} track:", tracktype);

            Dictionary<int, string> dropdownTracks = new Dictionary<int,string>();
            foreach (int Track in tracks)
            {
                dropdownTracks.Add(Track, string.Format("Track #{0}", Track));
            }
            comboBoxTracks.DataSource = new BindingSource(dropdownTracks, null);
            comboBoxTracks.ValueMember = "Key";
            comboBoxTracks.DisplayMember = "Value";
        }
    }
}
