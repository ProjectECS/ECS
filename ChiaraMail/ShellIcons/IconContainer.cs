namespace ChiaraMail
{
    using System;
    using System.Drawing;

    public class IconContainer : IDisposable
    {
        private IconCriticalHandle handle;
        private System.Drawing.Icon icon;

        public IconContainer(IconCriticalHandle critHandle)
        {
            this.handle = critHandle;
            this.icon = System.Drawing.Icon.FromHandle(critHandle.Handle);
        }

        public void Dispose()
        {
            this.icon = null;
            if (this.handle != null)
            {
                this.handle.Dispose();
                this.handle = null;
            }
        }

        public System.Drawing.Icon Icon
        {
            get
            {
                return this.icon;
            }
        }
    }
}

