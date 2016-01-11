using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Xml;
using ChiaraMail.Controls;
using ChiaraMail.FormRegions;
using ChiaraMail.Forms;
using ChiaraMail.Properties;
using Ionic.Zip;
using Ionic.Zlib;
using Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;
using Redemption;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Drawing;

namespace ChiaraMail
{
    internal class Utils
    {
        private const string CLASS_NAME = "Utils.";

        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr cursorHandle);

        internal static void OpenFile(object arg)
        {
            try
            {
                if (arg == null) return;
                var args = arg as string[];
                if (args == null) return;
                var startInfo = new ProcessStartInfo
                    {
                        Arguments = args.Length > 1 ? args[1] : "",
                        WindowStyle = ProcessWindowStyle.Normal,
                        FileName = args[0],
                        UseShellExecute = true,
                    };
                if (args.Length > 1)
                {
                    startInfo.Arguments = args[1];
                }
                try
                {
                    using (var process = Process.Start(startInfo)){}
                }
                catch (Exception ex)
                {
                    Logger.Info("OpenFile", string.Format("error for {0}: {1}",
                        args[0], ex.Message));
                    if (ex.Message.Contains("No application is associated with the specified file"))
                    {
                        Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " + args[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("OpenFile",ex.ToString());
            }            
            
        }

        internal static void OpenHelp()
        {
            Process.Start("http://www.chiaramail.com/faq.html");
        }

        internal static bool HasChiaraHeader(Outlook.MailItem mail)
        {
            const string SOURCE = CLASS_NAME + "HasChiaraHeader";
            //bool result = false;
            try
            {
                Logger.Info(SOURCE, "checking " + mail.Subject);
                object prop;
                //try PropertyAccessor first
                //MAPIUtils may fail - fall back to property accessor
                var accessor = mail.PropertyAccessor;
                //first check for the custom prop
                try
                {
                    prop = accessor.GetProperty(Resources.public_strings_root +
                        Resources.content_header.ToLower());
                }
                catch
                {
                    prop = null;
                }
                if (prop != null) return true;
                //if that fails try parsing the mail header
                try
                {
                    prop = accessor.GetProperty(ThisAddIn.PR_MAIL_HEADER_TAG);
                }
                catch
                {
                    prop = null;
                }
                if (prop != null)
                {
                    var header = prop.ToString();
                    //only need Content-Pointer header to be legal
                    if (header.ToLower().Contains(
                        Resources.content_header.ToLower()))
                        return true;
                }
                //fall back to MAPIUtils
                MAPIUtils mapiUtils;
                try
                {
                    mapiUtils = RedemptionLoader.new_MAPIUtils();
                }
                catch
                {
                    mapiUtils = null;
                }

                if (mapiUtils != null)
                {
                    //first check for the custom prop                    
                    try
                    {
                        var propTag = mapiUtils.GetIDsFromNames(mail,
                                                                Resources.public_strings_root,
                                                                Resources.content_header) | ThisAddIn.PT_STRING8;
                        prop = mapiUtils.HrGetOneProp(mail, propTag);
                    }
                    catch
                    {
                        prop = null;
                    }
                    if (prop != null) return true;
                    //if that fails try parsing the mail header
                    try
                    {
                        prop = mapiUtils.HrGetOneProp(mail, ThisAddIn.PR_MAIL_HEADER);
                    }
                    catch
                    {
                        prop = null;
                    }
                    if (prop != null)
                    {
                        var header = Convert.ToString(prop);
                        //only need Content-Pointer header to be legal
                        if (header.ToLower().Contains(
                            Resources.content_header.ToLower()))
                            return true;
                    }
                }                
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            Logger.Info(SOURCE, "returning false for " + mail.Subject);
            return false;
        }

        internal static bool HasChiaraHeader(MessageItem item)
        {
            if(!string.IsNullOrEmpty(GetHeader(item, Resources.content_header)))
                return true;
            //if that fails try parsing the mail header
            object prop;
            try
            {
                prop = item.Fields[ThisAddIn.PR_MAIL_HEADER];
            }
            catch
            {
                prop = null;
            }
            if (prop == null) return false;
            var header = Convert.ToString(prop);
            //only need Content-Pointer header to be legal
            return header.ToLower().Contains(
                Resources.content_header.ToLower());
        }

        internal static bool HasChiaraHeader(RDOMail item)
        {
            if (!string.IsNullOrEmpty(GetHeader(item, Resources.content_header)))
                return true;
            //if that fails try parsing the mail header
            object prop;
            try
            {
                prop = item.Fields[ThisAddIn.PR_MAIL_HEADER];
            }
            catch
            {
                prop = null;
            }
            if (prop == null) return false;
            var header = Convert.ToString(prop);
            //only need Content-Pointer header to be legal
            return header.ToLower().Contains(
                Resources.content_header.ToLower());
        }

        internal static void GetChiaraHeaders(MessageItem item, out string pointer, 
            out string server, out string port, out string contentKey, out string userAgent)
        {
            pointer = GetHeader(item, Resources.content_header);
            server = GetHeader(item, Resources.server_header);
            port = GetHeader(item, Resources.port_header);
            contentKey = GetHeader(item, Resources.encrypt_key_header2);
            userAgent = GetHeader(item, Resources.user_agent_header);
        }

        internal static string GetMailItemHeader(Outlook.MailItem item, string header)
        {
            object prop;
            var accessor = item.PropertyAccessor;
            //first check for the custom prop
            try
            {
                prop = accessor.GetProperty(Resources.public_strings_root +
                    header.ToLower());
            }
            catch
            {
                prop = null;
            }
            if (prop != null) return prop as string;
            //if that fails use RDO
            var session = RedemptionLoader.new_RDOSession();
            session.Logon("",false,false,false,0,true);
            var mail = (RDOMail)session.GetRDOObjectFromOutlookObject(item, false);
            return mail == null 
                ? string.Empty 
                : GetHeader(mail, header);
        }
        
        private static string GetHeader(MessageItem item, string header)
        {
            try
            {
                var propTag = item.GetIDsFromNames(ThisAddIn.PS_INTERNET_HEADERS, header) | ThisAddIn.PT_STRING8;
                return Convert.ToString(item.Fields[propTag]);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GetHeader(RDOMail item, string header)
        {
            try
            {
                var propTag = item.GetIDsFromNames(ThisAddIn.PS_INTERNET_HEADERS, header) | ThisAddIn.PT_STRING8;
                return Convert.ToString(item.Fields[propTag]);
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static string GetRecordKey(SafeMailItem safItem)
        {
            const string SOURCE = CLASS_NAME + "GetRecordKey";
            try
            {
                object prop = safItem.Fields[ThisAddIn.PR_RECORD_KEY];
                if (prop != null)
                {
                    var array = (object[]) prop;
                    var bytes = new byte[array.Length];
                    for (var i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = Convert.ToByte(array[i]);
                    }
                    return BitConverter.ToString(bytes).Replace("-","");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return "";
        }

        internal static string GetSenderAddress(Outlook.MailItem item)
        {
            string address = "";
            try
            {
                if (item.Sent)
                {
                    SafeMailItem safMail = RedemptionLoader.new_SafeMailItem();
                    if (safMail != null)
                    {
                        safMail.Item = item;
                        address = safMail.Sender.SMTPAddress;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(CLASS_NAME + "GetSenderAddress", ex.ToString());
            }
            return address;
        }

        internal static bool IsSpoofed(string displayName, string address)
        {
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(address))
                return false;
            //does the display name contain an address?
            var email = Regex.Match(displayName, @"[<[ ]??([\w.']+@\w+\.[a-z]{2,10})[]> ]", 
                RegexOptions.IgnoreCase).Groups[1].Value;
            if (string.IsNullOrEmpty(email)) return false;
            return !address.Replace("'","").Trim().Equals(
                email.Replace("'", "").Trim(), StringComparison.CurrentCultureIgnoreCase);
        }

        internal static void CreateTempFolder(string key)
        {
            const string SOURCE = CLASS_NAME + "CreateTempFolder";
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    Logger.Warning(SOURCE, "key is null");
                }
                else
                {
                    var tempFolder = Path.Combine(Path.GetTempPath(),
                        "ChiaraMail", key);
                    if (Directory.Exists(tempFolder))
                    {
                        Logger.Verbose(SOURCE, "found existing temp directory for " + key);
                    }
                    else
                    {
                        Logger.Verbose(SOURCE, "creating temp directory for " + key);
                        Directory.CreateDirectory(tempFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal static void CleanTempFolder(string key)
        {
            const string SOURCE = CLASS_NAME + "CleanTempFolder";
            try
            {
                var path = Path.Combine(Path.GetTempPath(), "ChiaraMail", key);
                //does the folder exist?
                if (!Directory.Exists(path)) return;
                if (string.IsNullOrEmpty(key))
                {
                    //delete all sub-directories                    
                    var di = new DirectoryInfo(path);
                    var subfolders = di.GetDirectories();
                    if (subfolders.Length > 0)
                    {
                        Logger.Info(SOURCE, string.Format(
                            "deleting {0} attachment directories",
                            subfolders.Length));
                        foreach (var folder in subfolders)
                        {
                            folder.Delete(true);
                        }
                    }
                    return;
                }


                //is there more than one region with a matching key?
                var count = 0;
                foreach (var item in Globals.FormRegions)
                {
                    if (item.GetType() == typeof (DynamicReadingPane))
                    {
                        var region =
                            (DynamicReadingPane) item;
                        if (region.RecordKey.Equals(key)) count++;
                    }
                    else
                    {
                        var region =
                            (DynamicInspector) item;
                        if (region.RecordKey.Equals(key)) count++;
                    }
                }
                if (count.Equals(0))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal static void ReadHeaders(Outlook.MailItem item, ref string contentPointer,
            ref string serverName, ref string serverPort, ref string encryptKey, 
            ref string encryptKey2, ref string duration, ref string userAgent, ref bool allowForwarding)
        {
            const string SOURCE = CLASS_NAME + "ReadHeaders";
            try
            {
                Outlook.PropertyAccessor accessor = item.PropertyAccessor;
                object prop;
                try
                {
                    prop = accessor.GetProperty(ThisAddIn.PR_MAIL_HEADER_TAG);
                }
                catch
                {
                    prop = null;
                }
                if (prop == null || string.IsNullOrEmpty(Convert.ToString(prop)))
                {
                    var props = accessor.GetProperties(new[]
                        {
                            ThisAddIn.MAIL_HEADER_GUID + Resources.content_header.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.server_header.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.port_header.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.encrypt_key_header.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.encrypt_key_header2.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.duration_header.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.user_agent_header.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.user_allow_forwarding_header.ToLower()
                        });
                    if(props == null) return;
                    contentPointer = Convert.ToString(props[0]);
                    if (string.IsNullOrEmpty(contentPointer)) return;
                    serverName = props[1] is string && !string.IsNullOrEmpty(props[1])
                        ? Convert.ToString(props[1]) 
                        : Resources.default_server;
                    serverPort = props[2] is string && !string.IsNullOrEmpty(props[2])
                        ? Convert.ToString(props[2]) 
                        : Resources.default_port;
                    encryptKey = props[3] is string && !string.IsNullOrEmpty(props[3])
                        ? Convert.ToString(props[3])
                        : string.Empty;
                    encryptKey2 = props[4] is string && !string.IsNullOrEmpty(props[4])
                        ? Convert.ToString(props[4])
                        : string.Empty;
                    duration = props[5] is string && !string.IsNullOrEmpty(props[5])
                        ? Convert.ToString(props[5]) 
                        : "0";
                    userAgent = props[6] is string && !string.IsNullOrEmpty(props[6])
                        ? Convert.ToString(props[6])
                        : string.Empty;
                    allowForwarding = props[7] is bool && !string.IsNullOrEmpty(props[7])
                        ? Convert.ToBoolean(props[7])
                        : false;
                    return;
                }
                var headerBlock = prop.ToString();
                var headers = ParseHeaders(headerBlock);
                //Content-Pointer 
                headers.TryGetValue(Resources.content_header.ToLower(),out contentPointer);
                //must have content pointer to be legit
                if (string.IsNullOrEmpty(contentPointer)) return;
                //Server-Name
                headers.TryGetValue(Resources.server_header.ToLower(), out serverName);
                //Server-Port
                headers.TryGetValue(Resources.port_header.ToLower(), out serverPort);
                //old Key
                headers.TryGetValue(Resources.encrypt_key_header.ToLower(), out encryptKey);
                //new Key
                headers.TryGetValue(Resources.encrypt_key_header2.ToLower(), out encryptKey2);
                //duration
                headers.TryGetValue(Resources.duration_header.ToLower(), out duration);
                //User Agent
                headers.TryGetValue(Resources.user_agent_header.ToLower(), out userAgent);
                //Allow Forwarding
                string strAllowForwarding = string.Empty;
                headers.TryGetValue(Resources.user_allow_forwarding_header.ToLower(), out strAllowForwarding);

                if (!string.IsNullOrEmpty(strAllowForwarding))
                {
                    allowForwarding = Convert.ToBoolean(strAllowForwarding);
                }

                //use configured server and port if not supplied in a header
                if (string.IsNullOrEmpty(serverName)) serverName = Resources.default_server;
                if (string.IsNullOrEmpty(serverPort)) serverPort = Resources.default_port;
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal static void ReadUpdateHeaders(Outlook.MailItem item, ref string senderAddress,
            ref string contentPointer, ref string serverName, ref string serverPort)
        {
            const string SOURCE = CLASS_NAME + "ReadUpdateHeaders";
            try
            {
                Outlook.PropertyAccessor accessor = item.PropertyAccessor;
                object prop;
                try
                {
                    prop = accessor.GetProperty(ThisAddIn.PR_MAIL_HEADER_TAG);
                }
                catch
                {
                    prop = null;
                }
                if (prop == null || string.IsNullOrEmpty(Convert.ToString(prop)))
                {
                    var props = accessor.GetProperties(new[]
                        {
                            ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_pointer.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_server.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_port.ToLower(),
                            ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_sender.ToLower()
                        });
                    if (props == null) return;
                    contentPointer = Convert.ToString(props[0]);
                    if (string.IsNullOrEmpty(contentPointer)) return;
                    serverName = Convert.ToString(props[1]);
                    serverPort = Convert.ToString(props[2]);
                    senderAddress = Convert.ToString(props[3]);
                    return;
                }
                var headerBlock = prop.ToString();
                var headers = ParseHeaders(headerBlock);
                //Content-Pointer 
                headers.TryGetValue(Resources.update_header_pointer.ToLower(), out contentPointer);
                //must have content pointer to be legit
                if (string.IsNullOrEmpty(contentPointer)) return;
                //Server-Name
                headers.TryGetValue(Resources.update_header_server.ToLower(), out serverName);
                //Server-Port
                headers.TryGetValue(Resources.update_header_port.ToLower(), out serverPort);
                //sender
                headers.TryGetValue(Resources.update_header_sender.ToLower(), out senderAddress);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal static void ReadUpdateHeaders(RDOMail item, ref string senderAddress,
            ref string contentPointer, ref string serverName, ref string serverPort)
        {
            const string SOURCE = CLASS_NAME + "ReadUpdateHeaders";
            try
            {
                var prop = item.Fields[MAPITags.PR_TRANSPORT_MESSAGE_HEADERS];                
                if (prop == null || string.IsNullOrEmpty(Convert.ToString(prop)))
                {
                    var contentProp = item.Fields[ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_pointer.ToLower()];
                    var serverNameProp= item.Fields[ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_server.ToLower()];
                    var serverPortProp = item.Fields[ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_port.ToLower()];
                    var senderProp = item.Fields[ThisAddIn.MAIL_HEADER_GUID + Resources.update_header_sender.ToLower()];
                    contentPointer = contentProp == null 
                        ? string.Empty
                        : Convert.ToString(contentProp);
                    if (string.IsNullOrEmpty(contentPointer)) return;
                    serverName = serverNameProp == null
                        ? string.Empty 
                        : Convert.ToString(serverNameProp);
                    serverPort = serverPortProp == null
                        ? string.Empty 
                        : Convert.ToString(serverPortProp);
                    senderAddress = senderAddress == null 
                        ? string.Empty
                        : Convert.ToString(senderProp);
                    return;
                }
                var headerBlock = prop.ToString();
                //parse the headers
                var headers = new Dictionary<string, string>();
                var array = Regex.Split(headerBlock, @"(\r\n(?!\s))");
                foreach (var t in array)
                {
                    if (t.Equals("\r\n")) continue;
                    var pos = t.IndexOf(':');
                    if (pos <= 0) continue;
                    var header = t.Substring(0, pos).Trim().ToLower();
                    //some headers may be duplicated?
                    if (headers.ContainsKey(header)) continue;
                    var value = t.Substring(pos + 1).Trim();
                    headers.Add(header, value);
                }          
                //Content-Pointer 
                headers.TryGetValue(Resources.update_header_pointer.ToLower(), out contentPointer);
                //must have content pointer to be legit
                if (string.IsNullOrEmpty(contentPointer)) return;
                //Server-Name
                headers.TryGetValue(Resources.update_header_server.ToLower(), out serverName);
                //Server-Port
                headers.TryGetValue(Resources.update_header_port.ToLower(), out serverPort);
                //sender
                headers.TryGetValue(Resources.update_header_sender.ToLower(), out senderAddress);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        //private static string EvalStringProp(object prop)
        //{
        //    if (prop is string) return prop.ToString();
        //    return string.Empty;
        //}
        internal static string ParseHeader(string mailHeader, string headerId)
        {
            string value = "";
            try
            {
                int start = mailHeader.IndexOf(headerId,StringComparison.InvariantCultureIgnoreCase);
                if (start > 0)
                {
                    //per RFC the end of the field name is defined by a colon
                    start = mailHeader.IndexOf(':', start) + 1;
                    //value is everything else on the current line
                    int end = mailHeader.IndexOf('\r', start);
                    //but this could be last header with no trailing return
                    value = end > start 
                        ? mailHeader.Substring(start, end - start).Trim() 
                        : mailHeader.Substring(start).Trim();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ParseHeader", ex.ToString());
            }
            return value;
        }

        internal static string GetFileSize(string path)
        {
            try
            {
                var fi = new FileInfo(path);
                return FormatFileSize(fi.Length);
            }
            catch (Exception ex)
            {
                Logger.Error("GetFileSize", ex.ToString());
            }
            return "";
        }

        internal static string FormatFileSize(long bytes)
        {
            if (bytes >= 1073741824)
            {
                var size = Decimal.Divide(bytes, 1073741824);
                return String.Format("{0:##.#} GB", size);
            }
            if (bytes >= 1048576)
            {
                var size = Decimal.Divide(bytes, 1048576);
                return String.Format("{0:##.#} MB", size);
            }
            if (bytes >= 1024)
            {
                var size = Decimal.Divide(bytes, 1024);
                return String.Format("{0:##.#} KB", size);
            }
            if (bytes > 0 & bytes < 1024)
            {
                var size = bytes;
                return String.Format("{0:##.#} bytes", size);
            }
            return "0 bytes";
        }

        internal static string ExtractBody(string html)
        {
            var regex = new Regex(@"<body\b[^>]*>(.*?)</body>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return  regex.Match(html).Groups[1].Value;
        }

        internal static IEnumerable<string> GetImgElements(string content, string attribute, string value)
        {
            var list = new List<string>();
            var regex = new Regex(@"(<img\b[^>]*" + 
                attribute + @"=['""]" + 
                value + @"['""]*\b[^>]*>)", 
                RegexOptions.IgnoreCase);
            var match = regex.Match(content);
            while (match.Success)
            {
                list.Add(match.Value);
                match = match.NextMatch();
            }
            return list;
        }

        internal static string GetAttrValueFromImg(string match, string attribute)
        {
            return Regex.Match(match, @"<img\b[^>]*" + attribute + @"=['""]{0,1})([^ '"">]+)[^>]*>", 
                RegexOptions.IgnoreCase).Groups[2].Value;
        }

        internal static Match MatchPointer(string value)
        {
            return Regex.Match(value, @"pointer:(\d+):(.[^'""]+)", RegexOptions.IgnoreCase);
        }
     
        internal static string ReplaceAttrValue(string original, string tagName, 
            string attribute, string value)
        {
            var pattern = @"(<img\b[^>]*" +
                          attribute + @"=['""]{0,1})([^ '"">]+)([^>]*>)";
            return Regex.Replace(original, pattern, 
                "$1" + value + "$3", RegexOptions.IgnoreCase);
        }

        internal static List<Match> GetImageFileLinks(string content, string attribute)
        {
            var list = new List<Match>();
            var regex = new Regex(@"(<img\b[^>]*" + attribute + @"=['""]{0,1})(file:///[^ '""]+)(\b[^>]*>)",
                RegexOptions.IgnoreCase);
            var matchResult = regex.Match(content);
            while (matchResult.Success)
            {
                list.Add(matchResult);
                matchResult = matchResult.NextMatch();
            }
            return list;
        } 

        internal static List<Match> GetImageLinks(string content, string attribute)
        {
            var list = new List<Match>();
            var regex = new Regex(@"(<img\b[^>]*" + attribute + @"=['""]{0,1})(pointer:[^ '""]+)(\b[^>]*>)", 
                RegexOptions.IgnoreCase);
            var matchResult = regex.Match(content);
            while (matchResult.Success)
            {
                list.Add(matchResult);
                matchResult = matchResult.NextMatch();
            }
            return list;
        }

        internal static List<Match> GetContentIdLinks(string content, string attribute)
        {
            var list = new List<Match>();
            var regex = new Regex(@"(<img\b[^>]*" + attribute + @"=['""]{0,1})(cid:[^ '""]+)(\b[^>]*>)",
                RegexOptions.IgnoreCase);
            var matchResult = regex.Match(content);
            while (matchResult.Success)
            {
                list.Add(matchResult);
                matchResult = matchResult.NextMatch();
            }
            return list;
        }

        internal static List<Match> GetVideoLinks(string content)
        {
            var list = new List<Match>();
            if (string.IsNullOrEmpty(content)) return list;
            var regex = new Regex(@"(<video\b[^>]*src=['""]{0,1})([^ '""]+)(\b[^>]*>)",
                RegexOptions.IgnoreCase);
            var matchResult = regex.Match(content);
            while (matchResult.Success)
            {
                list.Add(matchResult);
                matchResult = matchResult.NextMatch();
            }
            return list;
        } 

        internal static void CollapseImgLinks(ref string content)
        {
            //revert data value in embedded images with pointer:<pointer>:<fileName>
            var links = GetImageLinks(content, "alt");
            if (links.Count == 0) return;
            foreach (var link in links)
            {
                //get the pointer from Alt
                var pointer = link.Groups[2].Value;
                //drop it into src
                var revisedLink = ReplaceAttrValue(link.Value, "img", "src", pointer);
                content = content.Replace(link.Value, revisedLink);
            }
        }

        internal static string FetchEmbeddedFileImages(string content, List<Match> matches, Dictionary<string,string> pointerMap, 
                                                       string baseUrl, Account account, EcsConfiguration configuration,
                                                       string senderAddress, string serverName, string serverPort, string encryptKey2,
                 string userAgent, ref List<string> embeddedFileNames)
        {
            //extract the src paths, if any
            foreach (var match in matches)
            {
                //extract the filename from the src 
                var filePath = HttpUtility.UrlDecode(match.Groups[2].Value);
                if (string.IsNullOrEmpty(filePath)) continue;
                var fileName = Path.GetFileName(filePath);
                if(string.IsNullOrEmpty(fileName)) continue;
                if (!pointerMap.ContainsKey(fileName)) continue;
                var pointer = pointerMap[fileName];
                embeddedFileNames.Add(fileName);
                //save to modified 'src' path
                var path = Path.Combine(baseUrl, pointer);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, fileName);
                if (!File.Exists(path))
                {
                    //get the content and write it to the path
                    GetEmbeddedFile(pointer, path, account, configuration, senderAddress,
                                    serverName, serverPort, "", encryptKey2, userAgent);
                }
                //replace the path
                var newPath = "file:///" + path.Replace("\\", "/");
                //make sure the src value is wrapped with quotes
                if (content.Contains("src=" + filePath))
                {
                    //no - wrap with single quotes
                    newPath = "'" + newPath + "'";
                }
                content = content.Replace(filePath, newPath);
            }
            return content;
        }

        internal static string FetchEmbeddedImages(string content, List<Match> matches, 
            string baseUrl, Account account, EcsConfiguration configuration, string senderAddress, 
            string serverName, string serverPort, string encryptKey2, string userAgent)
        {
            //extract the src paths, if any
            foreach (var match in matches)
            {                
                //extract the link from the src 
                var ptrMatch = MatchPointer(match.Groups[2].Value);
                var pointer = ptrMatch.Groups[1].Value;
                var fileName = HttpUtility.UrlDecode(ptrMatch.Groups[2].Value);

                if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(pointer)) continue;
                //save to modified 'src' path
                var path = Path.Combine(baseUrl, pointer);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, fileName);
                if (!File.Exists(path))
                {
                    //get the content and write it to the path
                    GetEmbeddedFile(pointer, path, account, configuration, senderAddress,
                                    serverName, serverPort, "", encryptKey2, userAgent);
                }
                //read the bytes, base64 encode 
                var data = Convert.ToBase64String(File.ReadAllBytes(path));
                var newValue = match.Groups[1].Value +
                               string.Format("data:image/{0};base64,{1}", Path.GetExtension(path), data) +
                               match.Groups[3].Value;
                //make sure we keep the pointer in the alt attribute
                //replace the link with base64 data protocol
                content = content.Replace(match.Value, newValue);
            }
            return content;
        }

        internal static string LoadEmbeddedImageAttachments(Outlook.MailItem mailItem,
            string content)
        {
            //do we have any straight image links?
            var matches = GetContentIdLinks(content, "src");
            if (matches.Count == 0) return content;
            var safMail = RedemptionLoader.new_SafeMailItem();
            safMail.Item = mailItem;
            var attachments = safMail.Attachments;
            if (attachments == null || attachments.Count == 0) return content;
            //extract the src paths, if any
            foreach (var match in matches)
            {
                //extract the attachment name 
                var attachName = match.Groups[2].Value.Replace("cid:","");
                if (string.IsNullOrEmpty(attachName)) continue;
                //find the attachment
                foreach (Redemption.Attachment attachment in attachments)
                {
                    bool hidden;
                    string contentId;
                    GetAttachProps(attachment, out contentId, out hidden);
                    if (contentId != attachName) continue;
                    var bytes = attachment.AsArray != null
                        ? attachment.AsArray as byte[]
                        : null;
                    if(bytes == null) break;
                    //read the bytes, base64 encode 
                    var data = Convert.ToBase64String(bytes);
                    var newValue = string.Format("data:image/{0};base64,{1}",
                                    Path.GetExtension(attachment.FileName), data);
                    //replace the link with base64 data protocol
                    content = content.Replace(match.Groups[2].Value, newValue);
                }                
            }
            return content;
        }

        internal static string LoadEmbeddedVideos(string content, List<Match> matches,
            Dictionary<string, Attachment> attachList, string baseUrl, Account account,
            EcsConfiguration configuration, string senderAddress,
            string serverName, string serverPort, string encryptKey2, string userAgent)
        {
            //extract the src paths
            foreach (var match in matches)
            {
                //extract the filename from the src 
                var filePath = match.Groups[2].Value;
                var fileName = HttpUtility.UrlDecode(filePath.Substring(filePath.LastIndexOf("/")).Replace("/", ""));
                if (string.IsNullOrEmpty(fileName)) continue;
                //find pointer for matching attachment
                var pointer = GetAttachPointer(attachList, fileName);
                if (string.IsNullOrEmpty(pointer)) continue;
                //save to modified 'src' path
                var path = Path.Combine(baseUrl, pointer);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, fileName);
                if (!File.Exists(path))
                {
                    //get the content and write it to the path
                    GetEmbeddedFile(pointer, path, account, configuration, senderAddress,
                                    serverName, serverPort, "", encryptKey2, userAgent);
                }
                //read the bytes, base64 encode 
                var data = Convert.ToBase64String(File.ReadAllBytes(path));
                var ext = Path.GetExtension(path);
                if (!string.IsNullOrEmpty(ext))
                    ext = ext.Replace(".", "");
                var dataUri = string.Format("data:video/{0};base64,{1}", ext, data);
                //update the src link with the local path
                content = content.Replace(filePath, dataUri);
            }
            return content;
        }

        internal static string GetAttachPointer(Dictionary<string, Attachment> attachList, string fileName)
        {
            foreach (var attachment in attachList.Values.Where(attachment => attachment.Name == fileName))
            {
                return attachment.Pointer;
            }
            return string.Empty;
        }

        internal static void LoadAttachments(Outlook.MailItem item, string[] pointers,string baseUrl,
            Account account, string senderAddress, string serverName, string serverPort, 
            string encryptKey, string encryptKey2, List<string> EmbeddedFileNames, 
            ref Dictionary<string,Attachment> attachList,
            ref Dictionary<string, Redemption.Attachment> embedded,
            ref Panel panelAttach, ref int upperWidth, ref int upperHeight)
        {
            const string SOURCE = CLASS_NAME + "LoadAttachments";
            SafeMailItem safMail;
            try
            {
                safMail = RedemptionLoader.new_SafeMailItem();
            }
            catch (Exception ex)
            {
                Logger.Warning(SOURCE,string.Format(
                    "unable to load attachments for {0}, error instantiating SafeMailItem: {1}",
                    item.Subject,ex.Message));
                return;
            }
            safMail.Item = item;
            var safAttachments = safMail.Attachments;
            var index = 0;
            foreach (Redemption.Attachment safAttach in safAttachments)
            {
                index++;
                var attach = new Attachment
                                 {
                                     Index = index, 
                                     Name = safAttach.DisplayName
                                 };
                if (EmbeddedFileNames.Contains(safAttach.DisplayName)) continue;
                var btn = new AttachPanel
                              {
                                  Caption = safAttach.DisplayName
                              };
                switch (safAttach.Type)
                {
                    case 1: //byvalue
                        if (string.IsNullOrEmpty(pointers[index])) continue;
                        //is it hidden or have contentId
                        string contentId;
                        bool hidden;
                        GetAttachProps(safAttach, out contentId, out hidden);
                        if (hidden)
                        {
                            continue;
                        }
                        attach.Pointer = pointers[index];
                        //get the image
                        var container = ShellIcons.GetIconForFile(
                            attach.Name, true, false);
                        btn.Picture = container.Icon.ToBitmap();
                        btn.Pointer = attach.Pointer;
                        attachList.Add(attach.Pointer, attach);
                        break;
                    case 5: //embedded - will have null/0 pointer
                        btn.Pointer = "embedded:" + index.ToString(CultureInfo.InvariantCulture);
                        embedded.Add(btn.Pointer, safAttach);
                        //use default envelope picture for button 
                        break;
                }
                panelAttach.Controls.Add(btn);
                if (btn.Width > upperWidth) upperWidth = btn.Width;
                if (btn.Height > upperHeight) upperHeight = btn.Height;
            }
        }

        internal static string GetRootPath()
        {
            //assembly CodeBase and strip off file:// prefix
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:", "");
            while (path.StartsWith("/") || path.StartsWith("\\"))
            {
                path = path.Substring(1);
            }
            return Path.GetDirectoryName(path);
        }

        internal static string GetBrowserVersion()
        {
            try
            {
                var browser = new WebBrowser {Visible = false};
                return browser.Version.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        
        internal static void ConfigureEditorForPlainText(ToolStrip toolStrip)
        {
            //hide most items on first toolbar
            for (int i = 0; i < toolStrip.Items.Count - 1; i++)
            {
                ToolStripItem item = toolStrip.Items[i];
                switch (item.ToolTipText)
                {
                    case "Cut":
                    case "Copy":
                    case "Paste":
                    case "Undo":
                    case "Redo":
                        item.Visible = true;
                        break;
                    default:
                        item.Visible = false;
                        break;
                }
            }
        }

        internal static void GetAttachProps(Redemption.Attachment attach, out string contentId,
            out bool hidden)
        {
            const string SOURCE = CLASS_NAME + "GetAttachProps";
            contentId = null;
            hidden = false;
            try
            {
                object prop;
                try
                {
                    prop = attach.Fields[ThisAddIn.PR_ATTACHMENT_HIDDEN];
                }
                catch
                {
                    prop = null;
                }
                if (prop != null)
                {
                    hidden = Convert.ToBoolean(prop);
                }
                //only check hidden attachments for contentId
                if (hidden)
                {
                    try
                    {
                        prop = attach.Fields[ThisAddIn.PR_ATTACH_CONTENT_ID];
                    }
                    catch
                    {
                        prop = null;
                    }
                    if (prop != null)
                    {
                        contentId = Convert.ToString(prop);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }            
        }

        internal static void SetAttachProps(Outlook.Attachment attach, string contentId,
            bool hidden)
        {
            const string SOURCE = CLASS_NAME + "SetAttachProps";
            try
            {
                MAPIUtils mapiUtils;
                try
                {
                    mapiUtils = RedemptionLoader.new_MAPIUtils();
                }
                catch (Exception ex1)
                {
                    Logger.Error(SOURCE, "failed to instantiate MAPIUtils object: " + ex1.Message);
                    return;
                }
                mapiUtils.HrSetOneProp(attach, ThisAddIn.PR_ATTACH_CONTENT_ID, contentId, true);
                mapiUtils.HrSetOneProp(attach, ThisAddIn.PR_ATTACHMENT_HIDDEN, true, true);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal static bool PreviewPaneVisible(string viewXml)
        {
            if (string.IsNullOrEmpty(viewXml)) return true;
            var doc = new XmlDocument();
            doc.LoadXml(viewXml);
            try
            {
                var node = doc.SelectSingleNode("//previewpane");
                if (node == null) return false;
                var visible = node.SelectSingleNode("visible");
                return visible != null && visible.InnerText == "1";
            }
            catch
            {
                return false;
            }
        }

        internal static string GetFilePath(string recordKey, int index, string name)
        {
            if (string.IsNullOrEmpty(recordKey)) return "";            
            var path = Path.Combine(Path.GetTempPath(), "ChiaraMail",
                                recordKey, Convert.ToString(index));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, name);
            return path;
        }

        internal static void GetFile(string pointer, string name, int index, string recordKey,
            Account account, EcsConfiguration configuration, string senderAddress, string serverName, 
            string serverPort, string encryptKey, string encryptKey2, string userAgent, out string path, out string hash)
        {
            const string SOURCE = CLASS_NAME + "GetFile";
            path = "";
            hash = "";
            if (string.IsNullOrEmpty(recordKey)) return;
            try
            {
                path = GetFilePath(recordKey, index, name);
                if (!File.Exists(path))
                {
                    //fetch the content
                    string content;
                    string error;
                    ContentHandler.FetchContent(account.SMTPAddress, configuration, senderAddress,
                                                pointer, serverName, serverPort, true,
                                                out content, out error);
                    if (string.IsNullOrEmpty(content))
                    {
                        Logger.Warning(SOURCE, string.Format(
                            "failed to retrieve content for {0} using pointer {1} from {2}: {3}",
                            name, pointer, senderAddress, error));
                        return;
                    }
                    ContentHandler.SaveAttachment(
                        content, encryptKey, encryptKey2, userAgent, path);                    
                }
                //return the hash
                byte[] buf = File.ReadAllBytes(path);
                hash = Cryptography.GetHash(buf);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        //internal static bool ContainsPlaceholder(string plainText)
        //{
        //    //changed link
        //    var test = plainText.Replace("\r", "").Replace("\n", "").Replace(" ","");
        //    if (test.Contains(Resources.placeholder_text.ToLower().Replace(" ", ""))) return true;
        //    if (test.Contains(Resources.placeholder_text0.ToLower().Replace(" ", ""))) return true;
        //    if (test.Contains(Resources.placeholder_text1.ToLower().Replace(" ", ""))) return true;
        //    if (test.Contains(Resources.placeholder_text2.ToLower().Replace(" ", ""))) return true;
        //    if (test.Contains(Resources.placeholder_text3.ToLower().Replace(" ", ""))) return true;
        //    if (test.Contains(Resources.placeholder_text.ToLower().Replace(" ", "").Replace("download_extension","download_ecs"))) return true;
        //    if (test.Contains(Resources.placeholder_text0.ToLower().Replace(" ", "").Replace("download_extension", "download_ecs"))) return true;
        //    if (test.Contains(Resources.placeholder_text1.ToLower().Replace(" ", "").Replace("download_extension", "download_ecs"))) return true;
        //    return test.Contains(Resources.placeholder_text2.ToLower().Replace(" ", "").Replace("download_extension", "download_ecs")) 
        //        //placeholder_text3 has current download link, so use Replace to test for the old link 
        //        || test.Contains(Resources.placeholder_text3.ToLower().Replace(" ", "").Replace("download_ecs", "download_extension"));
        //}

        internal static void ReleaseObject(object comObject)
        {
            if(comObject == null) return;
            Marshal.ReleaseComObject(comObject);
        }

        internal static bool IsValidSmtp(string email)
        {
            return Regex.IsMatch(email, 
                @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", 
                RegexOptions.IgnoreCase);
        }

        internal static bool IsValidPointer(string value)
        {
            int pointer;
            if(!Int32.TryParse(value, out pointer)) return false;
            return pointer == 0 || pointer % 8 == 0;
        }

        internal static bool IsValidPort(string value)
        {
            int port;
            if (!Int32.TryParse(value, out port)) return false;
            return port > 0 && port < 1024;
        }

        public static string DiagnosticInfo
        {
            get
            {
                var sbInfo = new StringBuilder();
                var app = Globals.ThisAddIn.Application;
                var session = app.Session;
                var defaultStore = session.DefaultStore;
                sbInfo.AppendFormat("{0} v. {1}{2}",
                    Resources.product_name, AssemblyFullVersion, Environment.NewLine);

                sbInfo.AppendFormat("Operating System: {0} {1}{2}",
                    Environment.OSVersion,
                    Environment.Is64BitOperatingSystem ? "x64" : "x86",
                    Environment.NewLine);
                sbInfo.AppendFormat("Outlook Version: {0} {1}{2}",
                    app.Version,
                    Environment.Is64BitProcess ? "x64" : "",
                    Environment.NewLine);
                sbInfo.AppendFormat("UI Language ID: {0}{1}",
                    app.LanguageSettings.LanguageID[MsoAppLanguageID.msoLanguageIDUI], Environment.NewLine);
                sbInfo.AppendFormat("Connection Mode: {0}{1}",
                    session.ExchangeConnectionMode, Environment.NewLine);
                sbInfo.AppendFormat("Outlook Profile: {0}{1}",
                    session.CurrentProfileName, Environment.NewLine);
                sbInfo.AppendFormat("Default Store: {0}{1}",
                    defaultStore.DisplayName, Environment.NewLine);
                sbInfo.AppendFormat("Connected Add-ins:{1}{0}{1}",
                    ConnectedAddIns(app), Environment.NewLine);
                return sbInfo.ToString();
            }
        }

        public static void ZipDirectory(string sourceDirectory, string destinationFile,
            int level, bool recursive)
        {
            using (var zip = new ZipFile(destinationFile))
            {
                zip.CompressionLevel = (CompressionLevel)level;
                foreach (var file in Directory.GetFiles(sourceDirectory))
                {
                    zip.AddFile(file);
                }
                if (!recursive) return;
                //zip files in sub-directories
                foreach (var childDirectory in Directory.GetDirectories(sourceDirectory))
                {
                    ZipChildren(zip, childDirectory);
                }

                zip.Save();
            }
        }

        private static void ZipChildren(ZipFile zip, string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                zip.AddFile(file);
            }
            //zip files in sub-directories
            foreach (var childDirectory in Directory.GetDirectories(directory))
            {
                ZipChildren(zip, childDirectory);
            }
        }

        public static string AssemblyFullVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return version.ToString();
            }
        }

        public static string GetBody(string body)
        {
            const string SOURCE = CLASS_NAME + "GetBody";
            string content = body;
            try
            {
                //find the body start tag
                content = Regex.Match(body, @"<body\b[^>]*>(.*?)</body>",
                                      RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups[1].Value;
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return content;
        }

        public static IEnumerable<Attachment> GetEmbeddedImages(Outlook.MailItem item)
        {
            const string SOURCE = CLASS_NAME + "GetEmbeddedImages";
            var list = new List<Attachment>();
            SafeMailItem safMail;
            try
            {
                safMail = RedemptionLoader.new_SafeMailItem();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, string.Format(
                    "unable to work with attachments for {0}, failed to instantiate SafeMailItem: {1}",
                    item.Subject, ex.Message));
                return list;
            }
            //need to save the item first before we can work with the SafeMailItem
            item.Save();
            safMail.Item = item;
            var colAttach = safMail.Attachments;
            //walk down the existing 
            foreach (Redemption.Attachment rdoAttach in colAttach)
            {
                string contentId;
                bool hidden;
                GetAttachProps(rdoAttach, out contentId, out hidden);
                if (string.IsNullOrEmpty(contentId)) continue;
                try
                {
                    //could fail if attachment is too big
                    var content = rdoAttach.AsArray != null
                        ? rdoAttach.AsArray as byte[]
                        : null; //.Fields[ThisAddIn.PR_ATTACH_DATA_BIN]);
                    if (content == null) continue;
                    {
                        list.Add(new Attachment
                        {
                            Content = content,
                            ContentId = contentId,
                            Name = rdoAttach.FileName,
                            Index = rdoAttach.Index
                        });
                    }
                }
                catch
                {
                    Logger.Warning(SOURCE,
                                   "failed to retrieve content from embedded image");
                }
            }
            return list;
        }

        public static void DeleteReplaced(Outlook.MailItem item, List<int> replaced)
        {
            var attachments = item.Attachments;
            for (var i = attachments.Count; i > 0; i--)
            {
                var index = attachments[i].Index;
                if (replaced.Contains(index))
                    attachments.Remove(index);
            }
        }

        public static bool PostAttachments(Outlook.MailItem item, Account account, EcsConfiguration configuration,
            string encryptKey, string recips, ref List<string> pointers, OutlookWin32Window win, bool noPlaceholder)
        {
            const string SOURCE = CLASS_NAME + "PostAttachments";
            try
            {
                //get the bytes for the placeholder text
                var placeholder = Encoding.UTF8.GetBytes(Resources.placeholder_text);
                SafeMailItem safMail;
                try
                {
                    safMail = RedemptionLoader.new_SafeMailItem();
                }
                catch (Exception ex)
                {
                    Logger.Error(SOURCE, String.Format(
                        "unable to work with attachments for {0}, failed to instantiate SafeMailItem: {1}",
                        item.Subject, ex.Message));
                    return false;
                }
                //need to save the item first before we can work with the SafeMailItem
                item.Save();
                safMail.Item = item;
                var colAttach = safMail.Attachments;
                /* Outlook will move any embedded images to the head of the attachments table
                * if that's the case then we need to remove and re-add the other attachments 
                * so that the pointer list will match the finished order
                */
                var hidden = false;
                string contentId;
                var savedAttach = new Dictionary<int, byte[]>();
                //do we have any embedded images?
                foreach (Redemption.Attachment rdoAttach in colAttach)
                {
                    GetAttachProps(rdoAttach, out contentId, out hidden);
                    if (hidden) break;
                }
                if (hidden)
                {
                    //walk through in reverse order
                    //delete and reattach each non-hidden attachment
                    for (var i = colAttach.Count; i > 0; i--)
                    {
                        Redemption.Attachment rdoAttach = colAttach[i];
                        GetAttachProps(rdoAttach, out contentId, out hidden);
                        if (hidden) continue;
                        if (rdoAttach.Type.Equals(5)) //embedded
                        {
                            var msg = rdoAttach.EmbeddedMsg;
                            rdoAttach.Delete();
                            colAttach.Add(msg, 5);
                        }
                        else
                        {
                            var path = Path.Combine(Path.GetTempPath(), "ChiaraMail", rdoAttach.FileName);
                            var displayName = rdoAttach.DisplayName;
                            if (File.Exists(path)) File.Delete(path);
                            rdoAttach.SaveAsFile(path);
                            rdoAttach.Delete();
                            rdoAttach = colAttach.Add(path, 1, Type.Missing, displayName);
                            //get the bytes and drop those in the dictionary, linked to the current index
                            savedAttach.Add(rdoAttach.Index, File.ReadAllBytes(path));
                            File.Delete(path);
                        }
                    }
                }

                //now loop through and collect the content (except for embedded messages)
                var attachList = new List<Attachment>();
                bool showForm = false;
                foreach (Redemption.Attachment rdoAttach in colAttach)
                {
                    var attach = new Attachment { Type = rdoAttach.Type };
                    switch (rdoAttach.Type)
                    {
                        case (int)Outlook.OlAttachmentType.olEmbeddeditem:
                            //is this an ECS attachment?
                            var msg = rdoAttach.EmbeddedMsg;
                            if (HasChiaraHeader(msg))
                            {
                                ForwardEmbeddedECS(msg, recips, account);
                            }
                            //always add
                            attachList.Add(attach);
                            break;
                        case (int)Outlook.OlAttachmentType.olByReference:
                        case (int)Outlook.OlAttachmentType.olOLE:
                            attachList.Add(attach);
                            break;
                        case (int)Outlook.OlAttachmentType.olByValue:
                            showForm = true;
                            //we may have already gotten the bytes
                            if (savedAttach.Count > 0 && savedAttach.ContainsKey(rdoAttach.Index))
                            {
                                attach.Content = savedAttach[rdoAttach.Index];
                            }
                            if (attach.Content == null || attach.Content.Length == 0)
                            {
                                //try just read the bytes from the binary property
                                //this could fail if the attachment is too big
                                try
                                {
                                    attach.Content = rdoAttach.AsArray != null
                                        ? rdoAttach.AsArray as byte[]
                                        : null;//.Fields[ThisAddIn.PR_ATTACH_DATA_BIN]);
                                }
                                catch
                                {
                                    attach.Content = null;
                                }
                            }
                            if (attach.Content == null)
                            {
                                //save to disk then get the bytes
                                var path = Path.Combine(Path.GetTempPath(), "ChiaraMail", rdoAttach.FileName);
                                if (File.Exists(path)) File.Delete(path);
                                rdoAttach.SaveAsFile(path);
                                attach.Content = File.ReadAllBytes(path);
                                File.Delete(path);
                            }
                            if (attach.Content != null)
                            {
                                attach.Index = rdoAttach.Index;
                                attach.Name = rdoAttach.DisplayName;
                                attachList.Add(attach);
                            }
                            else
                            {
                                Logger.Warning(SOURCE,
                                               "aborting: failed to retrieve content for " + rdoAttach.DisplayName);
                                MessageBox.Show(String.Format(
                                    "Unable to retrieve original content from {0}",
                                    rdoAttach.DisplayName), Resources.product_name,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                return false;
                            }
                            break;
                    }
                }
                if (!showForm)
                {
                    pointers.AddRange(attachList.Select(attach => attach.Pointer));
                    return true;
                }
                //use the WaitForm to upload the attachments
                var form = new WaitForm
                {
                    Attachments = attachList,
                    Account = account,
                    Configuration = configuration,
                    Recips = recips,
                    EncryptKey2 = encryptKey
                };
                //use encryptKey2 for new post
                if (form.ShowDialog(win) == DialogResult.OK)
                {
                    //post succeeded for all attachments
                    //get the pointers
                    pointers.AddRange(form.Attachments.Select(attach => attach.Pointer));
                    //don't replace attachment bytes if we are sending content
                    if (noPlaceholder) return true;
                    //loop back through to replace the original content with the placeholder bytes
                    foreach (Redemption.Attachment rdoAttach in colAttach)
                    {
                        if (rdoAttach.Type.Equals(1)) //OlAttachmentType.olByValue)
                        {
                            rdoAttach.Fields[ThisAddIn.PR_ATTACH_DATA_BIN] = placeholder;
                        }
                    }
                    return true;
                }
                //get the pointer list anyway so we can delete the items that got posted
                pointers.AddRange(form.Attachments
                                      .TakeWhile(attach => !String.IsNullOrEmpty(attach.Pointer))
                                      .Select(attach => attach.Pointer));
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return false;
        }

        public static void AssignHeaders(Outlook.MailItem item, Account account, 
            List<string> pointers, string encryptKey, bool encrypted)
        {
            const string SOURCE = CLASS_NAME + "AssignHeaders";
            try
            {
                var accessor = item.PropertyAccessor;
                //set each property individually 
                //accessor fails to set the content header if there are too many pointer
                var pointerString = String.Join(" ", pointers.ToArray());
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.content_header,
                    pointerString);
                var config = account.Configurations[account.DefaultConfiguration];
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.server_header,
                    config.Server);
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.port_header,
                    config.Port);
                if (encrypted && !String.IsNullOrEmpty(encryptKey))
                {
                    //only write to new encrypt key header
                    accessor.SetProperty(ThisAddIn.MAIL_HEADER_GUID +
                                         Resources.encrypt_key_header2,
                                         encryptKey);
                }
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.user_agent_header,
                    Resources.label_help_group + " " + AssemblyFullVersion);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        public static void DeletePointers(List<string> pointers, Account account, EcsConfiguration configuration)
        {
            const string SOURCE = CLASS_NAME + "DeletePointers";
            Logger.Info(SOURCE, String.Format(
                "deleting {0} pointers after failed attachment upload",
                pointers.Count));
            foreach (var pointer in pointers)
            {
                string error;
                ContentHandler.DeleteContent(account.SMTPAddress,
                    configuration, pointer, out error, true);
            }
        }

        public static Dictionary<string, string> MapAttachments(string[] pointers, Outlook.Attachments attachments)
        {
            var map = new Dictionary<string, string>();
            for (var i = 1; i <= attachments.Count; i++)
            {
                if(i >= pointers.Count()) break;
                map.Add(attachments[i].FileName, pointers[i]);
            }
            return map;
        } 

        public static bool HasEmbeddedImages(Outlook.MailItem item)
        {
            var attachments = item.Attachments;
            if (attachments == null || attachments.Count == 0) return false;
            return (from Outlook.Attachment attachment in attachments
                    where attachment.Type == Outlook.OlAttachmentType.olByValue
                    select attachment.PropertyAccessor
                        into pa
                        select pa.GetProperty(ThisAddIn.DASL_ATTACH_CONTENT_ID))
                .Any(cid => (cid != null && !string.IsNullOrEmpty(cid)));
        }

        public static void ForwardEmbeddedECS(MessageItem msg, string recips, Account account)
        {
            string pointerString;
            string serverName;
            string serverPort;
            string encryptKey2;
            string userAgent;
            GetChiaraHeaders(msg, out pointerString, out serverName, out serverPort, out encryptKey2, out userAgent);
            var sender = msg.Sender.SMTPAddress;
            var config = account.Configurations.Values.
                First(cfg => cfg.Server.Equals(serverName,
                    StringComparison.CurrentCultureIgnoreCase));
            if (string.IsNullOrEmpty(sender))
            {
                Logger.Warning("ForwardEmbeddedECS", string.Format(
                    "failed to retrieve sender for {0}, skipping call to AddRecipients",
                    msg.Subject));
                return;
            }

            if (string.IsNullOrEmpty(pointerString))
            {
                Logger.Warning("ForwardEmbeddedECS", string.Format(
                    "failed to retrieve pointer(s) for {0}, skipping call to AddRecipients",
                    msg.Subject));
                return;
            }
            var pointers = pointerString.Split(new char[' ']);
            foreach (var pointer in pointers)
            {
                string error;
                ContentHandler.AddRecipients(account.SMTPAddress, config,
                    sender, pointer, serverName, serverPort, recips, out error);
            }
        }

        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Floor(bytes / Math.Pow(1024, place));
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        internal static Bitmap GetImage(long lngSpaceAvailable, double lngPercentageUsed)
        {
            const string SOURCE = CLASS_NAME + "GetImage";

            //first, create a dummy bitmap just to get a graphics object
            Bitmap img = new Bitmap(1, 1);
            try
            {
                Font font = new Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Bold);
                var color = Utils.GetColorByPercentage(lngPercentageUsed);
                var text = Utils.BytesToString(lngSpaceAvailable);

                Graphics drawing = Graphics.FromImage(img);

                //measure the string to see how big the image needs to be
                SizeF textSize = drawing.MeasureString(text, font);

                //free up the dummy image and old graphics object
                img.Dispose();
                drawing.Dispose();

                //create a new image of the right size
                img = new Bitmap((int)textSize.Width, (int)textSize.Width);

                drawing = Graphics.FromImage(img);

                //paint the background
                drawing.Clear(Color.White);

                //create a brush for the text
                Brush textBrush = new SolidBrush(color);

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Far;

                drawing.DrawString(text, font, textBrush, new PointF(textSize.Width / 2, textSize.Width / 2), sf);

                drawing.Save();

                textBrush.Dispose();
                drawing.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            
            return img;
        }

        public static Color GetColorByPercentage(double percentage)
        {
            // Calculate the percentage of storage remaining and set the corresponding color.
            if (percentage < .50)
            {
                return Color.Green;
            }
            else if (percentage >= .50 && percentage < .75)
            {
                return Color.Yellow;
            }
            else if (percentage >= .75 && percentage < .90)
            {
                return Color.Orange;
            }
            else if (percentage >= .90)
            {
                return Color.Red;
            }
            else
            {
                return Color.Black;
            }
        }

        /// <summary>
        /// to check if the given path is Image or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static bool IsFileImage(string path)
        {
            if (Path.GetExtension(path) == ".bmp" || Path.GetExtension(path) == ".jpg" || Path.GetExtension(path) == ".jpeg" || Path.GetExtension(path) == ".gif" || Path.GetExtension(path) == ".png")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// to update account storage value as how much space account is left
        /// </summary>
        /// <param name="account"></param>
        public static void UpdateAccountStorage(Account account)
        {
            string strResponseData = ContentHandler.GetDataResponse(account.SMTPAddress, account.Configurations[0].Password, account.Configurations[0].Server, account.Configurations[0].Port);
            if (strResponseData.StartsWith("6 "))
            {
                account.Storage = strResponseData.Substring(strResponseData.IndexOf("= ") + 2);
            }
        }

        #region Private methods

        private static string ConnectedAddIns(Outlook._Application app)
        {
            var sbInfo = new StringBuilder();
            var colAddIns = app.COMAddIns;
            foreach (var addin in colAddIns.Cast<COMAddIn>().Where(addin => addin.Connect))
            {
                sbInfo.Append("\t" + addin.Description + Environment.NewLine);
            }

            //colAddIns = null;
            return sbInfo.ToString();
        }

        private static Dictionary<string, string> ParseHeaders(string headers)
        {
            const string SOURCE = CLASS_NAME + "ParseHeaders";
            var headerDict = new Dictionary<string, string>();
            try
            {
                var array = Regex.Split(headers, @"(\r\n(?!\s))");
                foreach (var t in array)
                {
                    if (t.Equals("\r\n")) continue;
                    var pos = t.IndexOf(':');
                    if (pos <= 0) continue;
                    var header = t.Substring(0, pos).Trim().ToLower();
                    //some headers may be duplicated?
                    if (headerDict.ContainsKey(header)) continue;
                    var value = t.Substring(pos + 1).Trim();
                    headerDict.Add(header, value);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return headerDict;

        }

        private static void GetEmbeddedFile(string pointer, string path, Account account,
            EcsConfiguration configuration, string senderAddress, string serverName, string serverPort, 
            string encryptKey, string encryptKey2, string userAgent)
        {
            const string SOURCE = CLASS_NAME + "GetEmbeddedFile";
            try
            {
                if (File.Exists(path)) return;
                string content;
                string error;
                ContentHandler.FetchContent(account.SMTPAddress, configuration, senderAddress,
                    pointer, serverName, serverPort, true, out content,
                    out error);
                if (string.IsNullOrEmpty(content))
                {
                    Logger.Warning(SOURCE, string.Format(
                        "failed to retrieve image {0} using pointer {1} from {2}: {3}",
                        Path.GetFileName(path), pointer, senderAddress, error));
                    return;
                }
                ContentHandler.SaveAttachment(content, encryptKey, encryptKey2, userAgent, path);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        #endregion
    }
}
