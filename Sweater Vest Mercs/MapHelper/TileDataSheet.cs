using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SVMLib.Tiles;

namespace MapHelper
{
    public partial class TileDataSheet : UserControl
    {
        private List<TextBox> Boxes;
        public TileDataSheet()
        {
            InitializeComponent();

            Boxes = new List<TextBox>();

            Boxes.Add( nameBox );
            Boxes.Add( hexValueBox );

            lockedCheckBox_CheckedChanged( null, null );
        }

        public void loadTileData(TileData data)
        {
            nameBox.Text = data.Name;
            hexValueBox.Text = data.Hex;
        }

        private void lockedCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            foreach ( TextBox box in Boxes )
            {
                box.ReadOnly = lockedCheckBox.Checked;
            }
        }
    }
}
