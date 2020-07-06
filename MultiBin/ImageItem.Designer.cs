namespace MultiBin
{
    partial class ImageItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.item = new System.Windows.Forms.Panel();
            this.fileName = new System.Windows.Forms.TextBox();
            this.image = new System.Windows.Forms.PictureBox();
            this.item.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // item
            // 
            this.item.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.item.Controls.Add(this.image);
            this.item.Controls.Add(this.fileName);
            this.item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.item.Location = new System.Drawing.Point(0, 0);
            this.item.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.item.Name = "item";
            this.item.Size = new System.Drawing.Size(364, 200);
            this.item.TabIndex = 3;
            // 
            // fileName
            // 
            this.fileName.BackColor = System.Drawing.Color.Black;
            this.fileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fileName.ForeColor = System.Drawing.Color.White;
            this.fileName.Location = new System.Drawing.Point(0, 174);
            this.fileName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(364, 26);
            this.fileName.TabIndex = 2;
            this.fileName.Text = "icon.png";
            this.fileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fileName.CursorChanged += new System.EventHandler(this.fileName_CursorChanged);
            // 
            // image
            // 
            this.image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image.Image = global::MultiBin.Properties.Resources.file;
            this.image.Location = new System.Drawing.Point(0, 0);
            this.image.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(364, 174);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 0;
            this.image.TabStop = false;
            // 
            // ImageItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.item);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ImageItem";
            this.Size = new System.Drawing.Size(364, 200);
            this.item.ResumeLayout(false);
            this.item.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel item;
        public System.Windows.Forms.PictureBox image;
        public System.Windows.Forms.TextBox fileName;
    }
}
