using System;
using System.Collections.Generic;
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
            "Rich Text Format|RTF",
            "UnicodText",
            "Bitmap",
            "FileDrop"
        };

        //Constructor to initialise item data :
        public Item(object DataObject, string DataType, IDataObject ClipboardObject)
        {
            data = DataObject;
            type = DataType;
            clipboardObject = ClipboardObject;
        }

        //Seters & Geters :
        public object Data { get { return data; } }
        public string Type { get { return type; } }
        public IDataObject ClipboardObject { get { return clipboardObject; } }

        //Private variables storing all needed data :
        private object data;               // Data object converted to the type specified in DataFormatsIdentifiers for internal handling and preview
        private string type;               // The type of the "data" variable (from DataFormats class)
        private IDataObject clipboardObject; //Original clipboard object, stored so it can be set back to clipboard again
    }

    class Bin
    {
        private List<Item> items;
        private Thread updateBin;

        public List<Item> Items { get { return items; } }

        public Bin()
        {
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
                    IDataObject dataObject = Clipboard.GetDataObject();
                    addItem(dataObject);
                    Thread.Sleep(1000); //Wait 1 second
                }
            }
            catch { }
        }
        
        private void addItem(IDataObject Data)
        {
            string[] formats = Data.GetFormats(); //Get the compatible data formats that the IDataObject can be converted to
            string format = ""; //Create a variable to determin the data format

            //Determine the data type to be used for the data object :
            foreach(string s in Item.DataFormatIdentifiers)
            {
                if (formats.Contains<string>( ( (s.Contains("|"))?(s.Split('|').GetValue(0).ToString()):(s) ) ))
                {
                    format = ( (s.Contains("|")) ? (s.Split('|').GetValue(1).ToString()) : (s) );
                    break;
                }
            }
            //Check if format is supported :
            if( format == "")
            {
                //Format not supported
                return;
            }
            //Get the format type from Class DataFormats :
            format = typeof(DataFormats).GetProperty(format, BindingFlags.Static | BindingFlags.Public).GetValue(null, null).ToString(); //Get format from DataType in the same variable used before
            //Convert the IDataObject into the data specified earlier (this is done to handle the clipboard data internaly with a specific format) :
            object data = Data.GetData(format);
            //Search and delete the item if already exists before inserting it :
            Item itm = items.Find(i => i.Data == data);
            if( itm != null)
            {
                items.Remove(itm);
            }
            //Insert new item with data object in items if not already available :
            items.Insert(0, new Item(data,format,Data));
        }
    }
}
