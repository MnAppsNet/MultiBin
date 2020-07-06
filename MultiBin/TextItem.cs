using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiBin
{
    public partial class TextItem : UserControl
    {
        public TextItem()
        {
            InitializeComponent();
        }

        private void textArea_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            //((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }

        private void textArea_CursorChanged(object sender, EventArgs e)
        {
            textArea.Select(0, 0);
        }
    }
}
