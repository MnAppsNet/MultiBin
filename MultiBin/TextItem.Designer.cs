namespace MultiBin
{
    partial class TextItem
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
            this.textArea = new System.Windows.Forms.RichTextBox();
            this.item.SuspendLayout();
            this.SuspendLayout();
            // 
            // item
            // 
            this.item.AutoSize = true;
            this.item.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.item.Controls.Add(this.textArea);
            this.item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.item.Location = new System.Drawing.Point(0, 0);
            this.item.Name = "item";
            this.item.Size = new System.Drawing.Size(369, 203);
            this.item.TabIndex = 0;
            // 
            // textArea
            // 
            this.textArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textArea.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textArea.Location = new System.Drawing.Point(0, 0);
            this.textArea.MaximumSize = new System.Drawing.Size(400, 200);
            this.textArea.Name = "textArea";
            this.textArea.ReadOnly = true;
            this.textArea.Size = new System.Drawing.Size(366, 200);
            this.textArea.TabIndex = 0;
            this.textArea.Text = "";
            this.textArea.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.textArea_ContentsResized);
            this.textArea.CursorChanged += new System.EventHandler(this.textArea_CursorChanged);
            // 
            // TextItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.item);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TextItem";
            this.Size = new System.Drawing.Size(369, 203);
            this.item.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel item;
        public System.Windows.Forms.RichTextBox textArea;
    }
}
