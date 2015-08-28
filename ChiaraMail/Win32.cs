using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace ChiaraMail
{
    internal class Win32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr cursorHandle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);
        
        [DllImport("User32.dll")]
        public static extern Int32 FindWindow(String lpClassName, String lpWindowName);
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [SuppressUnmanagedCodeSecurity]
        internal static class UnsafeNativeMethods
        {
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        }

        /// <summary>
        /// to set enable/disable of Outlook controls
        /// </summary>
        /// <param name="isPressed"></param>
        public static void AllowForwarding(bool isPressed)
        {
            Enable(OutlookControls.ToButton, isPressed);
            Enable(OutlookControls.ToText, isPressed);
            Enable(OutlookControls.CcButton, isPressed);
            Enable(OutlookControls.CcText, isPressed);
            Enable(OutlookControls.BccButton, isPressed);
            Enable(OutlookControls.BccText, isPressed);
        }

        /// <summary>
        /// to enable/disable Outlook controls by passing Outlook control
        /// </summary>
        /// <param name="outlookControls"></param>
        /// <param name="value"></param>
        private static void Enable(OutlookControls outlookControls, bool value)
        {
            IntPtr controlButton = GetControl(outlookControls);
            EnableWindow(controlButton, value);
        }

        /// <summary>
        /// to get reference of Control
        /// </summary>
        /// <param name="outlookControls"></param>
        /// <returns></returns>
        private static IntPtr GetControl(OutlookControls outlookControls)
        {
            IntPtr hwndControl = IntPtr.Zero;

            //Get a handle for the Calculator Application main window
            int hwnd = FindWindow("rctrl_renwnd32", null);
            IntPtr control = GetAllChildrenWindowHandles((IntPtr)hwnd, 100, "AfxWndW"); //525494
            IntPtr control1 = GetAllChildrenWindowHandles((IntPtr)control, 100, "AfxWndW"); //525652
            IntPtr control2 = GetAllChildrenWindowHandles((IntPtr)control1, 100, "#32770"); //394446

            switch (outlookControls)
            {
                case OutlookControls.ToButton:
                    hwndControl = GetAllChildrenWindowHandles((IntPtr)control2, 100, "Button", "To&...");
                    break;
                case OutlookControls.ToText:
                    hwndControl = GetAllChildrenWindowHandles((IntPtr)control2, 100, "RichEdit20WPT", "To");
                    break;
                case OutlookControls.CcButton:
                    hwndControl = GetAllChildrenWindowHandles((IntPtr)control2, 100, "Button", "&Cc...");
                    break;
                case OutlookControls.CcText:
                    hwndControl = GetAllChildrenWindowHandles((IntPtr)control2, 100, "RichEdit20WPT", "Cc");
                    break;
                case OutlookControls.BccButton:
                    hwndControl = GetAllChildrenWindowHandles((IntPtr)control2, 100, "Button", "&Bcc...");
                    break;
                case OutlookControls.BccText:
                    hwndControl = GetAllChildrenWindowHandles((IntPtr)control2, 100, "RichEdit20WPT", "Bcc");
                    break;
            }

            return hwndControl;
        }

        /// <summary>
        /// to iterate sub/child control of Outlook window
        /// </summary>
        /// <param name="hParent"></param>
        /// <param name="maxCount"></param>
        /// <param name="strClass"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        static IntPtr GetAllChildrenWindowHandles(IntPtr hParent, int maxCount, string strClass, string strText = "")
        {
            IntPtr retValue = new IntPtr();
            int intIndex = 0;
            IntPtr prevChild = IntPtr.Zero;
            IntPtr currChild = IntPtr.Zero;

            bool blnGetNextControl = false;

            while (true && intIndex < maxCount)
            {
                currChild = FindWindowEx(hParent, prevChild, null, null);
                if (currChild == IntPtr.Zero) break;
                prevChild = currChild;

                StringBuilder lpClassName = new StringBuilder();
                StringBuilder lpWindowText = new StringBuilder();

                GetClassName((IntPtr)currChild, lpClassName, 100);
                UnsafeNativeMethods.GetWindowText((IntPtr)currChild, lpWindowText, lpWindowText.Capacity);

                if (blnGetNextControl)
                {
                    retValue = currChild;
                    break;
                }
                else if (!string.IsNullOrEmpty(strText) && lpClassName.ToString() == strClass && lpWindowText.ToString() == strText)
                {
                    retValue = currChild;
                    break;
                }
                else if (!string.IsNullOrEmpty(strText) && lpWindowText.ToString() == strText)
                {
                    blnGetNextControl = true;
                }
                else if (string.IsNullOrEmpty(strText) && lpClassName.ToString() == strClass)
                {
                    retValue = currChild;
                    break;
                }

                ++intIndex;
            }

            return retValue;
        }
    }

    enum OutlookControls
    {
        ToButton,
        ToText,
        CcButton,
        CcText,
        BccButton,
        BccText
    }
}