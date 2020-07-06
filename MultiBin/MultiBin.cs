using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiBin
{
    public partial class MultiBin : Form
    {
        public MultiBin()
        {
            InitializeComponent();
        }
        private static KeyboardEvents keyboardEvents;
        private Bin bin;
        private bool resize = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Refresh();
            keyboardEvents = new KeyboardEvents();
            keyboardEvents.AddEvent("MultiBin", new Keys[] { Keys.LControlKey, Keys.LShiftKey, Keys.Space },this , "openBin", new object[0]);
            keyboardEvents.CaptureKeyboard();
            bin = new Bin(this, new Bin.BinUpdated(binUpdated));
            this.Hide();
        }

        private void binUpdated(Item item, bool remove)
        {
            if (InvokeRequired)
            {
                Invoke(new Bin.BinUpdated(binUpdated), new object[] { item, remove });
            }
            else
            {
                if (remove)
                {
                    if (item.Control == null)
                        main_panel.Controls.Remove(main_panel.Controls.Find("bin_" + item.ID, false)[0]);
                    else
                        main_panel.Controls.Remove(item.Control);
                    return;
                }

                if (item.Type == DataFormats.FileDrop)
                {
                    string[] files = (string[])item.Data;
                    if (files.Length > 0)
                    {
                        ImageItem fileItem = new ImageItem();
                        string file_names = "";
                        foreach (string s in files)
                        {
                            file_names = file_names + Path.GetFileName(s) + " | ";
                        }
                        file_names = file_names.Substring(0, file_names.Length - 1);
                        fileItem.fileName.Text = file_names;
                        fileItem.Name = "bin_" + item.ID;
                        fileItem.MouseDown += binItem_MouseClick;
                        fileItem.Tag = item.ID;
                        foreach (Control c in fileItem.Controls)
                        {
                            foreach (Control c2 in c.Controls)
                            {
                                c2.Tag = item.ID;
                                c2.MouseDown += binItem_MouseClick;
                            }
                            c.Tag = item.ID;
                            c.MouseDown += binItem_MouseClick;
                        }
                        main_panel.Controls.Add(fileItem);
                        item.Control = fileItem;
                    }
                }
                else if (item.Type == DataFormats.Bitmap)
                {
                    Bitmap image = (Bitmap)item.Data;
                    ImageItem imageItem = new ImageItem();
                    imageItem.fileName.Visible = false;
                    imageItem.Name = "bin_" + item.ID;
                    imageItem.image.Image = image;
                    imageItem.MouseDown += binItem_MouseClick;
                    imageItem.Tag = item.ID;
                    foreach (Control c in imageItem.Controls)
                    {
                        foreach (Control c2 in c.Controls)
                        {
                            c2.Tag = item.ID;
                            c2.MouseDown += binItem_MouseClick;
                        }
                        c.Tag = item.ID;
                        c.MouseDown += binItem_MouseClick;
                    }
                    main_panel.Controls.Add(imageItem);
                    item.Control = imageItem;
                }
                else if (item.Type == DataFormats.Rtf)
                {
                    string rtf = (string)item.Data;
                    TextItem rtfItem = new TextItem();
                    rtfItem.Name = "bin_" + item.ID;
                    rtfItem.textArea.Rtf = rtf;
                    rtfItem.MouseDown += binItem_MouseClick;
                    rtfItem.Tag = item.ID;
                    foreach (Control c in rtfItem.Controls)
                    {
                        foreach (Control c2 in c.Controls)
                        {
                            c2.Tag = item.ID;
                            c2.MouseDown += binItem_MouseClick;
                        }
                        c.Tag = item.ID;
                        c.MouseDown += binItem_MouseClick;
                    }
                    main_panel.Controls.Add(rtfItem);
                    item.Control = rtfItem;
                }
                else if (item.Type == DataFormats.UnicodeText)
                {
                    string text = (string)item.Data;
                    TextItem textItem = new TextItem();
                    textItem.Name = "bin_" + item.ID;
                    textItem.textArea.Text = text;
                    textItem.MouseDown += binItem_MouseClick;
                    textItem.Tag = item.ID;
                    foreach (Control c in textItem.Controls)
                    {
                        foreach (Control c2 in c.Controls)
                        {
                            c2.Tag = item.ID;
                            c2.MouseDown += binItem_MouseClick;
                        }
                        c.Tag = item.ID;
                        c.MouseDown += binItem_MouseClick;
                    }
                    main_panel.Controls.Add(textItem);
                    item.Control = textItem;
                }
                updatePossitions();
            }
        }

        private void updatePossitions()
        {
            int yPoss = 0;
            foreach (Item itm in bin.Items.FindAll(itm => itm.IsPinned))
            {
                setPossition(itm, ref yPoss);
                itm.Control.BackColor = Color.Red;
            }
            foreach (Item itm in bin.Items.FindAll(itm => !itm.IsPinned))
            {
                setPossition(itm, ref yPoss);
                itm.Control.BackColor = Color.Transparent;
            }
            main_panel.AutoScrollPosition = new Point(main_panel.AutoScrollPosition.X, 0);
            main_panel.VerticalScroll.Value = 0;
            main_panel.Select();
            this.Refresh();
        }

        private void setPossition(Item itm, ref int yPoss)
        {
            int yOffset = 0;
            int xOffset = 10;
            int maxY = 300;
            if (yPoss == 0) yPoss = yOffset;
            if (itm.Control == null)
            {
                return;
            }
            try
            {
                RichTextBox rtb = (RichTextBox)itm.Control.Controls.Find("textArea", true)[0];
                if (rtb != null)
                {
                    int y = (rtb.GetLineFromCharIndex(rtb.Text.Length) + 1) * rtb.Font.Height + 1 + rtb.Margin.Vertical + 5;
                    if (y > maxY) y = maxY;
                    rtb.Size = new Size(rtb.Width, y);

                }
            }
            catch { }
            itm.Control.Tag = itm.ID;
            itm.Control.Location = new Point(xOffset, yPoss);
            itm.Control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            yPoss = yPoss + itm.Control.Height + yOffset;
        }

        private void binItem_MouseClick(object sender, MouseEventArgs e)
        {
            Item item = bin.Items.Find(itm => itm.ID == ((Control)sender).Tag.ToString());
            if (item.ClipboardObject != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        Clipboard.SetData(item.Type, item.Data);
                        this.Hide();
                        resize = false;
                        break;
                    case MouseButtons.Right:
                        item.SetPinned(!item.IsPinned);
                        updatePossitions();
                        break;
                }
            }
        }

        private void openBin()
        {
            if (!this.Visible)
            {
                updatePossitions();
                this.Location = Cursor.Position;
                this.Show();
            }
            else
                this.Hide();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            resize = true;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            resize = false;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (resize)
            {
                this.Size = new Size(this.Width, this.Height + e.Y);
            }
        }

        private void hide_Tick(object sender, EventArgs e)
        {
            int xoffset = 20;
            int yoffset = 20;
            if (this.Visible)
                if (Cursor.Position.X < this.Location.X - xoffset ||
                    Cursor.Position.X > this.Location.X + this.Width + xoffset ||
                    Cursor.Position.Y < this.Location.Y - yoffset ||
                    Cursor.Position.Y > this.Location.Y + this.Height + yoffset)
                {
                    this.Hide();
                    resize = false;
                }
        }
    }
}