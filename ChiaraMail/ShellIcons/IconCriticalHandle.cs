namespace ChiaraMail
{
    using System;
    using System.Runtime.InteropServices;

    public class IconCriticalHandle : CriticalHandle
    {
        public IconCriticalHandle(IntPtr handle) : base(IntPtr.Zero)
        {
            base.SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            if (!this.IsInvalid)
            {
                if (ShellAPI.DestroyIcon(base.handle))
                {
                    base.SetHandleAsInvalid();
                    return true;
                }
                return false;
            }
            return true;
        }

        public IntPtr Handle
        {
            get
            {
                return base.handle;
            }
        }

        public override bool IsInvalid
        {
            get
            {
                return (base.handle == IntPtr.Zero);
            }
        }
    }
}

