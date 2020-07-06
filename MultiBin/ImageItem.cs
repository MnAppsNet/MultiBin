using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MultiBin
{
    public partial class ImageItem : UserControl
    {
        public ImageItem()
        {
            InitializeComponent();
        }

        private void fileName_CursorChanged(object sender, EventArgs e)
        {
            fileName.Select(0, 0);
        }
    }
}
