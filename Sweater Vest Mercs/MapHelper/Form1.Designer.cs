namespace MapHelper
{
    partial class MapHelperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.includedSectionsCheckBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tilesListBox = new System.Windows.Forms.ListBox();
            this.tileDataSheet1 = new MapHelper.TileDataSheet();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 9, 15 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 102, 13 );
            this.label1.TabIndex = 1;
            this.label1.Text = "Textures To Include";
            // 
            // includedSectionsCheckBox
            // 
            this.includedSectionsCheckBox.CheckOnClick = true;
            this.includedSectionsCheckBox.FormattingEnabled = true;
            this.includedSectionsCheckBox.Location = new System.Drawing.Point( 12, 31 );
            this.includedSectionsCheckBox.Name = "includedSectionsCheckBox";
            this.includedSectionsCheckBox.Size = new System.Drawing.Size( 120, 94 );
            this.includedSectionsCheckBox.Sorted = true;
            this.includedSectionsCheckBox.TabIndex = 2;
            this.includedSectionsCheckBox.SelectedIndexChanged += new System.EventHandler( this.includedSectionsCheckBox_SelectedIndexChanged );
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 9, 132 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 41, 13 );
            this.label2.TabIndex = 3;
            this.label2.Text = "Search";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point( 12, 148 );
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size( 120, 20 );
            this.searchBox.TabIndex = 4;
            this.searchBox.TextChanged += new System.EventHandler( this.textBox1_TextChanged );
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 12, 174 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 75, 23 );
            this.button1.TabIndex = 5;
            this.button1.Text = "Search!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler( this.button1_Click );
            // 
            // tilesListBox
            // 
            this.tilesListBox.FormattingEnabled = true;
            this.tilesListBox.Location = new System.Drawing.Point( 148, 31 );
            this.tilesListBox.Name = "tilesListBox";
            this.tilesListBox.Size = new System.Drawing.Size( 337, 95 );
            this.tilesListBox.Sorted = true;
            this.tilesListBox.TabIndex = 7;
            this.tilesListBox.SelectedIndexChanged += new System.EventHandler( this.tilesListBox_SelectedIndexChanged );
            // 
            // tileDataSheet1
            // 
            this.tileDataSheet1.AutoSize = true;
            this.tileDataSheet1.Location = new System.Drawing.Point( 148, 132 );
            this.tileDataSheet1.Name = "tileDataSheet1";
            this.tileDataSheet1.Size = new System.Drawing.Size( 337, 224 );
            this.tileDataSheet1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 148, 15 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 76, 13 );
            this.label3.TabIndex = 8;
            this.label3.Text = "Matching Tiles";
            // 
            // MapHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size( 635, 388 );
            this.Controls.Add( this.label3 );
            this.Controls.Add( this.tilesListBox );
            this.Controls.Add( this.tileDataSheet1 );
            this.Controls.Add( this.button1 );
            this.Controls.Add( this.searchBox );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.includedSectionsCheckBox );
            this.Controls.Add( this.label1 );
            this.Name = "MapHelperForm";
            this.Padding = new System.Windows.Forms.Padding( 2 );
            this.ShowIcon = false;
            this.Text = "SVM Map Helper";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox includedSectionsCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button button1;
        private TileDataSheet tileDataSheet1;
        private System.Windows.Forms.ListBox tilesListBox;
        private System.Windows.Forms.Label label3;
    }
}

