using System.Drawing;
using System.IO;

namespace ChiaraMail
{
    internal class Attachment
    {
        public int Index = -1;
        public string Name = "";
        public byte[] Content;
        public string Pointer = "0";
        public string Hash = "";
        public string ContentId = "";
        public bool Hidden;
        public int Position;
        public int? Flags;
        public int? AttachFlags;
        //public int? linkId = null;
        //public object dataObject = null;
        public int Type;
        public string FilePath = "";
        public Icon GetIcon(string path)
        {
            if (File.Exists(Name))
            {
                return Icon.ExtractAssociatedIcon(path);
            }
            return null;            
        }
    }
}
