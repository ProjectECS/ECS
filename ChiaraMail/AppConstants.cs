using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChiaraMail
{
    class AppConstants
    {
        public const string FromString = "From: ";
        public static decimal TotalChunks { get; set; }
        public static decimal CurrentChunk { get; set; }
        public const string TotalContentSize = "total content size=";
        public static double UploadedSize { get; set; }
        public static double TotalSize { get; set; }
    }

    public enum DownloadUpload
    {
        Download,
        Upload
    }
}