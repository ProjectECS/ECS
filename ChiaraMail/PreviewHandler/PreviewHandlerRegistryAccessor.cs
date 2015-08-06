using System.Collections.Generic;
using Microsoft.Win32;


namespace ChiaraMail
{
    /// <summary>
    /// Class used to traverse the registry to read all the file previewer registrations that exist on the system
    /// The majority of this code and logic comes from the Preview Handler Association Editor that Stephen Toub wrote
    /// and posted about on his blog.  http://blogs.msdn.com/toub/archive/2006/12/14/preview-handler-association-editor.aspx
    /// We made a few minor tweaks for our purposes, but the core of the logic is his.  Thanks to Stephen for sharing this code.
    /// </summary>
    internal static class PreviewHandlerRegistryAccessor
    {
        private const string BaseRegistryKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers";
        private const string BaseClsIDKey = @"HKEY_CLASSES_ROOT\{0}\shellex\{{8895b1c6-b41f-4c1c-a562-0d564250836f}}";
        private const string BaseClsIdKey2 = @"HKEY_CLASSES_ROOT\SystemFileAssociations\{0}\shellex\{{8895b1c6-b41f-4c1c-a562-0d564250836f}}";
        //private const string BaseClsIdKey3 = @"HKEY_CLASSES_ROOT\SystemFileAssociations\{0}\ShellEx\{{BB2E617C-0920-11d1-9A0B-00C04FC2D6C1}}";
        private static RegistrationData _data;
        internal static RegistrationData Data
        {
            get { return _data ?? (_data = LoadRegistrationInformation()); }
        }

        /// <summary>
        /// Read the registry to learn about the preview handlers that are available on this machine
        /// Return a structure containing 2 collections.  One of all the file extensions and whether we found a preview handler
        /// registered, and one of the preview handlers and their CLSIDs
        /// </summary>
        internal static RegistrationData LoadRegistrationInformation()
        {
            // Load and sort all preview handler information from registry
            var handlers = new List<PreviewHandlerInfo>();
            using (var handlersKey = Registry.LocalMachine.OpenSubKey(
                BaseRegistryKey, false))
            {
                if (handlersKey != null)
                    foreach (string id in handlersKey.GetValueNames())
                    {
                        var handler = new PreviewHandlerInfo
                                          {
                                              Id = id, 
                                              Name = handlersKey.GetValue(id, null) as string
                                          };
                        handlers.Add(handler);
                    }
            }
            handlers.Sort(delegate(PreviewHandlerInfo first, PreviewHandlerInfo second)
                              {
                                  if (first.Name == null) return 1;
                                  if (second.Name == null) return -1;
                                  return System.String.CompareOrdinal(first.Name, second.Name);
                              });

            // Create a lookup table of preview handler ID -> PreviewHandlerInfo
            var handlerMapping = new Dictionary<string, PreviewHandlerInfo>(handlers.Count);
            foreach (PreviewHandlerInfo handler in handlers)
            {
                handlerMapping.Add(handler.Id, handler);
            }

            // Get all classes/extensions from registry
            var extensions = Registry.ClassesRoot.GetSubKeyNames();

            // Find out what each extension is registered to be previewed with
            var extensionInfos = new List<ExtensionInfo>(extensions.Length);
            foreach (string extension in extensions)
            {
                if (extension.StartsWith("."))
                {
                    var info = new ExtensionInfo {Extension = extension};
                    var openSubKey = Registry.ClassesRoot.OpenSubKey(extension);
                    if (openSubKey != null)
                        info.PerceivedType = openSubKey.GetValue("PerceivedType", "").ToString();
                    var id = Registry.GetValue(
                        string.Format(BaseClsIDKey, extension),
                        null, null) as string ?? Registry.GetValue(
                            string.Format(BaseClsIdKey2, extension),
                            null, null) as string;
                    //try perceived type
                    if (id == null && info.PerceivedType!=null)
                    {
                        id = Registry.GetValue(
                            string.Format(BaseClsIDKey, info.PerceivedType),
                            null, null) as string ?? Registry.GetValue(
                                string.Format(BaseClsIdKey2, info.PerceivedType),
                                null, null) as string;
                        //if (id == null)
                        //{
                        //    id = Registry.GetValue(
                        //        string.Format(BaseClsIdKey3, info.perceivedType),
                        //        null, null) as string;
                        //}
                    }
                    PreviewHandlerInfo mappedHandler;
                    if (id != null && handlerMapping.TryGetValue(id, out mappedHandler)) info.Handler = mappedHandler;

                    extensionInfos.Add(info);
                }
            }

            // Return the information
            var data = new RegistrationData
                           {
                               Handlers = handlers, 
                               Extensions = extensionInfos
                           };

            return data;
        }
    }

    internal class RegistrationData
    {
        public List<PreviewHandlerInfo> Handlers;
        public List<ExtensionInfo> Extensions;
    }

    internal class PreviewHandlerInfo
    {
        public string Name;
        public string Id;

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? Id : Name;
        }
    }

    internal class ExtensionInfo
    {
        public string Extension;
        public string PerceivedType;
        public PreviewHandlerInfo Handler;
        public override string ToString() { return Extension; }
        public override bool Equals(object obj)
        {
            if (obj is ExtensionInfo)
            {
                return base.ToString().Equals(obj.ToString());
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
