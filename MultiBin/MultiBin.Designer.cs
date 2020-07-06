namespace MultiBin
{
    partial class MultiBin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.main_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.hide = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // main_panel
            // 
            this.main_panel.AutoScroll = true;
            this.main_panel.BackgroundImage = global::MultiBin.Properties.Resources.img1;
            this.main_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel.Location = new System.Drawing.Point(0, 0);
            this.main_panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(408, 277);
            this.main_panel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(362, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "::..";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // hide
            // 
            this.hide.Enabled = true;
            this.hide.Tick += new System.EventHandler(this.hide_Tick);
            // 
            // MultiBin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::MultiBin.Properties.Resources.img1;
            this.ClientSize = new System.Drawing.Size(408, 277);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.main_panel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MultiBin";
            this.Opacity = 0.9D;
            this.Text = "MultiBin";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer hide;
    }
}

