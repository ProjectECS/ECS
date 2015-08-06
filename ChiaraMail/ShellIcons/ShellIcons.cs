namespace ChiaraMail
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;

    public class ShellIcons
    {
        public static IconContainer GetIconForFile(string path, bool useExtension, bool largeIcon)
        {
            string str = useExtension ? Path.GetExtension(path) : path;
            return GetIconFromShell(str, useExtension, largeIcon);
        }

        private static IconContainer GetIconFromShell(string path, bool useExtension, bool largeIcon)
        {
            IconContainer container;
            ShellFileInfo structure = new ShellFileInfo();
            IconCriticalHandle critHandle = null;
            uint fileAttributes = 0x80;
            uint flags = 0x100;
            if (!largeIcon)
            {
                flags |= 1;
            }
            if (useExtension)
            {
                flags |= 0x10;
                if (string.IsNullOrEmpty(path))
                {
                    fileAttributes = 0x10;
                }
            }
            try
            {
                int num3 = Marshal.SizeOf(structure);
                if (ShellAPI.SHGetFileInfo(path, fileAttributes, ref structure, (uint) num3, flags) == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                if (structure.handle == IntPtr.Zero)
                {
                    throw new Exception("You have exceeded the maximum number of open GDI handles. Close some of the existing icon handles to avoid this condition.");
                }
                critHandle = new IconCriticalHandle(structure.handle);
                container = new IconContainer(critHandle);
            }
            finally
            {
                if ((critHandle == null) && (structure.handle != IntPtr.Zero))
                {
                    ShellAPI.DestroyIcon(structure.handle);
                }
            }
            return container;
        }
    }
}

