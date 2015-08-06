using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ChiaraMail
{
    internal class OutlookWin32Window : IWin32Window
    {

        //The Class implements the IWin32Window interface required for .net Messageboxes and Forms

        //<summary>
        //The <b>FindWindow</b> method finds a window by it's classname and caption. 
        //</summary>
        //<param name="lpClassName">The classname of the window (use Spy++)</param>
        //<param name="lpWindowName">The Caption of the window.</param>
        //<returns>Returns a valid window handle or 0.</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);


        //<summary>
        //This holds the window handle for the found Window.
        //</summary>
        private readonly IntPtr _windowsHandle;

        //<summary>
        //The <b>Handle</b> of the Outlook WindowObject.
        //</summary>
        public IntPtr Handle
        {
            get
            {
                return _windowsHandle;
            }
        }


        //<summary>
        //Constructor (New)
        //The <b>OutlookWin32Window</b> class could be used to get the parent IWin32Window for Windows.Forms and MessageBoxes.
        //</summary>
        //<param name="windowObject">The current WindowObject.</param>
        public OutlookWin32Window(object window,bool isWord)
        {
            string caption = window.GetType().InvokeMember("Caption", 
                                                           BindingFlags.GetProperty, null, window, null).ToString();
            _windowsHandle = FindWindow(isWord 
                ? "OpusApp" 
                : "rctrl_renwnd32", caption);
            if (!_windowsHandle.Equals(IntPtr.Zero) || !caption.EndsWith(" - Message")) return;
            caption = caption.Replace(" - Message", "");
            _windowsHandle = FindWindow(isWord 
                ? "OpusApp"
                : "rctrl_renwnd32", caption);
        }

        public OutlookWin32Window(string caption, bool isWord)
        {
            _windowsHandle = FindWindow(isWord 
                ? "OpusApp" 
                : "rctrl_renwnd32", caption);
            if (!_windowsHandle.Equals(IntPtr.Zero) || !caption.EndsWith(" - Message")) return;
            caption = caption.Replace(" - Message", "");
            _windowsHandle = FindWindow(isWord 
                                            ? "OpusApp" 
                                            : "rctrl_renwnd32", caption);
        }

    }
}
