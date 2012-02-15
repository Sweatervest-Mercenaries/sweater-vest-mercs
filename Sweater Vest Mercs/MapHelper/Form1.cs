using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SVMLib.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Sweater_Vest_Mercs;

namespace MapHelper
{
    public partial class MapHelperForm : Form
    {
        private List<Section> Sections { get; set; }

        public MapHelperForm()
        {
            InitializeComponent();

            Sections = new List<Section>();

            LoadTiles();
        }

        private void textBox1_TextChanged( object sender, EventArgs e )
        {

        }

        private void LoadTiles()
        {
            SVMGame game = new SVMGame();

            TileHelper.Load( game.GetContent().Load<TileDataList>( "Tiles" ) );

            List<TileData> tiles = TileHelper.DataList.tiles;

            bool foundSection = false;

            foreach ( TileData tile in tiles )
            {
                foundSection = false;

                // Find the tiles section
                foreach ( Section sec in Sections )
                {
                    if ( sec.Name.Equals( tile.Sheet.ToString() ) )
                    {
                        // Section found, add it and move on
                        sec.Elements.Add( tile );
                        foundSection = true;
                        break;
                    }
                }

                if ( !foundSection )
                {
                    // Section not found, add a new one
                    Section tmp = new Section( tile.Sheet.ToString() );
                    tmp.Elements.Add( tile );
                    Sections.Add( tmp );
                }
            }

            // All tiles loaded, go ahead and populate drop down
            foreach ( Section sec in Sections )
            {
                includedSectionsCheckBox.Items.Add( sec.Name );
            }

            // Update
            includedSectionsCheckBox.Update();
        }

        private void includedSectionsCheckBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            // Clear the tile list
            tilesListBox.Items.Clear();

            foreach (String iter in includedSectionsCheckBox.CheckedItems)
            {
                // Look for the matching section
                foreach ( Section sec in Sections )
                {
                    if ( sec.Name.Equals( iter ) )
                    {
                        foreach (TileData tile in sec.Elements)
                        {
                            tilesListBox.Items.Add( tile );
                        }
                        break;
                    }
                }
                Console.WriteLine( iter.ToString() ); 
            }

            tilesListBox.Update();
        }

        private void button1_Click( object sender, EventArgs e )
        {
            String searchStr = searchBox.Text.ToLower();

            if ( searchStr.Length > 0 )
            {
                tilesListBox.Items.Clear();

                // Non empty string, actually apply search
                foreach ( Section sec in Sections )
                {
                    foreach ( TileData tile in sec.Elements )
                    {
                        if ( tile.Name.ToLower().Contains( searchStr ) )
                        {
                            // Found!
                            tilesListBox.Items.Add( tile );
                        }
                    }
                }
            }
        }

        private void tilesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            tileDataSheet1.loadTileData( (TileData)tilesListBox.SelectedItem );
        }
    }
}
