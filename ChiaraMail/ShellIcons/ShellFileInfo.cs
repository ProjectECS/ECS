namespace ChiaraMail
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    internal struct ShellFileInfo
    {
        public IntPtr handle;
        public int iIcon;
        public uint attributes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=260)]
        public char[] displayName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=80)]
        public char[] typeName;
    }
}

