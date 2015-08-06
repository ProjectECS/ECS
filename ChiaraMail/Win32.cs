using System;
using System.Runtime.InteropServices;

namespace ChiaraMail
{
    internal class Win32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr cursorHandle);
    }
}
