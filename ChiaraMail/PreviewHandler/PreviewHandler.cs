// Preview Handlers Revisted
// Bradley Smith - 2010/09/17

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.ComponentModel;
using System.Drawing;

namespace ChiaraMail
{   
    public class PreviewHandlerControl : Control
    {
        private string _className = "PreviewHandlerControl.";
        private string _errorMessage;
        private AppDomain _domain;
        private PreviewHandlerHost _previewHandlerHost;
        private readonly Timer _timer;
        private int _cycles;
        private string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                Invalidate();	// repaint the control
            }
        }

        /// <summary>
        /// Gets or sets the background colour of this PreviewHandlerHost.
        /// </summary>
        [DefaultValue("White")]
        public override sealed Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        /// <summary>
        /// Initialialises a new instance of the PreviewHandlerControl class.
        /// </summary>
        public PreviewHandlerControl()
        {
            //mCurrentPreviewHandlerGUID = Guid.Empty;
            BackColor = Color.White;
            Size = new Size(320, 240);

            // enable transparency
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            _timer = new Timer {Interval = 1000, Enabled = false};
            _timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            string source = _className+ "Timer_Tick";
            if (_previewHandlerHost == null)
            {
                _timer.Enabled = false;
                return;
            }
            //increment the cycles
            _cycles++;
            //check the status 
            PreviewHandlerHost.HandlerStatus status = _previewHandlerHost.Status;
            switch (_cycles)
            {
                //should have GUID with 1 second
                case 1:
                    if (status < PreviewHandlerHost.HandlerStatus.FoundGuid)
                    {
                        Logger.Info(source, "terminating host after 1 second - no GUID");
                        ReleaseHost();
                        _timer.Stop();
                        ErrorMessage = "Unable to preview file";
                    }
                    break;
                //allow up to 8 secs to instantiate
                case 8:
                    if (status < PreviewHandlerHost.HandlerStatus.Initialized)
                    {
                        Logger.Info(source, "terminating host after 8 seconds - failed to instantiate handler");
                        ReleaseHost();
                        _timer.Stop();
                        ErrorMessage = "Unable to preview file";
                    }
                    break;
                case 20:
                    Logger.Info(source, "terminating host after 20 seconds - failed to initialize handler");
                        ReleaseHost();
                        _timer.Stop();
                        ErrorMessage = "Unable to preview file";
                    break;
            }
        }

        public void Open(string filename)
        {
            string source = _className + "Open";
            try
            {
                //cleanup existing domain if it exists
                ReleaseHost();
                Controls.Clear();
                //sending a blank filename just releases the handler
                if (string.IsNullOrEmpty(filename)) return;
                if(PreviewInBrowser(filename))
                {
                    var browser = new WebBrowser {Dock = DockStyle.Fill};
                    if (!Utils.IsFileImage(filename))
                    {
                        browser.Navigate("file://" + filename);
                    }
                    else
                    {
                        var data = Convert.ToBase64String(File.ReadAllBytes(filename));
                        var newValue = string.Format("data:image/{0};base64,{1}", Path.GetExtension(filename), data);

                        browser.DocumentText = "0";
                        browser.Document.OpenNew(true);
                        browser.Document.Write("<img src='" + newValue + "'>");
                        browser.Refresh();
                    }

                    Controls.Add(browser);
                    return;
                }
                AppDomainSetup currentSetup = AppDomain.CurrentDomain.SetupInformation;
                var info = new AppDomainSetup
                {
                    ApplicationBase = currentSetup.ApplicationBase,
                    LoaderOptimization = currentSetup.LoaderOptimization,
                    ApplicationTrust= currentSetup.ApplicationTrust
                };                
                
                _domain = AppDomain.CreateDomain(
                    "Preview Host Domain " + Guid.NewGuid().ToString(), null, info);
                _previewHandlerHost = (PreviewHandlerHost)
                    _domain.CreateInstanceAndUnwrap("OutlookECS", "ChiaraMail.PreviewHandlerHost");
                //start the timer 
                _timer.Start();
                string result = _previewHandlerHost.Open(filename, ClientRectangle, Handle);
                _timer.Stop();
                Logger.Verbose(source, string.Format(
                    "preview handler returned {0} for {1}",
                    result, filename));
                switch (result)
                {
                    case "success":
                        break;
                    default:
                        ErrorMessage = result;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PreviewHandlerControl.Open", ex.ToString());
            }
            ErrorMessage = "Unable to preview file";
        }

        /// <summary>
        /// Releases the unmanaged resources used by the PreviewHandlerHost and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            GC.SuppressFinalize(this);
            ReleaseHost();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Paints the error message text on the PreviewHandlerHost control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_errorMessage != String.Empty)
            {
                // paint the error message
                TextRenderer.DrawText(
                    e.Graphics,
                    "\r" + _errorMessage,
                    new Font("Segoe UI",9F,FontStyle.Bold),
                    ClientRectangle,
                    ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.EndEllipsis
                );
            }
        }

        /// <summary>
        /// Resizes the hosted preview handler when this PreviewHandlerHost is resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            try
            {
                if (_previewHandlerHost != null)
                {
                    _previewHandlerHost.OnResize(ClientRectangle);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PreviewHandlerControl.OnResize", ex.ToString());
            }
        }

/*
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PreviewHandlerHost
            // 
            this.Font = new Font("Segoe UI", 10F, 
                FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.ResumeLayout(false);

        }
*/

        private void ReleaseHost()
        {
            if (_domain == null) return;
            try
            {
                if (_previewHandlerHost != null) _previewHandlerHost.Dispose();
                AppDomain.Unload(_domain);
            }
            finally
            {
                _domain = null;
            }
        }

        private bool PreviewInBrowser(string filename)
        {
            try
            {
                return true;

                string ext = Path.GetExtension(filename);
                if (string.IsNullOrEmpty(ext)) return false;
                switch (ext.ToLower())
                {
                    //case ".bmp": - doesn't load in browser, but does in IE?
                    case ".gif":
                    case ".ico":
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".txt":
                    //case ".bat":
                    case ".htm":
                    case ".html":
                    case ".xml":
                    case ".xps":
                    case ".config":
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PreviewInBrowser", ex.Message);
            }
            return false;
        }
    }

    public interface IPreviewHandlerHost
    {
        string Open(string filename, Rectangle clientRectangle, IntPtr handle);
        string Open(Stream stream, Guid previewHandler, Rectangle clientRectangle, IntPtr handle);
        void UnloadPreviewHandler();
        void OnResize(Rectangle clientRectangle);
        
    }

    [Serializable]
    public class PreviewHandlerHost : MarshalByRefObject, IPreviewHandlerHost, IDisposable
    {
        internal const string GUID_ISHELLITEM = "43826d1e-e718-42ee-bc55-a1e261c37bfe";
        private object _currentPreviewHandler;
        private Guid _currentPreviewHandlerGUID;
        private Stream _currentPreviewHandlerStream;
        private string _className = "PreviewHandlerHost.";
        private HandlerStatus _status;

        public enum HandlerStatus
        {
            None=0,
            FoundGuid = 1,
            Instantiated = 2,
            Initialized = 3,
            Failed=4
        }

        public PreviewHandlerHost()
        {
            _currentPreviewHandlerGUID = Guid.Empty;
        }
        public HandlerStatus Status
        {
            get { return _status; }
        }

        /// <summary>
        /// Opens the specified file using the appropriate preview handler and displays the result in this PreviewHandlerHost.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="clientRectangle"> </param>
        /// <param name="handle"> </param>
        /// <returns></returns>
        public string Open(string filename,Rectangle clientRectangle, IntPtr handle)
        {
            _status = HandlerStatus.None;
            UnloadPreviewHandler();

            if (String.IsNullOrEmpty(filename))
            {
                return "No file loaded";
            }
            if (!File.Exists(filename))
            {
                return "The file was not found";
            }

            // try to get GUID for the preview handler
            Guid guid = GetPreviewHandlerGUID(filename);
            if (guid != Guid.Empty)
            {
                _status = HandlerStatus.FoundGuid;
                try
                {
                    if (guid != _currentPreviewHandlerGUID)
                    {
                        _currentPreviewHandlerGUID = guid;

                        // need to instantiate a different COM type (file format has changed)
                        if (_currentPreviewHandler != null)
                        {
                            Marshal.FinalReleaseComObject(_currentPreviewHandler);
                            GC.Collect();
                        }
                        // use reflection to instantiate the preview handler type
                        Type comType = Type.GetTypeFromCLSID(_currentPreviewHandlerGUID);
                        _currentPreviewHandler = Activator.CreateInstance(comType);
                    }
                    _status = HandlerStatus.Instantiated;
                    //try stream first
                    if (_currentPreviewHandler is IInitializeWithStream)
                    {
                        // some handlers want an IStream (in this case, a file stream)
                        _currentPreviewHandlerStream = new MemoryStream();
                        using (var fs = File.Open(filename, FileMode.Open, FileAccess.Read))
                        {
                            fs.CopyTo(_currentPreviewHandlerStream);
                        }                        
                        var stream = new StreamWrapper(_currentPreviewHandlerStream);
                        ((IInitializeWithStream)_currentPreviewHandler).Initialize(stream, 0);
                    }
                    else if (_currentPreviewHandler is IInitializeWithFile)
                    {
                        // some handlers accept a filename
                        ((IInitializeWithFile)_currentPreviewHandler).Initialize(filename, 0);
                    }
                    else if (_currentPreviewHandler is IInitializeWithItem)
                    {
                        // a third category exists, must be initialised with a shell item
                        IShellItem shellItem;
                        SHCreateItemFromParsingName(filename, IntPtr.Zero, new Guid(GUID_ISHELLITEM), out shellItem);
                        ((IInitializeWithItem)_currentPreviewHandler).Initialize(shellItem, 0);
                    }
                    _status = HandlerStatus.Initialized;
                    var currentPreviewHandler = _currentPreviewHandler as IPreviewHandler;
                    if (currentPreviewHandler != null)
                    {
                        // bind the preview handler to the control's bounds and preview the content
                        Rectangle r = clientRectangle;
                        (currentPreviewHandler).SetWindow(handle, ref r);
                        Application.DoEvents();
                        (currentPreviewHandler).DoPreview();

                        return "success";
                    }
                }
                catch (Exception ex)
                {
                    _status = HandlerStatus.Failed;
                    return "The file could not be previewed\n" + ex.Message;
                }
            }
            else
            {
                return "No previewer is registered for this type of file";
            }
            return "Unable to preview file";
        }

        /// <summary>
        /// Opens the specified stream using the preview handler COM type with the provided GUID and displays the result in this PreviewHandlerHost.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="previewHandler"></param>
        /// <param name="clientRectangle"> </param>
        /// <param name="handle"> </param>
        /// <returns></returns>
        public string Open(Stream stream, Guid previewHandler, Rectangle clientRectangle, IntPtr handle)
        {
            UnloadPreviewHandler();

            if (stream == null)
            {
                return "The file could not be previewed.";
            }
            
            if (previewHandler != Guid.Empty)
            {
                try
                {
                    if (previewHandler != _currentPreviewHandlerGUID)
                    {
                        _currentPreviewHandlerGUID = previewHandler;

                        // need to instantiate a different COM type (file format has changed)
                        if (_currentPreviewHandler != null)
                        {
                            Marshal.FinalReleaseComObject(_currentPreviewHandler);
                            GC.Collect();
                        }

                        // use reflection to instantiate the preview handler type
                        Type comType = Type.GetTypeFromCLSID(_currentPreviewHandlerGUID);
                        _currentPreviewHandler = Activator.CreateInstance(comType);
                    }

                    var currentPreviewHandler = _currentPreviewHandler as IInitializeWithStream;
                    if (currentPreviewHandler != null)
                    {
                        // must wrap the stream to provide compatibility with IStream
                        _currentPreviewHandlerStream = stream;
                        var wrapped = new StreamWrapper(_currentPreviewHandlerStream);
                        (currentPreviewHandler).Initialize(wrapped, 0);
                    }

                    var handler = _currentPreviewHandler as IPreviewHandler;
                    if (handler != null)
                    {
                        // bind the preview handler to the control's bounds and preview the content
                        Rectangle r = clientRectangle;
                        (handler).SetWindow(handle, ref r);
                        (handler).DoPreview();

                        return "success";
                    }
                }
                catch (Exception ex)
                {
                    return "The file could not be previewed.\n" + ex.Message;
                }
            }
            else
            {
                return "No preview is registered for this type of file.";
            }

            return "failure";
        }

        /// <summary>
        /// Unloads the preview handler hosted in this PreviewHandlerHost and closes the file stream.
        /// </summary>
        public void UnloadPreviewHandler()
        {
            string source = _className + "UnloadPreviewHandler";
            try
            {
                if (_currentPreviewHandler != null)// is IPreviewHandler)
                {
                    // explicitly unload the content
                    try
                    {
                        ((IPreviewHandler)_currentPreviewHandler).Unload();
                    }
                    finally
                    {
                        //do full release on unload
                        Marshal.FinalReleaseComObject(_currentPreviewHandler);
                        GC.Collect();
                        _currentPreviewHandler = null;
                    }
                }
                if (_currentPreviewHandlerStream != null)
                {
                    _currentPreviewHandlerStream.Close();
                    _currentPreviewHandlerStream = null;
                }
                //clear the guid every time
                _currentPreviewHandlerGUID = Guid.Empty;
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
        }

        public void OnResize(Rectangle clientRectangle)
        {
            if (_currentPreviewHandler != null)//is IPreviewHandler)
            {
                // update the preview handler's bounds to match the control's
                Rectangle r = clientRectangle;
                ((IPreviewHandler)_currentPreviewHandler).SetRect(ref r);
            }
        }

        private Guid GetPreviewHandlerGUID(string filename)
        {
            string extension = Path.GetExtension(filename);
            RegistrationData data = PreviewHandlerRegistryAccessor.Data;
            var ei = data.Extensions.Find(
                search => search.Extension == extension);
            if (ei != null)
            {
                var handler = ei.Handler;
                if (handler != null) return new Guid(handler.Id);
            }
            return Guid.Empty;
        }

        #region P/Invoke

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        static extern void SHCreateItemFromParsingName(
            [In][MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            [In] IntPtr pbc, [In][MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out][MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppv
        );

        #endregion


        public void Dispose()
        {
            try
            {
                UnloadPreviewHandler();
            }
            catch { }
        }
    }
 
    #region COM Interop

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("8895b1c6-b41f-4c1c-a562-0d564250836f")]
    internal interface IPreviewHandler
    {
        void SetWindow(IntPtr hwnd, ref Rectangle rect);
        void SetRect(ref Rectangle rect);
        void DoPreview();
        void Unload();
        void SetFocus();
        void QueryFocus(out IntPtr phwnd);
        [PreserveSig]
        uint TranslateAccelerator(ref Message pmsg);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("b7d14566-0509-4cce-a71f-0a554233bd9b")]
    internal interface IInitializeWithFile
    {
        void Initialize([MarshalAs(UnmanagedType.LPWStr)] string pszFilePath, uint grfMode);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("b824b49d-22ac-4161-ac8a-9916e8fa3f7f")]
    internal interface IInitializeWithStream
    {
        void Initialize(IStream pstream, uint grfMode);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("7F73BE3F-FB79-493C-A6C7-7EE14E245841")]
    interface IInitializeWithItem
    {
        void Initialize(IShellItem psi, uint grfMode);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid(PreviewHandlerHost.GUID_ISHELLITEM)]
    interface IShellItem
    {
        void BindToHandler(IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)]Guid bhid, [MarshalAs(UnmanagedType.LPStruct)]Guid riid, out IntPtr ppv);
        void GetParent(out IShellItem ppsi);
        void GetDisplayName(uint sigdnName, out IntPtr ppszName);
        void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);
        void Compare(IShellItem psi, uint hint, out int piOrder);
    };

    /// <summary>
    /// Provides a bare-bones implementation of System.Runtime.InteropServices.IStream that wraps an System.IO.Stream.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    internal class StreamWrapper : IStream
    {

        private readonly Stream _inner;

        /// <summary>
        /// Initialises a new instance of the StreamWrapper class, using the specified System.IO.Stream.
        /// </summary>
        /// <param name="inner"></param>
        public StreamWrapper(Stream inner)
        {
            _inner = inner;
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="ppstm"></param>
        public void Clone(out IStream ppstm)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="grfCommitFlags"></param>
        public void Commit(int grfCommitFlags)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="pstm"></param>
        /// <param name="cb"></param>
        /// <param name="pcbRead"></param>
        /// <param name="pcbWritten"></param>
        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="libOffset"></param>
        /// <param name="cb"></param>
        /// <param name="dwLockType"></param>
        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Reads a sequence of bytes from the underlying System.IO.Stream.
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="cb"></param>
        /// <param name="pcbRead"></param>
        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            long bytesRead = _inner.Read(pv, 0, cb);
            if (pcbRead != IntPtr.Zero) Marshal.WriteInt64(pcbRead, bytesRead);
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public void Revert()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Advances the stream to the specified position.
        /// </summary>
        /// <param name="dlibMove"></param>
        /// <param name="dwOrigin"></param>
        /// <param name="plibNewPosition"></param>
        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            long pos = _inner.Seek(dlibMove, (SeekOrigin)dwOrigin);
            if (plibNewPosition != IntPtr.Zero) Marshal.WriteInt64(plibNewPosition, pos);
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="libNewSize"></param>
        public void SetSize(long libNewSize)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns details about the stream, including its length, type and name.
        /// </summary>
        /// <param name="pstatstg"></param>
        /// <param name="grfStatFlag"></param>
        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG
                           {
                               cbSize = _inner.Length,
                               type = 2,
                               pwcsName = (_inner is FileStream) 
                                    ? ((FileStream) _inner).Name 
                                    : String.Empty
                           };
            // stream type
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        /// <param name="libOffset"></param>
        /// <param name="cb"></param>
        /// <param name="dwLockType"></param>
        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Writes a sequence of bytes to the underlying System.IO.Stream.
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="cb"></param>
        /// <param name="pcbWritten"></param>
        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            _inner.Write(pv, 0, cb);
            if (pcbWritten != IntPtr.Zero) Marshal.WriteInt64(pcbWritten, cb);
        }
    }

    #endregion

}//end namespace