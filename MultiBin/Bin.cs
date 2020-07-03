using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiBin
{
    class Item
    {
        //Data formarts used to handle the clipboard data objects internaly. Clipboard objects are converted to those formats for internal use
        public static string[] DataFormatIdentifiers = {    //Identifier in IDataObject.GetFormats() | Property in DataFormats if they are different
            //"Rich Text Format",
            "UnicodeText",
            "Bitmap",
            "FileDrop"
        };

        //Constructor to initialise item data :
        public Item(object DataObject, string DataType, IDataObject ClipboardObject)
        {
            data = DataObject;
            type = DataType;
            clipboardObject = ClipboardObject;
            id = calculate_id(DataType, DataObject);
        }

        //Seters & Geters :
        public string ID { get { return id; } }
        public object Data { get { return data; } }
        public string Type { get { return type; } }
        public Control Control { get { return control; } set { control = value; } }
        public IDataObject ClipboardObject { get { return clipboardObject; } }

        //Private variables storing all needed data :
        private string id;                   // Item identified
        private object data;                 // Data object converted to the type specified in DataFormatsIdentifiers for internal handling and preview
        private string type;                 // The type of the "data" variable (from DataFormats class)
        private IDataObject clipboardObject; //Original clipboard object, stored so it can be set back to clipboard again
        private Control control;             //The graphical control that handles the item

        public static string calculate_id(string type, object data)
        {
            string id = "";
            if (type == "Bitmap")
            {
                Bitmap btm = (Bitmap)data;
                int bytes = btm.Width * btm.Height * (Image.GetPixelFormatSize(btm.PixelFormat) / 8);
                id = bytes.ToString() + btm.PixelFormat.ToString() + btm.VerticalResolution.ToString() + btm.Width.ToString() + btm.Height.ToString();
            }
            else if(type == "FileDrop")
            {
                string[] files = (string[])data;
                foreach(string s in files)
                {
                    id += s;
                }
            }
            else
            {
                id = data.ToString();
            }
            return id;
        }
    }

        class Bin
        {
            private List<Item> items;
            private Thread updateBin;
            private Control parent;
            public delegate void BinUpdated(Item item, bool remove);
            private event BinUpdated binUpdatedEvent;

            public List<Item> Items { get { return items; } }

            public Bin(Control Parent, BinUpdated binUpdated)
            {
                binUpdatedEvent += binUpdated;
                parent = Parent;

                items = new List<Item>();
                updateBin = new Thread(updateBinFromClipboard);
                updateBin.Start();
            }

            public void Dispose()
            {
                try
                {
                    items.Clear();
                    updateBin.Abort();
                }
                catch { }
            }

        private void updateBinFromClipboard()
        {
            try
            {
                while (true)
                {
                    if (Thread.CurrentThread.ThreadState == ThreadState.Aborted || Thread.CurrentThread.ThreadState == ThreadState.AbortRequested || Thread.CurrentThread.ThreadState == ThreadState.Stopped || Thread.CurrentThread.ThreadState == ThreadState.StopRequested || Thread.CurrentThread.ThreadState == ThreadState.Suspended || Thread.CurrentThread.ThreadState == ThreadState.SuspendRequested)
                    {
                        break;
                    }
                    IDataObject dataObject = null;
                    parent.Invoke((MethodInvoker)delegate
                    {
                        dataObject = Clipboard.GetDataObject();
                    });
                    if (dataObject != null)
                    {
                        addItem(dataObject);
                    }
                    Thread.Sleep(2000); //Wait 2 second
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

            private void addItem(IDataObject Data)
            {
                string[] formats = Data.GetFormats(); //Get the compatible data formats that the IDataObject can be converted to
                string format = ""; //Create a variable to determin the data format

                //Determine the data type to be used for the data object :
                foreach (string s in Item.DataFormatIdentifiers)
                {
                    if (formats.Contains<string>(((s.Contains("|")) ? (s.Split('|').GetValue(0).ToString()) : (s))))
                    {
                        format = ((s.Contains("|")) ? (s.Split('|').GetValue(1).ToString()) : (s));
                        break;
                    }
                }
                //Check if format is supported :
                if (format == "")
                {
                    //Format not supported
                    return;
                }
                //Get the format type from Class DataFormats :
                format = DataFormats.GetFormat(format).Name;
                //format = typeof(DataFormats).GetProperty(format, BindingFlags.Static | BindingFlags.Public).GetValue(null, null).ToString(); //Get format from DataType in the same variable used before
                //Convert the IDataObject into the data specified earlier (this is done to handle the clipboard data internaly with a specific format) :
                object data = Data.GetData(format);
                if (data == null) return;
                //Search and delete the item if already exists before inserting it :
                Item itm = items.Find(i => i.ID == Item.calculate_id(format,data));
                if (itm != null)
                {
                    if (items.Count > 0)
                    {
                        if (itm.Data.GetHashCode() == items[0].Data.GetHashCode())
                            return;
                    }
                    items.Remove(itm);
                    binUpdatedEvent.Invoke(itm, true);
                }
                //Insert new item with data object in items if not already available :
                Item newItem = new Item(data, format, Data);
                items.Insert(0, newItem);
                binUpdatedEvent.Invoke(newItem, false);

            }
        }
    }
