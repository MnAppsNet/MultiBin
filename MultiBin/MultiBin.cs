using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Refresh();
            KeyboardEvents keyboardEvents = new KeyboardEvents();
            keyboardEvents.AddEvent("Testing", new Keys[] { Keys.RControlKey, Keys.M },this , "test", new object[0]);
            keyboardEvents.CaptureKeyboard();
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


    }
}
