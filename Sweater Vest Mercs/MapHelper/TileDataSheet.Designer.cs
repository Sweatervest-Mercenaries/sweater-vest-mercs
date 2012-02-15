namespace MapHelper
{
    partial class TileDataSheet
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lockedCheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.dataBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hexValueBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sheetBox = new System.Windows.Forms.TextBox();
            this.dataBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lockedCheckBox
            // 
            this.lockedCheckBox.AutoSize = true;
            this.lockedCheckBox.Checked = true;
            this.lockedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lockedCheckBox.Location = new System.Drawing.Point( 3, 7 );
            this.lockedCheckBox.Name = "lockedCheckBox";
            this.lockedCheckBox.Size = new System.Drawing.Size( 62, 17 );
            this.lockedCheckBox.TabIndex = 0;
            this.lockedCheckBox.Text = "Locked";
            this.lockedCheckBox.UseVisualStyleBackColor = true;
            this.lockedCheckBox.CheckedChanged += new System.EventHandler( this.lockedCheckBox_CheckedChanged );
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point( 71, 3 );
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size( 75, 23 );
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // dataBox
            // 
            this.dataBox.Controls.Add( this.sheetBox );
            this.dataBox.Controls.Add( this.label3 );
            this.dataBox.Controls.Add( this.hexValueBox );
            this.dataBox.Controls.Add( this.label2 );
            this.dataBox.Controls.Add( this.nameBox );
            this.dataBox.Controls.Add( this.label1 );
            this.dataBox.Location = new System.Drawing.Point( 3, 32 );
            this.dataBox.Name = "dataBox";
            this.dataBox.Size = new System.Drawing.Size( 331, 189 );
            this.dataBox.TabIndex = 2;
            this.dataBox.TabStop = false;
            this.dataBox.Text = "Tile Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 136, 65 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 35, 13 );
            this.label3.TabIndex = 4;
            this.label3.Text = "Sheet";
            // 
            // hexValueBox
            // 
            this.hexValueBox.Location = new System.Drawing.Point( 10, 81 );
            this.hexValueBox.Name = "hexValueBox";
            this.hexValueBox.ReadOnly = true;
            this.hexValueBox.Size = new System.Drawing.Size( 100, 20 );
            this.hexValueBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 7, 65 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 56, 13 );
            this.label2.TabIndex = 2;
            this.label2.Text = "Hex Value";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point( 10, 37 );
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size( 315, 20 );
            this.nameBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 7, 21 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 35, 13 );
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // sheetBox
            // 
            this.sheetBox.Location = new System.Drawing.Point( 139, 81 );
            this.sheetBox.Name = "sheetBox";
            this.sheetBox.ReadOnly = true;
            this.sheetBox.Size = new System.Drawing.Size( 100, 20 );
            this.sheetBox.TabIndex = 5;
            // 
            // TileDataSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add( this.dataBox );
            this.Controls.Add( this.saveButton );
            this.Controls.Add( this.lockedCheckBox );
            this.Name = "TileDataSheet";
            this.Size = new System.Drawing.Size( 355, 257 );
            this.dataBox.ResumeLayout( false );
            this.dataBox.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox lockedCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox dataBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hexValueBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox sheetBox;
    }
}
