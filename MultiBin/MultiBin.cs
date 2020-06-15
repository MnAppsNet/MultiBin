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

        private Bin bin;
        private bool resize = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Refresh();
            KeyboardEvents keyboardEvents = new KeyboardEvents();
            keyboardEvents.AddEvent("Testing", new Keys[] { Keys.LControlKey, Keys.LShiftKey, Keys.Space },this , "test", new object[0]);
            keyboardEvents.CaptureKeyboard();
            bin = new Bin(this, new Bin.BinUpdated(binUpdated));
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
                    main_panel.Controls.Add(imageItem);
                    item.Control = imageItem;
                }
                else if (item.Type == DataFormats.Rtf)
                {
                    string rtf = (string)item.Data;
                    TextItem rtfItem = new TextItem();
                    rtfItem.Name = "bin_" + item.ID;
                    rtfItem.textArea.Rtf = rtf;
                    main_panel.Controls.Add(rtfItem);
                    item.Control = rtfItem;
                }
                else if (item.Type == DataFormats.UnicodeText)
                {
                    string text = (string)item.Data;
                    TextItem textItem = new TextItem();
                    textItem.Name = "bin_" + item.ID;
                    textItem.textArea.Text = text;
                    main_panel.Controls.Add(textItem);
                    item.Control = textItem;
                }
                updatePossitions();
            }
        }

        private void updatePossitions()
        {
            int yOffset = 5;
            int xOffset = 10;
            int yPoss = yOffset;
            foreach (Item itm in bin.Items)
            {
                if (itm.Control == null)
                {
                    continue;
                }
                itm.Control.Tag = itm;
                itm.Control.MouseClick += binItem_MouseClick;
                itm.Control.Location = new Point(xOffset, yPoss);
                itm.Control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                yPoss = yPoss + itm.Control.Height + yOffset;
            }
            this.Refresh();
        }

        private void binItem_MouseClick(object sender, MouseEventArgs e)
        {
            Item item = (Item)((Control)sender).Tag;
            if (item.ClipboardObject != null)
                Clipboard.SetDataObject(item.ClipboardObject);

        }

        private void test()
        {
            IDataObject data = Clipboard.GetDataObject();
            string t = "";
            
            foreach( string s in data.GetFormats())
            {
                t += Environment.NewLine + s;
            }
            MessageBox.Show(t);

            data.GetData(DataFormats.Rtf);

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
    }
}