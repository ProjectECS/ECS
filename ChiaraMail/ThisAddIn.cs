using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ChiaraMail.Forms;
using ChiaraMail.FormRegions;
using ChiaraMail.Properties;
using ChiaraMail.Wrappers;
using Microsoft.Office.Interop.Outlook;
using Newtonsoft.Json;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Windows.Forms;
using System;
using System.IO;
using System.Text;
using Redemption;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using Exception = System.Exception;
using System.Diagnostics;

namespace ChiaraMail
{
    public partial class ThisAddIn
    {
        #region Instance members

        private const string CLASS_NAME = "ThisAddIn.";
        private NameSpace _session;
        private Inspectors _inspectors;
        private Explorers _explorers;
        private Dictionary<string, InspWrap> _inspWrappers;
        private Dictionary<string, ExplWrap> _explWrappers;
        private static Ribbon _ribbon;
        //private readonly Dictionary<int, string> _acctDictionary = new Dictionary<int, string>();
        //private readonly List<string> _registeredList = new List<string>();
        private static string _storageEntryId;
        private static readonly Dictionary<string, LicenseCheck> LicenseChecks = new Dictionary<string, LicenseCheck>();
        private static readonly Dictionary<string, RegistrationCheck> RegistrationChecks = new Dictionary<string, RegistrationCheck>(0); 
        private readonly Dictionary<string, string> _storeAddresses = new Dictionary<string, string>();
        private readonly Dictionary<string, SearchFolderWrap> _searchFolderWrappers = new Dictionary<string, SearchFolderWrap>();
        Outlook.Items _items;

        static ThisAddIn()
        {
            Accounts = new Dictionary<string, Account>();
            AccountProxies = new Dictionary<string, List<string>>();
            NoPreviewer = false;
        }

        #endregion

        #region Constants
        public const int PT_STRING8 = 0x001E;
        public const int PR_MAIL_HEADER = 0x007D001E;
        public const string PR_MAIL_HEADER_TAG =
            "http://schemas.microsoft.com/mapi/proptag/0x007D001E";
        public const string MAIL_HEADER_GUID =
            "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/";
        public const int PR_ATTACH_DATA_BIN = 0x37010102;
        public const int PR_ATTACH_SIZE = 0x0E200003;
        public const int PR_RECORD_KEY = 0x0FF90102;
        public const int PR_ATTACHMENT_HIDDEN = 0x7FFE000B;
        public const int PR_ATTACH_CONTENT_ID = 0x3712001E;
        public const string DASL_INTERNET_ACCOUNT_NAME =
            "http://schemas.microsoft.com/mapi/id/{00062008-0000-0000-C000-000000000046}/8580001E";
        public const string DASL_DELIVERED_TO =
            "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/delivered-to";
        public const string DASL_ATTACH_CONTENT_ID =
            "http://schemas.microsoft.com/mapi/proptag/0x3712001E";
        public const string PS_INTERNET_HEADERS = "{00020386-0000-0000-C000-000000000046}";
        public const string STORAGE_SUBJECT = "ChiaraMailSettings";
        public const string REGISTRATION_CONFIRMED = "RegistrationConfirmed";
        public const string PUBLIC_SERVER = "www.chiaramail.com";
        public const string UPDATE_SUBJECT = "ECS message updated";
        #endregion

        #region Enums
        public enum LicenseState
        {
            Unknown,
            Licensed,
            NotLicensed
        }

        public enum RegistrationState
        {
            Unknown,
            Registered,
            NotRegistered,
            BadCredentials,
            ServerError
        }
        #endregion

        #region Properties

        internal NameSpace Session
        {
            get { return _session ?? (_session = Application.Session); }
        }

        internal static int AppVersion { get; set; }

        internal bool Configured
        {
            get
            {
                if (Accounts == null || Accounts.Count.Equals(0)) return false;
                return Accounts.Values.Any(account =>
                    account.Configurations.Values.Any(config =>
                        !string.IsNullOrEmpty(config.Password)));
            }
        }

        public static bool IsMailAllowForwarding { get; set; }
        public static bool  IsCurrentItemChiaraMail { get; set; }
        public static string _pointerString { get; set; }

        internal string[] Pointers
        {
            get
            {
                if (string.IsNullOrEmpty(_pointerString))
                {
                    return null;
                }
                return _pointerString.Split(' ');
            }
        }

        internal static string PublicKey { get; set; }

        internal static Dictionary<string, Account> Accounts { get; set; }

        internal static Dictionary<string, List<string>> AccountProxies { get; set; } 

        internal Account ActiveAccount
        {
            get
            {
                var explorer = Application.ActiveExplorer();
                var folder = explorer.CurrentFolder;
                var storeAddress = GetStoreAddress(folder.StoreID);
                return GetAccount(storeAddress);
            }
        }

        internal static Account ValidAccount
        {
            get
            {
                //need to walk through
                if (Accounts.Count > 0)
                {
                    return Accounts.Values.DefaultIfEmpty(null).FirstOrDefault(account =>
                        !string.IsNullOrEmpty(account.Configurations[0].Password));
                }
                return null;
            }
        }

        internal static bool NoPreviewer { get; set; }

        internal static bool Initialized { get; set; }

        #endregion

        #region Structs
        public struct LicenseCheck
        {
            public LicenseState Licensed;
            public DateTime LastCheck;
        }

        public struct RegistrationCheck
        {
            public RegistrationState Registered;
            public DateTime LastCheck;
        }

        #endregion

        #region Startup

        private void ThisAddInStartup(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "Startup";
            try
            {
                Stopwatch swStartUpTime = new Stopwatch();
                swStartUpTime.Start();

                Logger.Init();
                var version = Application.Version;
                AppVersion = Convert.ToInt32(version.Split(new[] { '.' })[0]);

                Logger.Info(SOURCE, string.Format("{0}Assembly version: {1}{0}Install path: {2}{0}" +
                              "OS: {3} ({4}){0}Outlook version: {5} {6}{0}Profile: {9}{0}Framework version: {7}{0}IE version: {8}",
                              Environment.NewLine,
                              Assembly.GetExecutingAssembly().FullName,
                              Utils.GetRootPath(),
                              Environment.OSVersion,
                              Environment.Is64BitOperatingSystem ? "x64" : "x86",
                              version,
                              Environment.Is64BitProcess ? "64-bit" : "",
                              RuntimeEnvironment.GetSystemVersion(),
                              Utils.GetBrowserVersion(),
                              Session.CurrentProfileName));
                //read the stored settings first            
                var gotStored = GetStoredSettings();
                //add new mail handler
                Application.NewMailEx += ApplicationNewMailEx;                
                _inspWrappers = new Dictionary<string, InspWrap>();
                _explWrappers = new Dictionary<string, ExplWrap>();

                Inspectors = Application.Inspectors;
                #region Commented below code as it takes more time to make Outlook ready if we have more mails in selected folder (Inbox)
                Explorers = Application.Explorers;
                _items = Application.ActiveExplorer().CurrentFolder.Items;
                lstFolderIds.Add(Application.ActiveExplorer().CurrentFolder.EntryID);
                _items.ItemRemove += Items_ItemRemove;
                Application.ActiveExplorer().BeforeFolderSwitch += ThisAddIn_BeforeFolderSwitch;
                if (Explorers.Count > 0)
                {
                    //may not be an ActiveExplorer on restart after a crash
                    Explorer active;
                    try
                    {
                        active = Application.ActiveExplorer();
                    }
                    catch
                    {
                        active = null;
                    }
                    if (active == null)
                    {
                        Logger.Info(SOURCE, "no ActiveExplorer");
                    }
                    else
                    {
                        AddWrapper(Application.ActiveExplorer());
                    }
                }
                #endregion
                //finish initialization in background, so we can exit startup immediately
                ThreadPool.QueueUserWorkItem(InitHandler, gotStored);

                ////load the current accounts
                //ValidateAccountsRdo();
                //Logger.Verbose(SOURCE, string.Format(
                //    "found {0} accounts", Accounts.Count));
                ////read the stored settings            
                //GetStoredSettings();
                //Logger.Verbose(SOURCE, string.Format(
                //    "active accounts: {0}",
                //    ListCurrentAccounts()));
                //EvalAttachmentPreview();
                ////CheckRegistrations();

                Logger.Info(SOURCE, string.Format("Takes {0} seconds", swStartUpTime.Elapsed.TotalSeconds));
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        List<string> lstFolderIds = new List<string>();

        void ThisAddIn_BeforeFolderSwitch(object NewFolder, ref bool Cancel)
        {
            var folder = NewFolder as Folder;

            if (lstFolderIds.Contains(folder.EntryID)) return;

            lstFolderIds.Add(folder.EntryID);
            _items = folder.Items;
            _items.ItemRemove += Items_ItemRemove;
        }

        void Items_ItemRemove()
        {
            const string SOURCE = CLASS_NAME + "Items_ItemRemove";

            try
            {
                //if current selected mail is ChiaraMail
                if (IsCurrentItemChiaraMail)
                {
                    //prompt for confirmation
                    if (MessageBox.Show(Resources.prompt_delete_content_confirm_outlook,
                           Resources.product_name,
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (var pointer in Pointers)
                        {
                            string error;
                            ContentHandler.DeleteContent(ActiveAccount.SMTPAddress,
                                                            ActiveAccount.Configurations[0], pointer, out error, false);
                            
                            if (error.Equals("success")) continue;
                            MessageBox.Show(string.Format(
                                Resources.error_deleting_content,
                                Environment.NewLine, error),
                                            Resources.product_name,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }
        #endregion

        #region Shutdown

        private void ThisAddInShutdown(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "Shutdown";
            try
            {
                //clean up wrappers
                if (_inspWrappers != null)
                {
                    Logger.Verbose(SOURCE, string.Format(
                        "releasing {0} inspector wrappers", _inspWrappers.Count));
                    var keys = new string[_inspWrappers.Count];
                    _inspWrappers.Keys.CopyTo(keys, 0);
                    foreach (var key in keys)
                        _inspWrappers.Remove(key);
                }

                if (_explWrappers != null)
                {
                    Logger.Verbose(SOURCE, string.Format(
                        "releasing {0} explorer wrappers", _explWrappers.Count));
                    var keys = new string[_explWrappers.Count];
                    _explWrappers.Keys.CopyTo(keys, 0);
                    foreach (var key in keys)
                        _explWrappers.Remove(key);
                }

                if (_searchFolderWrappers != null && _searchFolderWrappers.Count > 0)
                {
                    var keys = new string[_searchFolderWrappers.Count];
                    _searchFolderWrappers.Keys.CopyTo(keys, 0);
                    foreach (var key in keys)
                        _searchFolderWrappers.Remove(key);
                    {

                    }
                }

                //release Inspectors & Explorers which will release handlers
                Inspectors = null;
                Explorers = null;
                //clean the temp directory
                Logger.Verbose(SOURCE, "cleaning temp folder");
                Utils.CleanTempFolder("");
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        #endregion

        #region Outlook objects and events

        private Explorers Explorers
        {
            get { return _explorers; }
            set
            {
                if (_explorers == value) return;
                if (value != null)
                {
                    //assign
                    _explorers = value;
                    //add handler
                    _explorers.NewExplorer += ExplorersNewExplorer;
                }
                else
                {
                    //release handler
                    _explorers.NewExplorer -= ExplorersNewExplorer;
                    //release reference
                    Marshal.FinalReleaseComObject(_explorers);
                    _explorers = null;
                }
            }
        }

        private void ExplorersNewExplorer(Explorer explorer)
        {
            //add to list
            AddWrapper(explorer);
        }

        private Inspectors Inspectors
        {
            set
            {
                if (_inspectors == value) return;
                if (value != null)
                {
                    //assign collection
                    _inspectors = value;
                    //add handler
                    _inspectors.NewInspector += InspectorsNewInspector;
                }
                else
                {
                    //release handler
                    _inspectors.NewInspector -= InspectorsNewInspector;
                    Marshal.FinalReleaseComObject(_inspectors);
                    _inspectors = null;
                }
            }
        }

        private void InspectorsNewInspector(Inspector inspector)
        {
            //add to list
            AddWrapper(inspector);
        }

        private void ApplicationNewMailEx(string entryIdCollection)
        {
            const string SOURCE = CLASS_NAME + "ApplicationNewMail";
            try
            {                
                var ids = entryIdCollection.Split(new[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
                Logger.Info(SOURCE, "fired with " + ids.Count());
                foreach (var id in ids)
                {
                    MailItem mailItem;
                    try
                    {
                        mailItem = Session.GetItemFromID(id);
                    }
                    catch
                    {
                        mailItem = null;
                    }
                    if (mailItem == null)
                    {
                        Logger.Info(SOURCE,"failed to retrieve new message");
                        continue;
                    }
                    //intercept Message Updated
                    if (IsUpdateMessage(mailItem)) return;
                    //check for our headers
                    if (mailItem.MessageClass == Resources.message_class_CM || !Utils.HasChiaraHeader(mailItem))
                        continue;
                    mailItem.MessageClass = Resources.message_class_CM;
                    mailItem.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }
        #endregion

        #region Shared Methods

        internal string GetStoreAddress(string storeId)
        {
            return !_storeAddresses.ContainsKey(storeId)
                ? string.Empty
                : _storeAddresses[storeId];
        }

        internal void AddWrapper(Explorer explorer)
        {
            var wrapper = new ExplWrap(explorer);
            //don't know how this could happen, but just in case
            if (_explWrappers.ContainsKey(wrapper.Key))
            {
                _explWrappers.Remove(wrapper.Key);
            }
            _explWrappers.Add(wrapper.Key, wrapper);
        }

        internal void AddWrapper(Inspector inspector)
        {
            const string SOURCE = CLASS_NAME + "AddWrapper";
            dynamic currentItem = null;
            try
            {
                currentItem = inspector.CurrentItem;
                if (!(currentItem is MailItem)) return;
                if (currentItem.Sent) return;
                Logger.Verbose(SOURCE, "creating new wrapper");
                var wrapper = new InspWrap(inspector);
                //don't know how this could happen, but just in case
                if (_inspWrappers.ContainsKey(wrapper.Key))
                {
                    _inspWrappers.Remove(wrapper.Key);
                }
                //is this a forward or reply?
                var item = (MailItem)currentItem;
                if (!item.Sent && !string.IsNullOrEmpty(item.ConversationIndex) &&
                    item.ConversationIndex.Length > 44)
                {
                    //find the parent
                    var parent = FindParentByConversation(item);
                    if (parent == null)
                    {
                        Logger.Error(SOURCE, string.Format(
                            "failed to retrieve parent message for reply/forward {0}; unable to check for ECS-enabled content",
                            item.Subject));
                    }
                    else
                    {
                        Logger.Verbose(SOURCE, string.Format(
                            "found parent {0} for reply/forward",
                            parent.Subject));
                        if (Utils.HasChiaraHeader(parent))
                            HandleReplyForward(parent, item, ref wrapper);
                    }
                }
                Logger.Verbose(SOURCE, "adding wrapper to collection with key " + wrapper.Key);
                _inspWrappers.Add(wrapper.Key, wrapper);
                ResetInspButtons();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                if (currentItem != null)
                {
                    Marshal.ReleaseComObject(currentItem);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
        }

        internal void ReleaseInspWrap(string key)
        {
            if (!_inspWrappers.ContainsKey(key)) return;
            Logger.Verbose("ReleaseInspWrap", "releasing wrapper with key " + key);
            _inspWrappers.Remove(key);

        }

        internal void ReleaseExplWrap(string key)
        {
            if (!_explWrappers.ContainsKey(key)) return;
            _explWrappers.Remove(key);
        }

        internal void ReleaseSearchFolderWrap(string key)
        {
            if (!_searchFolderWrappers.ContainsKey(key)) return;
            Logger.Verbose("ReleaseSearchFolderWrap", "releasing wrapper with key " + key);
            _searchFolderWrappers.Remove(key);
        }

        internal InspWrap GetInspWrap(string key)
        {
            if (_inspWrappers != null &&
                _inspWrappers.ContainsKey(key))
                return _inspWrappers[key];
            return null;
        }

        internal InspWrap GetInspWrap(Inspector insp)
        {
            return _inspWrappers == null
                ? null
                : _inspWrappers.Values.DefaultIfEmpty(null).FirstOrDefault(wrap => wrap.Inspector == insp);
        }

        internal ExplWrap GetActiveExplWrap()
        {
            
            if (_explWrappers != null)
            {
                var expl = Application.ActiveExplorer();
                return _explWrappers.Values.FirstOrDefault(wrapper => wrapper.Explorer == expl);
            }
            return null;
        }
        internal static bool AccountHasEnabledConfiguration(string smtpAddress)
        {
            var account = FindMatchingAccount(smtpAddress);
            if (account == null) return false;
            return account.Configurations.Keys
                .Select(key => account.Configurations[key])
                .Any(config =>!string.IsNullOrEmpty(config.Password));
        }

        internal static EcsConfiguration GetMatchingConfiguration(string smtpAddress, string server, 
            string port, bool create)
        {
            EcsConfiguration config = null;
            //if account was just created it won't be in the list
            var account = FindMatchingAccount(smtpAddress);
            if (account != null)
            {
                config = account.Configurations.Keys
                .Select(key => account.Configurations[key])
                .FirstOrDefault(c => 
                    c.Server.Equals(server, StringComparison.CurrentCultureIgnoreCase) 
                    && c.Port == port);
            }
            // even for an unknown account there is always a default configuration (for the public content server)
            // it just won't have a password
            if (config == null && create &&
                server.Equals(PUBLIC_SERVER, StringComparison.CurrentCultureIgnoreCase) &&
                port == Resources.default_port)
            {                 
                //if we have the account address try to get the password
                config = new EcsConfiguration
                {
                    Server = PUBLIC_SERVER,
                    Port = Resources.default_port
                };
                if (account != null)
                {
                    config.Password = FetchPasswordPublicServer(account.SMTPAddress,
                        account.UserName,account.Protocol,
                        account.Host,account.Port, account.LoginName);
                    account.Configurations.Add(0,config);
                }
            }
            return config;
        }

        internal static bool IsAccountConfigured(string smtpAddress, string server, string port, bool create)
        {
            var config = GetMatchingConfiguration(smtpAddress, server, port, create);
            return config != null && !string.IsNullOrEmpty(config.Password);
        }

        internal bool AccountDefaultState(string smtpAddress)
        {
            var account = FindMatchingAccount(smtpAddress);
            if (account == null) return false;
            return account.Configurations[account.DefaultConfiguration].DefaultOn;
        }

        internal bool AccountDefaultEncrypt(string smtpAddress)
        {
            var account = FindMatchingAccount(smtpAddress);
            if (account == null) return false;
            return account.Configurations[account.DefaultConfiguration].Encrypt;
        }

        internal static bool IsEditable(MailItem mailItem, string senderAddress, string server, string port, string storeAddress)
        {
            //for an item to be editable it must have already been sent,
            //have the headers/custom props,
            //the sender address must match the address of the store owner
            //and there must be a configured account linked with that address
            if (string.IsNullOrEmpty(senderAddress)) return false;
            if (!IsAccountConfigured(senderAddress, server, port, false)) return false;
            //check proxies
            return mailItem.Sent && mailItem.MessageClass.Contains("ChiaraMail") &&
                    !string.IsNullOrEmpty(storeAddress) &&
                    ((AccountProxies.ContainsKey(storeAddress) && 
                    AccountProxies[storeAddress].Contains(senderAddress)) ||
                    (AccountProxies.ContainsKey(senderAddress) && 
                    AccountProxies[senderAddress].Contains(storeAddress)));
        }

        internal static Account GetAccount(string smtpAddress, string server = PUBLIC_SERVER)
        {
            var account = FindMatchingAccount(smtpAddress);
            if (account == null) return null;
            return account.Configurations.Values.Any(config =>
                    config.Server.Equals(server, StringComparison.CurrentCultureIgnoreCase)
                    && !string.IsNullOrEmpty(config.Password)) ? account : null;
        }

        internal static Account GetAccount(Recipients recips, string server)
        {
            const string SOURCE = CLASS_NAME + "GetAccount";
            try
            {
                //find the first recip address that is associated with a configured account
                foreach (Recipient recip in recips)
                {
                    var smtpAddress = recip.Address;
                    var ae = recip.AddressEntry;
                    if (ae.AddressEntryUserType == OlAddressEntryUserType.olExchangeUserAddressEntry)
                    {
                        var exUser = ae.GetExchangeUser();
                        smtpAddress = exUser.PrimarySmtpAddress;
                    }
                    var account = FindMatchingAccount(smtpAddress);
                    if(account == null) continue;
                    if (account.Configurations.Values.Any(config =>
                        config.Server.Equals(server, StringComparison.CurrentCultureIgnoreCase)
                        && !string.IsNullOrEmpty(config.Password)))
                    {
                        return account;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return null;
        }

        internal static void ResetInspButtons()
        {
            _ribbon.ResetInspButtons();
        }

        internal void ShowConfig()
        {
            //refresh the accounts
            ValidateAccounts();
            //do we have any accounts?
            var count = Accounts.Count;
            var form = new ConfigurationForm();
            //get the launching window
            var active = Application.ActiveExplorer();
            var win = new OutlookWin32Window(active, false);
            if (form.ShowDialog(win) != DialogResult.OK) return;
            Logger.Verbose("ShowConfig", string.Format(
                "active accounts: {0}",
                ListCurrentAccounts()));
            //write changes back to storage
            StoreSettings();
            //prompt for invite if we just added the first accounts
            if (count.Equals(0) && Accounts.Count > 0)
            {
                if (MessageBox.Show(Resources.invite_prompt,
                    Resources.product_name,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                    CreateInvite();
            }
        }

        internal void CreateInvite()
        {
            var addressBook = Session.GetSelectNamesDialog();
            addressBook.ForceResolution = true;
            addressBook.AllowMultipleSelection = true;
            addressBook.Caption = Resources.screentip_invite;
            if (!addressBook.Display()) return;
            var recips = addressBook.Recipients;
            var invalid = 0;
            var registered = 0;
            //collect the email addresses
            var selections = new Dictionary<string, Recipient>();
            foreach (Recipient recip in recips)
            {
                switch (recip.DisplayType)
                {
                    case OlDisplayType.olUser:
                        if (!EvalRecip(recip, ref selections)) invalid++;
                        break;
                    case OlDisplayType.olDistList:
                    case OlDisplayType.olPrivateDistList:
                        //case Outlook.OlDisplayType.olAgent: //might be query-dl
                        //expand the DL and evaluate each address
                        invalid += ExpandDl(recip, ref selections);
                        break;
                    default:
                        invalid++;
                        break;
                }
            }
            //if there's more than one account we need the active account

            //get comma-delimited list from dictionary
            var addresses = string.Join(",", selections.Keys);
            //pop up wait dialog
            var waitForm = new WaitForm
                               {
                                   Account = ActiveAccount,
                                   Configuration = ActiveAccount.Configurations[0],
                                   Recips = addresses,
                                   CheckRegistration = true
                               };
            if (waitForm.ShowDialog() != DialogResult.OK) return;
            var result = waitForm.RegistrationResponse;
            //strip out the registered users
            var regList = new List<string>();
            if (!string.IsNullOrEmpty(result))
            {
                var responses = result.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var keys = addresses.Split(new[] { ',' });
                for (var i = 0; i < responses.Length; i++)
                {
                    if (!Convert.ToBoolean(responses[i])) continue;
                    var key = keys[i];
                    regList.Add(selections[key].Name);
                    selections.Remove(key);
                    registered++;
                }
            }
            Logger.Info("CreateInvite", string.Format(
                "user made {0} selections, {1} were invalid or duplicates and {2} were already registered",
                recips.Count, invalid, registered));
            if (regList.Count > 0)
            {
                if (selections.Count > 0)
                {
                    MessageBox.Show(string.Format(
                        Resources.invite_registered_alert + string.Join("{0}{1}", regList),
                        Environment.NewLine, "\t"),
                        Resources.invite_alert_caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        Resources.invite_all_registered,
                        Resources.invite_alert_caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            if (selections.Count == 0) return;
            MailItem mail = Application.CreateItem(OlItemType.olMailItem);
            var mailRecips = mail.Recipients;
            foreach (Recipient selection in selections.Values)
            {
                var recip = mailRecips.Add(selection.Address);
                recip.Type = selection.Type;
                recip.Resolve();
            }
            mail.Subject = Resources.invite_subject;
            mail.Body = string.Format(Resources.invite_text,
                Environment.NewLine,
                Session.CurrentUser.Name);
            mail.Display();
        }

        internal void RequestSupport()
        {
            try
            {
                var message = Application.CreateItem(OlItemType.olMailItem);
                //create tech support email
                message.To = "support@chiaramail.com";
                message.Subject = "ChiaraMail for Outlook Support Request";

                message.Body = string.Format("{0}\r\n\r\n\r\n\r\n\r\n\r\n\r\n{1}",
                    "How can we help you?", Utils.DiagnosticInfo);
                //create a temporary folder where we can zip the logs
                var tempName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                tempName = tempName.Substring(0,
                    tempName.LastIndexOf(".", StringComparison.InvariantCultureIgnoreCase) - 1);
                Directory.CreateDirectory(tempName);
                var zipPath = Path.Combine(tempName, "Logs.zip");
                //delete the file if it already exists
                if (File.Exists(zipPath)) File.Delete(zipPath);
                //zip and attach logs
                Utils.ZipDirectory(Logger.LogsPath, zipPath, 9, true);
                //add zipped logs as attachment
                if (zipPath.Length > 0 & File.Exists(zipPath))
                {
                    var attachments = message.Attachments;
                    attachments.Add(zipPath);
                    File.Delete(zipPath);
                }
                //delete temp folder and zip
                Directory.Delete(tempName, true);
                message.Display(false);
            }
            catch (Exception ex)
            {
                Logger.Error("RequestSupport","error generating support request: " + ex);
            }

        }

        internal static void RelayContentChange(object sourceRegion, string recordKey, string content)
        {
            const string SOURCE = CLASS_NAME + "RelayContentChange";
            try
            {
                foreach (var item in Globals.FormRegions)
                {
                    if (item != sourceRegion)
                    {
                        if (item.GetType() == typeof(DynamicInspector))
                        {
                            var region = (DynamicInspector)item;
                            if (region.RecordKey.Equals(recordKey))
                            {
                                region.RefreshContent(content);
                            }
                        }
                        else
                        {
                            var region = (DynamicReadingPane)item;
                            if (region.RecordKey.Equals(recordKey))
                            {
                                region.RefreshContent(content);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal static RegistrationState CheckSenderRegistration(string sender,Account acct, out string error)
        {
            const string SOURCE = "CheckSenderRegistration";
            var registered = false;
            var hasExpired = true;
            RegistrationCheck check;
            if (RegistrationChecks.TryGetValue(sender, out check))
            {
                hasExpired = (check.LastCheck.Date != DateTime.Today.Date);
                registered = check.Registered == RegistrationState.Registered;
            }
            if (registered || !hasExpired)
            {
                error = string.Empty;
                return check.Registered;
            }
            //if (!registered && hasExpired)

            Logger.Info(SOURCE, string.Format(
                "checking registration for {0}", sender));
            //var acct = ValidAccount;
            if (acct == null)
            {
                Logger.Info(SOURCE, string.Format(
                    "no valid accounts, unable to submit 'CheckRegistered' request for {0}",
                    sender));
                error = "no valid accounts";
                return RegistrationState.Unknown;
            }
            //default is NotRegistered
            check.Registered = RegistrationState.NotRegistered;
            check.Registered = ContentHandler.CheckRegistered(acct.SMTPAddress, acct.Configurations[0], sender, out error);
            //if (string.IsNullOrEmpty(response))
            //{
            //    check.Registered = RegistrationState.Unknown;
            //}
            //else
            //{
            //    bool answer;
            //    if (bool.TryParse(response, out answer))
            //    {
            //        check.Registered = answer
            //            ? RegistrationState.Registered 
            //            : RegistrationState.NotRegistered;
            //    }
            //    else if (response == "invalid credentials")
            //    {
            //        check.Registered = RegistrationState.BadCredentials;
            //    }
            //    else if (response == )
            //    {
            //        check.Registered = RegistrationState.ServerError;
            //    }
            //}
            //registered = check.Registered == RegistrationState.Registered;
            //store the result
            check.LastCheck = DateTime.Now;
            if (RegistrationChecks.ContainsKey(sender))
            {
                RegistrationChecks[sender] = check;
            }
            else
            {
                RegistrationChecks.Add(sender, check);
            }
            return check.Registered;
        }

        internal static bool IsServerLicensed(string server, string port)
        {
            if (server.Equals(PUBLIC_SERVER, StringComparison.CurrentCultureIgnoreCase)) return true;
            var registered = false;
            var hasExpired = true;
            LicenseCheck check;
            var url = string.Format("https://{0}:{1}",
                                    server, port);
            if (LicenseChecks.TryGetValue(url, out check))
            {
                hasExpired = (check.LastCheck.Date != DateTime.Today.Date);
                registered = check.Licensed == LicenseState.Licensed;
            }
            if (!registered && hasExpired)
            {
                Logger.Info("IsServerLicensed", string.Format(
                    "checking registration for {0}", url));
                var acct = ValidAccount;
                var licensed = ContentHandler.CheckLicensed(acct.SMTPAddress, acct.Configurations[0], url);
                registered = licensed == LicenseState.Licensed;
                //store the result
                check.Licensed = licensed;
                check.LastCheck = DateTime.Now;
                if (LicenseChecks.ContainsKey(url))
                {
                    LicenseChecks[url] = check;
                }
                else
                {
                    LicenseChecks.Add(url, check);
                }
            }
            return registered;
        }

        internal static Account FindMatchingAccount(string smtpAddress)
        {
            if (Accounts.ContainsKey(smtpAddress)) return Accounts[smtpAddress];
            //could also match on proxy address
            return AccountProxies.Keys
                .Where(proxy => AccountProxies[proxy]
                    .Contains(smtpAddress) &&
                    Accounts.ContainsKey(proxy))
                    .Select(proxy => Accounts[proxy]).FirstOrDefault();
        }
        #endregion

        #region Private methods

        private void InitHandler(object arg)
        {
            Stopwatch swInitHandler = new Stopwatch();
            swInitHandler.Start();

            const string SOURCE = CLASS_NAME + "InitHandler";
            //read the stored settings first            
            var gotStored = arg is bool && (bool) arg;
            //load the current accounts
            ValidateAccountsRdo();
            Initialized = true;
            Logger.Verbose(SOURCE, string.Format(
                "found {0} accounts", Accounts.Count));
            //store settings if reading was incomplete            
            if (!gotStored)
            {               
                StoreSettings();
            }
            Logger.Verbose(SOURCE, string.Format(
                "active accounts: {0}",
                ListCurrentAccounts()));
            EvalAttachmentPreview();
            var readingPane = new DynamicReadingPane.DynamicReadingPaneFactory();

            Logger.Info(SOURCE, string.Format("Takes {0} seconds", swInitHandler.Elapsed.TotalSeconds));
        }

        private static bool EvalRecip(Recipient recip, ref Dictionary<string, Recipient> selections)
        {
            try
            {
                var ae = recip.AddressEntry;
                if (ae == null) return false;
                var smtp = ae.GetPrimarySMTP();
                if (string.IsNullOrEmpty(smtp) || selections.ContainsKey(smtp)) return false;
                selections.Add(smtp, recip);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Verbose("", ex.Message);
            }
            return false;
        }

        private static int ExpandDl(Recipient dl, ref Dictionary<string, Recipient> selections)
        {
            var invalid = 0;
            Outlook.AddressEntry ae = null;
            try
            {
                ae = dl.AddressEntry;
            }
            catch (Exception ex)
            {
                Logger.Warning("ExpandDl", string.Format("failed to retrieve AddressEntry for {0}: {1}",
                    dl.Name, ex.Message));
            }
            if (ae == null) return 1;
            if (ae.Members == null || ae.Members.Count == 0) return 0;
            foreach (Outlook.AddressEntry member in ae.Members)
            {
                switch (member.AddressEntryUserType)
                {
                    case OlAddressEntryUserType.olExchangeOrganizationAddressEntry:
                    case OlAddressEntryUserType.olExchangePublicFolderAddressEntry:
                    case OlAddressEntryUserType.olExchangeRemoteUserAddressEntry:
                    case OlAddressEntryUserType.olOtherAddressEntry:
                    case OlAddressEntryUserType.olLdapAddressEntry:
                    case OlAddressEntryUserType.olExchangeAgentAddressEntry: //might be query-dl
                        invalid++;
                        break;
                    case OlAddressEntryUserType.olExchangeUserAddressEntry:
                    case OlAddressEntryUserType.olSmtpAddressEntry:
                    case OlAddressEntryUserType.olOutlookContactAddressEntry:
                        var smtp = member.GetPrimarySMTP();
                        if (string.IsNullOrEmpty(smtp))
                        {
                            invalid++;
                        }
                        else if (!selections.ContainsKey(smtp))
                        {
                            var recip = GenerateRecip(smtp, dl.Type, dl.Session);
                            if (recip != null)
                                selections.Add(smtp, recip);
                        }
                        break;
                    case OlAddressEntryUserType.olExchangeDistributionListAddressEntry:
                    case OlAddressEntryUserType.olOutlookDistributionListAddressEntry:

                        var memberRecip = GenerateRecip(member.Address, dl.Type, dl.Session);
                        if (memberRecip != null)
                            invalid += ExpandDl(memberRecip, ref selections);
                        break;
                }
            }
            return invalid;
        }

        private static Recipient GenerateRecip(string smtp, int type, NameSpace session)
        {
            var recip = session.CreateRecipient(smtp);
            recip.Type = type;
            recip.Resolve();
            return recip;
        }

        private void GetAccountInfoRDO(Outlook.Account olAccount, ref Account account)
        {
            var rdoSession = RedemptionLoader.new_RDOSession();
            rdoSession.MAPIOBJECT = Session.MAPIOBJECT;
            try
            {
                var olStore = olAccount.DeliveryStore;
                if (olStore == null) return;
                RDOStore rdoStore = rdoSession.GetRDOObjectFromOutlookObject(olStore, true);
                if (rdoStore == null) return;
                var acct = rdoStore.StoreAccount;
                if(acct == null) return;
                switch (acct.AccountType)
                {
                    case rdoAccountType.atExchange:
                        var exAcct = acct as RDOExchangeAccount;
                        if (exAcct == null) return;
                        var user = exAcct.User;
                        //add all SMTP proxies
                        List<string> proxies = null;
                        try
                        {
                            var prop = user.Fields[(int)MAPITags.PR_EMS_AB_PROXY_ADDRESSES];
                            if (prop != null)
                            {
                                var addresses = prop as object[];
                                if (addresses != null)
                                {
                                    proxies = addresses.Select(Convert.ToString).ToList();
                                }
                            }
                        }
                        catch
                        {
                            proxies = null;
                        }
                        List<string> smtpProxies = null;
                        if (proxies != null)
                        {
                            smtpProxies = (from proxy in proxies 
                                               where proxy.StartsWith("SMTP:", StringComparison.InvariantCultureIgnoreCase) 
                                               select proxy.Replace("SMTP:", "").Replace("smtp:", "")).ToList();
                        }
                        AddAccountProxies(user.SMTPAddress,smtpProxies);
                        break;
                    case rdoAccountType.atIMAP:
                        var imap = (RDOIMAPAccount) acct;
                        //account.Password = imap.IMAP_Password;
                        account.Host = imap.IMAP_Server;
                        account.Port = Convert.ToString(imap.IMAP_Port);
                        account.Protocol = "IMAP";
                        account.LoginName = imap.IMAP_UserName;                       
                        break;
                    case rdoAccountType.atPOP3:
                        var pop3 = (RDOPOP3Account) acct;
                        //account.Password = pop3.POP3_Password;
                        account.Host = pop3.POP3_Server;
                        account.Port = Convert.ToString(pop3.POP3_Port);
                        account.Protocol = "POP3";
                        account.LoginName = pop3.POP3_UserName;
                        break;
                    case rdoAccountType.atEAS:
                        var eas = (RDOEASAccount) acct;
                        account.Host = eas.Server;
                        account.Port = "";
                        account.Protocol = "EAS";
                        //account.Password = eas.Password;
                        account.UserName = eas.UserName;
                        break;
                    case rdoAccountType.atHTTP:
                        var http = (RDOHTTPAccount) acct;
                        account.Host = http.Server;
                        account.Port = "";
                        account.Protocol = "HTTP";
                        account.UserName = http.UserName;
                        break;
                    default:
                        return;
                }
            }
            catch(Exception ex)
            {
                Logger.Error("GetAccountInfoRDO",string.Format(
                    "error for olAccount {0}: {1}",
                    olAccount.DisplayName,
                    ex));
            }
        }

        private void ValidateAccounts()
        {
            const string SOURCE = CLASS_NAME + "ValidateAccounts";
            try
            {
                //Session.Accounts doesn't update immediately when you add or remove an account
                //try RDOSession stores if available
                if (ValidateAccountsRdo()) return;
                var accounts = Session.Accounts;
                Logger.Info(SOURCE, string.Format(
                    "comparing {0} OL accounts against {1} ECS accounts",
                    accounts.Count, Accounts.Count));
                //check for deletes first
                var keys = Accounts.Keys;
                foreach (var key in keys
                    .TakeWhile(key => accounts.Cast<Outlook.Account>()
                        .All(olAcct => olAcct.SmtpAddress != key)))
                {
                    Logger.Info(SOURCE, string.Format(
                        "removing account with key {0}", key));
                    Accounts.Remove(key);
                }
                //now check for new OL accounts
                foreach (Outlook.Account olAcct in accounts)
                {
                    if (Accounts.ContainsKey(olAcct.SmtpAddress)) continue;
                    var userName = olAcct.UserName;
                    //OL 2K7 doesn't expose CurrentUser
                    if (AppVersion > 12)
                        userName = olAcct.CurrentUser.Name;
                    var account = new Account
                    {
                        UserName = userName,
                        SMTPAddress = olAcct.SmtpAddress,
                        DefaultConfiguration = 0,
                        Configurations = new Dictionary<int, EcsConfiguration>
                                {
                                    {
                                        0, new EcsConfiguration
                                            {
                                                Key = 0,
                                                Description = Resources.config_public_server_description,
                                                Password = string.Empty,
                                                Server = PUBLIC_SERVER,
                                                Port = Resources.default_port,
                                                DefaultOn = false,
                                                Encrypt = false,
                                                NoPlaceholder = false,
                                                AllowForwarding = false
                                            }
                                    }
                                }
                    };
                    
                    GetAccountInfoRDO(olAcct,ref account);
                    Accounts.Add(account.SMTPAddress, account);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private bool ValidateAccountsRdo()
        {
            const string SOURCE = CLASS_NAME + "ValidateAccountsRdo";
            try
            {
                //Session.Accounts doesn't update immediately when you add or remove an account
                //try RDOSession stores if available
                var session = RedemptionLoader.new_RDOSession();
                session.Logon("","",false,false,0,true);
                var activeAddresses = new List<string>();
                Logger.Info(SOURCE,"storeAddress count:" + _storeAddresses.Count);
                //_storeAddresses.Clear();
                foreach (RDOAccount acct in session.Accounts)
                {
                    Logger.Info(SOURCE,string.Format(
                        "checking acct {0}, type is {1}",
                        acct.Name, acct.AccountTypeStr));
                    string smtp;
                    string storeEntryId;
                    switch (acct.AccountType)
                    {
                        case rdoAccountType.atIMAP:
                            var imap = (RDOIMAPAccount)acct;
                            smtp = imap.SMTPAddress;
                            if (string.IsNullOrEmpty(smtp))
                            {
                                Logger.Info(SOURCE, "no SMTP address for " + acct.Name);
                                continue;
                            }
                            var store = imap.Store;
                            storeEntryId = store.EntryID;
                            if (!activeAddresses.Contains(smtp))
                                activeAddresses.Add(smtp);
                            //we don't have this account
                            AddAccountProxies(smtp);
                            if (Accounts.ContainsKey(smtp)) break;
                            Logger.Info(SOURCE, string.Format("adding account for {0}", smtp));
                            Accounts.Add(smtp, new Account
                                {
                                    UserName = imap.UserName,
                                    SMTPAddress = smtp,
                                    DefaultConfiguration = 0,
                                    Host = imap.IMAP_Server,
                                    Port = Convert.ToString(imap.IMAP_Port),
                                    Protocol = "IMAP",
                                    LoginName = imap.IMAP_UserName,
                                    Configurations = new Dictionary<int, EcsConfiguration>
                                        {
                                            {
                                                0, new EcsConfiguration
                                                    {
                                                        Key = 0,
                                                        Description = Resources.config_public_server_description,
                                                        Password =
                                                            FetchPasswordPublicServer(smtp, imap.UserName, "IMAP",
                                                                                      imap.IMAP_Server,
                                                                                      Convert.ToString(imap.IMAP_Port),
                                                                                      imap.IMAP_UserName),
                                                        Server = PUBLIC_SERVER,
                                                        Port = Resources.default_port,
                                                        DefaultOn = false,
                                                        Encrypt = false,
                                                        NoPlaceholder = false,
                                                        AllowForwarding = false
                                                    }
                                            }
                                        }
                                });                            
                            break;
                        case rdoAccountType.atExchange:
                            var ex = (RDOExchangeAccount)acct;
                            var primary = ex.PrimaryStore;
                            if (primary.StoreKind != TxStoreKind.skPrimaryExchangeMailbox) continue;
                            var root = primary.IPMRootFolder;
                            storeEntryId = root != null 
                                ? root.StoreID//Convert.ToString(longTermEntryId)
                                : primary.EntryID;
                            //Logger.Info(SOURCE,string.Format("EX account {0}, PrimaryStore {1}, StoreEntryID {2}",
                            //    ex.Name, primary.Name, string.IsNullOrEmpty(storeEntryId) ? 0 : storeEntryId.Length));
                            var user = ex.User;
                            smtp = user.SMTPAddress;
                            if (string.IsNullOrEmpty(smtp))
                            {
                                Logger.Info(SOURCE, "no SMTP address for " + acct.Name);
                                continue;
                            }
                            if (!activeAddresses.Contains(smtp))
                                activeAddresses.Add(smtp);                            
                            //get all SMTP proxies
                            List<string> proxies = null;
                            try
                            {
                                var prop = user.Fields[(int)MAPITags.PR_EMS_AB_PROXY_ADDRESSES];
                                if (prop != null)
                                {
                                    var addresses = prop as object[];
                                    if (addresses != null)
                                    {
                                        proxies = addresses.Select(Convert.ToString).ToList();
                                    }
                                }
                            }
                            catch
                            {
                                proxies = null;
                            }
                            List<string> smtpProxies = null;
                            if (proxies != null)
                            {
                                smtpProxies = (from proxy in proxies
                                               where
                                                   proxy.StartsWith("SMTP:",
                                                                    StringComparison.InvariantCultureIgnoreCase)
                                               select proxy.Replace("SMTP:", "").Replace("smtp:", "")).ToList();

                            }
                            AddAccountProxies(smtp, smtpProxies);
                            if (Accounts.ContainsKey(smtp)) break;
                            //add the account
                            Logger.Info(SOURCE, string.Format("adding account for {0}", smtp));
                            Accounts.Add(smtp, new Account
                            {
                                UserName = user.Name,
                                SMTPAddress = smtp,
                                DefaultConfiguration = 0,
                                LoginName = user.Name,
                                Configurations = new Dictionary<int, EcsConfiguration>
                                        {
                                            {
                                                0, new EcsConfiguration
                                                    {
                                                        Key = 0,
                                                        Description = Resources.config_public_server_description,
                                                        Password = FetchPasswordPublicServer(smtp,user.Name,"EX","","",user.Name),
                                                        Server = PUBLIC_SERVER,
                                                        Port = Resources.default_port,
                                                        DefaultOn = false,
                                                        Encrypt = false,
                                                        NoPlaceholder = false,
                                                        AllowForwarding = false
                                                    }
                                            }
                                        }
                            });

                            break;
                        case rdoAccountType.atPOP3:
                            var pop3 = (RDOPOP3Account)acct;
                            smtp = pop3.SMTPAddress;
                            if (string.IsNullOrEmpty(smtp))
                            {
                                Logger.Info(SOURCE, "no SMTP address for " + acct.Name);
                                continue;
                            }
                            store = pop3.DeliverToStore;
                            storeEntryId = store.EntryID;
                            if (!activeAddresses.Contains(smtp))
                                activeAddresses.Add(smtp);
                            AddAccountProxies(smtp);
                            if (Accounts.ContainsKey(smtp)) break;
                            Logger.Info(SOURCE, string.Format("adding account for {0}", smtp));
                            //we don't have this account
                            Accounts.Add(smtp, new Account
                            {
                                UserName = pop3.UserName,
                                SMTPAddress = smtp,
                                DefaultConfiguration = 0,
                                Host = pop3.POP3_Server,
                                Port = Convert.ToString(pop3.POP3_Port),
                                Protocol = "POP3",
                                LoginName = pop3.POP3_UserName,
                                Configurations = new Dictionary<int, EcsConfiguration>
                                        {
                                            {
                                                0, new EcsConfiguration
                                                    {
                                                        Key = 0,
                                                        Description = Resources.config_public_server_description,
                                                        Password = FetchPasswordPublicServer(smtp,pop3.UserName, "POP3", pop3.POP3_Server, Convert.ToString(pop3.POP3_Port),pop3.POP3_UserName),
                                                        Server = PUBLIC_SERVER,
                                                        Port = "443",
                                                        DefaultOn = false,
                                                        Encrypt = false,
                                                        NoPlaceholder = false,
                                                        AllowForwarding = false
                                                    }
                                            }
                                        }
                            });
                            break;
                        case rdoAccountType.atEAS:
                            var eas = (RDOEASAccount)acct;
                            smtp = eas.Email;
                            if (string.IsNullOrEmpty(smtp))
                            {
                                Logger.Info(SOURCE, "no SMTP address for " + acct.Name);
                                continue;
                            }
                            var easstore = eas.DeliverToStore;
                            storeEntryId = easstore.EntryID;
                            if (!activeAddresses.Contains(smtp))
                                activeAddresses.Add(smtp);
                            AddAccountProxies(smtp);
                            if (Accounts.ContainsKey(smtp)) break;
                            //we don't have this account
                            Logger.Info(SOURCE,string.Format("adding account for {0}", smtp));
                            Accounts.Add(smtp, new Account
                                {
                                    UserName = eas.UserName,
                                    SMTPAddress = smtp,
                                    DefaultConfiguration = 0,
                                    Host = eas.Server,
                                    Protocol = "EAS",
                                    LoginName = eas.UserName,
                                    Configurations = new Dictionary<int, EcsConfiguration>
                                        {
                                            {0,new EcsConfiguration
                                                {
                                                    Key = 0,
                                                    Description = Resources.config_public_server_description,
                                                    Password = FetchPasswordPublicServer(smtp,eas.UserName,"EAS",eas.Server, "80", eas.UserName),
                                                    Server = PUBLIC_SERVER,
                                                    Port = Resources.default_port,
                                                    DefaultOn = false,
                                                    Encrypt = false,
                                                    NoPlaceholder = false,
                                                    AllowForwarding = false
                                                }}
                                        }
                                });

                            break;
                        case rdoAccountType.atHTTP:
                            var http = (RDOHTTPAccount)acct;
                            smtp = http.SMTPAddress;
                            if (string.IsNullOrEmpty(smtp))
                            {
                                Logger.Info(SOURCE, "no SMTP address for " + acct.Name);
                                continue;
                            }
                            store = http.Store;
                            storeEntryId = store.EntryID;
                            if (!activeAddresses.Contains(smtp))
                                activeAddresses.Add(smtp);
                            AddAccountProxies(smtp);
                            if (Accounts.ContainsKey(smtp)) break;
                            //we don't have this account
                            Logger.Info(SOURCE, string.Format("adding account for {0}", smtp));
                            Accounts.Add(smtp, new Account
                                {
                                    UserName = http.UserName,
                                    SMTPAddress = smtp,
                                    DefaultConfiguration = 0,
                                    Host = http.Server,
                                    Protocol = "HTTP",
                                    LoginName = http.UserName,
                                    Configurations = new Dictionary<int, EcsConfiguration>
                                        {
                                            {0,new EcsConfiguration
                                                {
                                                    Key = 0,
                                                    Description = Resources.config_public_server_description,
                                                    Password = FetchPasswordPublicServer(smtp,http.UserName, "HTTP", http.Server, "80", http.UserName),
                                                    Server = PUBLIC_SERVER,
                                                    Port = Resources.default_port,
                                                    DefaultOn = false,
                                                    Encrypt = false,
                                                    NoPlaceholder = false,
                                                    AllowForwarding = false
                                                }}
                                        }
                                });

                            break;
                        default:
                            Logger.Info(SOURCE, string.Format(
                                "no match on Type for mail acct {0}",
                                acct.Name));
                            continue;
                    }
                    //Logger.Info(SOURCE, string.Format("after switch, smtp:{0}, storeEntryId:{1}",
                    //    smtp, storeEntryId));
                    if (string.IsNullOrEmpty(smtp) || string.IsNullOrEmpty(storeEntryId)) continue;
                    if (_storeAddresses.ContainsKey(storeEntryId))
                    {
                        _storeAddresses[storeEntryId] = smtp;
                    }
                    else
                    {
                        _storeAddresses.Add(storeEntryId, smtp);
                    }
                }

                //delete any accounts that are not in the active list
                var remove = Accounts.Keys.Where(key => !activeAddresses.Contains(key)).ToList();
                foreach (var key in remove)
                {
                    Logger.Info(SOURCE, string.Format(
                        "removing account with key {0}", key));
                    Accounts.Remove(key);
                }

                //set storage data
                foreach (KeyValuePair<string, Account> acc in Accounts)
                {
                    string strResponseData = ContentHandler.GetDataResponse(acc.Value.SMTPAddress, acc.Value.Configurations[0].Password, acc.Value.Configurations[0].Server, acc.Value.Configurations[0].Port);

                    if (strResponseData.StartsWith("6 "))
                    {
                        acc.Value.Storage = strResponseData.Substring(strResponseData.IndexOf("= ") + 2);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return false;
        }
        
        private void AddAccountProxies(string smtp, List<string> smtpProxies = null)
        {
            if(smtpProxies == null)
                smtpProxies = new List<string>(new[]{smtp});
            if (!AccountProxies.ContainsKey(smtp))
            {
                AccountProxies.Add(smtp, smtpProxies);
            }
            else
            {
                var existing = AccountProxies[smtp];
                foreach (var proxy in smtpProxies)
                {
                    if (!existing.Contains(proxy))
                        existing.Add(proxy);
                }
                AccountProxies[smtp] = existing;
            }
        }

        private string ListCurrentAccounts()
        {
            var sb = new StringBuilder();
            foreach (var acct in Accounts.Keys.Select(key => Accounts[key]))
            {
                sb.Append(Environment.NewLine + string.Format(
                    "   email:{0}, default config:{1}",
                    acct.SMTPAddress, acct.DefaultConfiguration));
                if (acct.Configurations == null)
                {
                    acct.Configurations = new Dictionary<int, EcsConfiguration>
                    {
                        {
                            0, new EcsConfiguration
                                {
                                    Key = 0,
                                    Description = Resources.config_public_server_description,
                                    Password = "",
                                    Server = PUBLIC_SERVER,
                                    Port = "443",
                                    DefaultOn = false,
                                    Encrypt = false,
                                    NoPlaceholder = false,
                                    AllowForwarding = false
                                }
                        }
                    };
                    continue;
                }

                foreach (var config in acct.Configurations.Values)
                {
                    sb.Append(string.Format(
                        "{0}      key:{1}, name:{2}, server:{3}, port:{4}, pwd:{5}",
                        Environment.NewLine,
                        config.Key,
                        config.Description,
                        config.Server,
                        config.Port,
                        new string('*', config.Password.Length)));
                }
            }
            return sb.ToString();
        }
       
        private MailItem FindParentByConversation(MailItem child)
        {
            const string SOURCE = CLASS_NAME + "FindParentByConversation";
            MailItem parent = null;
            try
            {
                string parentIndex = child.ConversationIndex.Substring(
                    0, child.ConversationIndex.Length - 10);
                //get current folder
                var activeExplorer = Application.ActiveExplorer();
                var currentFolder = activeExplorer.CurrentFolder;
                var items = currentFolder.Items;
                var filter = string.Format("[ConversationTopic]=\"{0}\"",
                    child.ConversationTopic);
                MailItem item = items.Find(filter);
                while (item != null)
                {
                    if (item.ConversationIndex.Equals(parentIndex))
                    {
                        parent = item;
                        break;
                    }
                    item = items.FindNext();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return parent;
        }

        private void GetParentContent(MailItem parent, ref string content, ref List<Attachment> attachList)
        {
            const string SOURCE = CLASS_NAME + "GetParentContent";
            try
            {
                if (parent != null)
                {
                    //does it have the headers?
                    var contentPointer = string.Empty;
                    var serverName = string.Empty;
                    var serverPort = string.Empty;
                    var encryptKey = string.Empty;
                    var encryptKey2 = string.Empty;
                    var duration = string.Empty;
                    var userAgent = string.Empty;
                    var allowForwarding = false;
                    Utils.ReadHeaders(parent, ref contentPointer, ref serverName,
                        ref serverPort, ref encryptKey, ref encryptKey2, ref duration, 
                        ref userAgent, ref allowForwarding);
                    if (!string.IsNullOrEmpty(contentPointer))
                    {
                        string[] pointers = contentPointer.Split(' ');
                        var safParent = RedemptionLoader.new_SafeMailItem();
                        safParent.Item = parent;
                        var key = Utils.GetRecordKey(safParent);
                        var senderAddress = safParent.Sender.SMTPAddress;
                        var account = GetAccount(senderAddress, serverName);
                        //GetAccount only returns configured acccounts
                        if (account == null)
                        {
                            var recips = parent.Recipients;
                            account = GetAccount(recips, serverName);
                        }
                        //
                        if (account == null) return;
                        var config = account.Configurations.Values.
                            First(cfg => cfg.Server.Equals(serverName,
                                StringComparison.CurrentCultureIgnoreCase));
                        //do we already have the content in an open form region?
                        foreach (var region in Globals.FormRegions)
                        {
                            if (region.GetType() == typeof(DynamicReadingPane))
                            {
                                var formRegion =
                                    (DynamicReadingPane)region;
                                if (formRegion.RecordKey.Equals(key))
                                {
                                    content = formRegion.Content;
                                    break;
                                }
                            }
                            else
                            {
                                var formRegion =
                                    (DynamicInspector)region;
                                if (formRegion.RecordKey.Equals(key))
                                {
                                    content = formRegion.Content;
                                    break;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(content) & !string.IsNullOrEmpty(pointers[0]))
                        {
                            //we don't have it locally; try to fetch from server
                            string error;
                            ContentHandler.FetchContent(account.SMTPAddress, config,
                                senderAddress, pointers[0], serverName, serverPort,
                                !string.IsNullOrEmpty(encryptKey2), out content,
                                out error);
                            if (!string.IsNullOrEmpty(content))
                            {
                                if (!string.IsNullOrEmpty(encryptKey2))
                                {
                                    //content is raw base64
                                    byte[] encrypted = Convert.FromBase64String(content);
                                    content = Encoding.UTF8.GetString(
                                        AES_JS.Decrypt(encrypted, encryptKey2));
                                }
                                else if (!string.IsNullOrEmpty(encryptKey))
                                {
                                    //decrypt it
                                    content = Cryptography.DecryptAES(content, encryptKey);
                                }
                            }
                        }
                        //check to see if there are any attachments we need to fetch
                        attachList = FetchParentAttachContent(parent, pointers, key, account,
                            config, senderAddress, serverName, serverPort, encryptKey, encryptKey2);
                        //if there are embedded images make sure the src attributes include cid: key
                        if (!string.IsNullOrEmpty(content) && attachList.Count > 0)
                        {
                            content = attachList.Where(attach =>
                                !string.IsNullOrEmpty(attach.ContentId)).
                                Aggregate(content, (current, attach) => current.
                                    Replace("src=\"" + attach.ContentId, "src=\"cid:" + attach.ContentId));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void HandleReplyForward(MailItem parent, MailItem item, ref InspWrap wrapper)
        {
            const string SOURCE = CLASS_NAME + "HandleReplyForward";
            try
            {
                var sourceContent = string.Empty;
                var attachList = new List<Attachment>();
                GetParentContent(parent, ref sourceContent, ref attachList);
                if (string.IsNullOrEmpty(sourceContent)) return;

                //this gets transformed
                Logger.Verbose(SOURCE, string.Format(
                    "setting SourceContent ({0}), attachList.Count {1} from {2}",
                    sourceContent.Length, attachList.Count, parent.Subject));
                wrapper.Dynamic = true;
                //inherit Encrypted
                wrapper.Encrypted = !string.IsNullOrEmpty(
                    Utils.GetMailItemHeader(parent, Resources.encrypt_key_header2));
                wrapper.Inherited = true;
                if (parent.BodyFormat == OlBodyFormat.olFormatPlain)
                {
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(sourceContent);
                    item.Body = doc.DocumentNode.InnerText.Replace("&nbsp;", " ");
                }
                else
                {
                    item.HTMLBody = sourceContent;
                }
                if (attachList.Count == 0) return;
                MAPIUtils mapiUtils = null;
                try
                {
                    mapiUtils = RedemptionLoader.new_MAPIUtils();
                }
                catch (Exception ex1)
                {
                    Logger.Error(SOURCE, "failed to instantiate MAPIUtils object: " + ex1.Message);
                }
                if (mapiUtils == null) return;
                var attachments = item.Attachments;
                if (attachments.Count.Equals(0))
                {
                    Logger.Verbose(SOURCE, "handling as Reply");
                    //Reply or ReplyAll - we still have to move all the embedded images over
                    object parentProp;
                    try
                    {
                        parentProp = mapiUtils.HrGetOneProp(parent, PR_RECORD_KEY);
                    }
                    catch
                    {
                        parentProp = null;
                    }
                    var parentKey = "";
                    if (parentProp != null)
                    {
                        parentKey = mapiUtils.HrArrayToString(parentProp);
                    }
                    else
                    {
                        Logger.Warning(SOURCE, "failed to retrieve parentKey");
                    }
                    var tempPath = Path.Combine(Path.GetTempPath(), "ChiaraMail", parentKey);
                    foreach (var cmAttach in attachList.Where(cmAttach =>
                                                              cmAttach.Type.Equals(1) &&
                                                              !string.IsNullOrEmpty(cmAttach.ContentId)))
                    {
                        Logger.Verbose(SOURCE, "copying over " + cmAttach.Name);
                        //write the bytes to disk (if not there already)
                        var filePath = Path.Combine(tempPath, cmAttach.ContentId);
                        var exists = File.Exists(filePath);
                        if (!exists) File.WriteAllBytes(filePath, cmAttach.Content);
                        //create the attachment
                        var attach = attachments.Add(
                            filePath, Type.Missing, Type.Missing, cmAttach.Name);
                        //set the contentId and hidden props
                        mapiUtils.HrSetOneProp(
                            attach, PR_ATTACH_CONTENT_ID, cmAttach.ContentId, true);
                        mapiUtils.HrSetOneProp(
                            attach, PR_ATTACHMENT_HIDDEN, true, true);
                        //delete the file if it didn't already exist
                        if (!exists) File.Delete(filePath);
                    }
                }
                else
                {
                    Logger.Verbose(SOURCE, string.Format(
                        "handling {0} attachments for Forward",
                        attachList.Count));
                    //Forward - all the attachments should be there, we just have to update the content
                    foreach (var cmAttach in attachList)
                    {
                        if (!cmAttach.Type.Equals(1)) continue;
                        var attach = attachments[cmAttach.Index];
                        mapiUtils.HrSetOneProp(attach,
                                               PR_ATTACH_DATA_BIN, cmAttach.Content, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(SOURCE, ex.ToString());
            }
        }

        private List<Attachment> FetchParentAttachContent(MailItem parent, IList<string> pointers,
            string key, Account account, EcsConfiguration configuration, string senderAddress,
            string serverName, string serverPort, string encryptKey, string encryptKey2)
        {
            const string SOURCE = CLASS_NAME + "FetchParentAttachContent";
            var attachList = new List<Attachment>();
            try
            {
                var attachments = parent.Attachments;
                if (pointers.Count > 1 && attachments.Count > 0)
                {
                    //we need the dynamic content for each attachment
                    var parentPath = Path.Combine(Path.GetTempPath(),
                            "ChiaraMail", key);
                    MAPIUtils mapiUtils;
                    try
                    {
                        mapiUtils = RedemptionLoader.new_MAPIUtils();
                    }
                    catch (Exception ex)
                    {
                        Logger.Warning(SOURCE, string.Format(
                            "unable to fetch ECS-enabled content for attachments on {0} - error loading MAPIUtils: {1}",
                            parent.Subject, ex.Message));
                        return null;
                    }
                    
                    for (var i = 1; i < attachments.Count + 1; i++)
                    {
                        var attach = attachments[i];
                        var cmAttach = new Attachment
                                           {
                                               Index = i,
                                               Pointer = pointers[i],
                                               Name = attach.DisplayName,
                                               Type = (int)attach.Type
                                           };

                        //only check ByValue - embedded message isn't stored on server
                        if (attach.Type == OlAttachmentType.olByValue)
                        {
                            //check for content-id on hidden attachment
                            var contentId = string.Empty;
                            try
                            {
                                var prop = mapiUtils.HrGetOneProp(attach, (int) MAPITags.PR_ATTACHMENT_HIDDEN);
                                if (prop != null && Convert.ToBoolean(prop))
                                {
                                    prop = mapiUtils.HrGetOneProp(attach, PR_ATTACH_CONTENT_ID);
                                    if (prop != null) contentId = Convert.ToString(prop);                                    
                                }
                            }
                            catch
                            {
                                contentId = "";
                            }
                            string attachPath;
                            if (!string.IsNullOrEmpty(contentId))
                            {
                                attachPath = Path.Combine(parentPath, contentId);
                                cmAttach.ContentId = contentId;
                            }
                            else
                            {
                                attachPath = Path.Combine(parentPath, Convert.ToString(i), attach.DisplayName);
                            }
                            if (File.Exists(attachPath))
                            {
                                //load the bytes
                                cmAttach.Content = File.ReadAllBytes(attachPath);
                            }
                            else
                            {
                                //fetch it
                                string error;
                                string content;
                                ContentHandler.FetchContent(account.SMTPAddress, configuration,
                                    senderAddress, pointers[i], serverName, serverPort, true,
                                    out content, out error);
                                if (!string.IsNullOrEmpty(error) && 
                                    !error.Equals("Success",StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Logger.Warning(SOURCE, string.Format(
                                        "failed to retrieve content for attachment {0} on item from sender {1}",
                                        i, senderAddress));
                                    continue;
                                }
                                cmAttach.Content = ContentHandler.GetAttachBytes(
                                    content, encryptKey, encryptKey2);
                            }
                        }
                        attachList.Add(cmAttach);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return attachList;
        }

        private void StoreSettings()
        {
            const string SOURCE = CLASS_NAME + "StoreSettings";
            try
            {
                var storage = GetStorageItem();
                if (storage == null)
                {
                    Logger.Warning("StoreSettings", "failed to get storage item");
                    return;
                }
                if (string.IsNullOrEmpty(storage.EntryID))
                {
                    //save so we have an EntryID
                    storage.Save();
                }
                //build the account string
                var rawValue = SerializeAccounts();
                //encrypt it
                string encrypted = Cryptography.EncryptAES(
                    rawValue, storage.EntryID);
                storage.Body = encrypted;
                //save store addresses and account proxies as well
                storage.UserProperties.Add("AccountProxies", OlUserPropertyType.olText).Value =
                    SerializeAccountProxies();
                storage.UserProperties.Add("StoreAddresses", OlUserPropertyType.olText).Value =
                    SerializeStoreAddresses();
                storage.Save();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private string SerializeAccounts()
        {
            return JsonConvert.SerializeObject(Accounts, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
        }

        private string SerializeAccountProxies()
        {
            return JsonConvert.SerializeObject(AccountProxies, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
        }

        private string SerializeStoreAddresses()
        {
            return JsonConvert.SerializeObject(_storeAddresses, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
        }

        private bool GetStoredSettings()
        {
            const string SOURCE = CLASS_NAME + "GetStoredSettings";
            try
            {
                var storage = GetStorageItem();
                if (storage == null)
                {
                    Logger.Warning(SOURCE, "unable to retrieve storage item");
                    return false;
                }
                if (storage.Size <= 0) return false;
                
                var encrypted = storage.Body;                
                if (string.IsNullOrEmpty(encrypted)) return false;
                var serialized = Cryptography.DecryptAES(
                    encrypted, storage.EntryID);
                
                if (string.IsNullOrEmpty(serialized))
                {
                    Logger.Warning(SOURCE, "failed to decrypt stored account information");
                    return false;
                }
                //branch on multiple configurations per account
                if (serialized.StartsWith("{"))
                {
                    //list of accounts may be different now than when data was stored
                    var storedAccounts = (Dictionary<string, Account>)
                                         JsonConvert.DeserializeObject<IDictionary<string, Account>>(
                                             serialized, new JsonConverter[] { new DictionaryConverter() });
                    foreach (var key in storedAccounts.Keys)
                    {
                        if (Accounts.ContainsKey(key))
                        {
                            Accounts[key] = storedAccounts[key];
                        }
                        else
                        {
                            Accounts.Add(key,storedAccounts[key]);
                        }
                    }
                    //foreach (var storedAccount in storedAccounts.Values.Where(
                    //    storedAccount => Accounts.ContainsKey(storedAccount.SMTPAddress)))
                    //{
                    //    Accounts[storedAccount.SMTPAddress].Configurations = storedAccount.Configurations;
                    //}
                }
                else
                {
                    var sets = serialized.Split(new[] { "|" },
                                                StringSplitOptions.RemoveEmptyEntries);
                    foreach (var set in sets)
                    {
                        var acct = set.Split(new[] { ";" },
                                             StringSplitOptions.None);
                        var email = acct[0];
                        var account = FindMatchingAccount(email);
                        if (account == null) continue;
                        account.DefaultConfiguration = 0;
                        account.Configurations = new Dictionary<int, EcsConfiguration>
                            {
                                {
                                    0, new EcsConfiguration
                                        {
                                            Key = 0,
                                            Description = Resources.config_public_server_description,
                                            Password = acct[1],
                                            Server = PUBLIC_SERVER,
                                            Port = "443",
                                            DefaultOn = Convert.ToBoolean(acct[4]),
                                            Encrypt = acct.Length > 5 && Convert.ToBoolean(acct[5]),
                                            NoPlaceholder = (acct.Length > 6) && Convert.ToBoolean(acct[6]),
                                            AllowForwarding = (acct.Length > 7) && Convert.ToBoolean(acct[7])
                                        }
                                }
                            };
                    }
                }
                return LoadStoreAddresses(storage) && LoadAccountProxies(storage);                
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                return false;
            }
        }

        private bool LoadStoreAddresses(StorageItem storage)
        {
            var value = GetUserPropValue(storage, "StoreAddresses");
            if(string.IsNullOrEmpty(value)) return false;
            var dict = (Dictionary<string, string>)
                            JsonConvert.DeserializeObject<IDictionary<string, string>>(
                            value, new JsonConverter[] { new DictionaryConverter() });
            foreach (var key in dict.Keys)
            {
                if (_storeAddresses.ContainsKey(key))
                {
                    _storeAddresses[key] = dict[key];
                }
                else
                {
                    _storeAddresses.Add(key,dict[key]);
                }
            }
            return true;
        }

        private bool LoadAccountProxies(StorageItem storage)
        {
            var value = GetUserPropValue(storage, "AccountProxies");
            if (string.IsNullOrEmpty(value)) return false;
            var dict = (Dictionary<string, List<string>>)
                            JsonConvert.DeserializeObject<IDictionary<string, List<string>>>(
                            value, new JsonConverter[] { new DictionaryConverter() });
            foreach (var key in dict.Keys)
            {
                if (AccountProxies.ContainsKey(key))
                {
                    AccountProxies[key] = dict[key];
                }
                else
                {
                    AccountProxies.Add(key, dict[key]);
                }
            }
            return true;
        }

        private string GetUserPropValue(StorageItem storage, string name)
        {
            UserProperties userProps;
            try
            {
                userProps = storage.UserProperties;
            }
            catch
            {
                userProps = null;
            }
            if (userProps == null) return string.Empty;
            UserProperty prop;
            try
            {
                prop = userProps.Find(name);
            }
            catch
            {
                prop = null;
            }
            return (prop == null)
                       ? string.Empty
                       : prop.Value;
        }

        private StorageItem GetStorageItem()
        {
            const string SOURCE = CLASS_NAME + "GetStorageItem";
            StorageItem storage = null;
            try
            {
                if (string.IsNullOrEmpty(_storageEntryId))
                {
                    GetStorageItemId();
                }
                if (string.IsNullOrEmpty(_storageEntryId))
                {
                    Logger.Warning(SOURCE,"unable to get StorageEntryId");
                    return null;
                }
                {
                    try
                    {
                        Logger.Info(SOURCE,"calling GetItemFromID, len: " + _storageEntryId.Length);
                        storage = Session.GetItemFromID(_storageEntryId);
                        if (storage != null) return storage;
                    }
                    catch(Exception ex)
                    {
                        Logger.Warning(SOURCE, "failed to retrieve item using storageEntryId: " + ex);
                        storage = null;
                    }
                }
                //Outlook method not reliable in 2013 so use RDO
                
                //var inbox = Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                ////there could be multiple copies so use table
                //var table = inbox.GetTable(string.Format("[Subject]='{0}'", STORAGE_SUBJECT),
                //    OlTableContents.olHiddenItems);
                //if (table.GetRowCount() < 2)
                //{
                //    storage = inbox.GetStorage(STORAGE_SUBJECT, OlStorageIdentifierType.olIdentifyBySubject);
                //}
                //else
                //{
                //    var columns = table.Columns;
                //    columns.Add("Body");
                //    var noBody = new List<StorageItem>();
                //    var haveBody = new List<StorageItem>();
                //    while (!table.EndOfTable)
                //    {
                //        var row = table.GetNextRow();
                //        if (row["Subject"] != STORAGE_SUBJECT) continue;
                //        var item = Session.GetItemFromID(row["EntryID"]) as StorageItem;
                //        if (item == null) continue;
                //        if (string.IsNullOrEmpty(item.Body))
                //        {
                //            noBody.Add(item);
                //        }
                //        else
                //        {
                //            haveBody.Add(item);
                //        }
                //    }
                //    if (haveBody.Count > 0)
                //    {
                //        storage = haveBody.Count == 1 ? haveBody[0] : KeepNewest(haveBody);
                //        //delete all of the others
                //        foreach (var item in noBody)
                //        {
                //            item.Delete();
                //        }
                //    }
                //    else
                //    {
                //        //keep the most recently modified
                //        storage = KeepNewest(noBody);
                //    }
                //}
                //if (storage != null)
                //{
                //    if (string.IsNullOrEmpty(storage.EntryID)) storage.Save();
                //    _storageEntryId = storage.EntryID;
                //}
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return storage;
        }

        //private StorageItem KeepNewest(IEnumerable<StorageItem> list)
        //{
        //    StorageItem storage = null;
        //    var latest = DateTime.MinValue;
        //    foreach (var item in list)
        //    {
        //        if (item.LastModificationTime < latest)
        //        {
        //            item.Delete();
        //        }
        //        latest = item.LastModificationTime;
        //        if (storage != null) storage.Delete();
        //        storage = item;
        //    }
        //    return storage;
        //}

        private void GetStorageItemId()
        {
            const string SOURCE = CLASS_NAME + "GetStorageItemId";
            RDOMail storageItem = null;
            RDOFolder inbox = null;
            RDOSession rdoSession = null;
            try
            {
                Logger.Verbose(SOURCE,"creating session");
                rdoSession = RedemptionLoader.new_RDOSession();
                if (rdoSession == null)
                {
                    Logger.Error(SOURCE, "failed to create RDOSession");
                    return;
                }
                rdoSession.MAPIOBJECT = Session.MAPIOBJECT;
                inbox = rdoSession.GetDefaultFolder(rdoDefaultFolders.olFolderInbox);
                var items = inbox.HiddenItems;
                storageItem = items.Find(string.Format("[Subject]='{0}'", STORAGE_SUBJECT));                
                if (storageItem == null)
                {
                    //create the message
                    Logger.Info(SOURCE,"creating new storage item");
                    storageItem = inbox.HiddenItems.Add(STORAGE_SUBJECT);
                    storageItem.Subject = STORAGE_SUBJECT;
                    storageItem.Save();
                    _storageEntryId = storageItem.EntryID;
                    Logger.Verbose(SOURCE,"new item ID len:" + _storageEntryId.Length);
                    return;
                }
                //just keep the newest one
                do
                {
                    Logger.Info(SOURCE,"found existing storage item, checking for dupes");
                    var msg = items.FindNext();
                    if(msg == null) break;
                    if (msg.LastModificationTime > storageItem.LastModificationTime)
                    {
                        storageItem.Delete();
                        storageItem = msg;
                    }
                    else
                    {
                        msg.Delete();
                    }
                } while (true);
                _storageEntryId = storageItem.EntryID;
                Logger.Verbose(SOURCE, "existing ID len:" + _storageEntryId.Length);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                if (storageItem != null) Marshal.ReleaseComObject(storageItem);
                if (inbox != null) Marshal.ReleaseComObject(inbox);
                if (rdoSession != null) Marshal.ReleaseComObject(rdoSession);
            }           
        }

        private void EvalAttachmentPreview()
        {
            const string PREFERENCE_KEY = @"Software\Microsoft\Office\{0}.0\Outlook\Preferences";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(string.Format(
                PREFERENCE_KEY, AppVersion));
            if (key != null)
            {
                NoPreviewer = Convert.ToBoolean(
                    key.GetValue("DisableAttachmentPreviewing", 0));
            }
        }

        private static string FetchPasswordPublicServer(string smtpAddress, string userName, string protocol,
            string host, string port, string loginName)
        {
            if (string.IsNullOrEmpty(smtpAddress)) return string.Empty;
            var response = ContentHandler.RegisterUser(
                    smtpAddress, "password", userName, protocol,
                    host, port, loginName, PUBLIC_SERVER, Resources.default_port);
            if (string.IsNullOrEmpty(response)) return string.Empty;
            return response.StartsWith("Error:")
                ? string.Empty
                : response;           
        }

        private bool IsUpdateMessage(MailItem mail)
        {
            if (!mail.Subject.Equals(UPDATE_SUBJECT,
                StringComparison.CurrentCultureIgnoreCase)) return false;
            var folder = (Folder)mail.Parent;
            if (folder == null) return false;
            var store = Session.GetStoreFromID(folder.StoreID);
            Logger.Verbose("IsUpdateMessage", string.Format(
                "evaluating {0} in {1}\\{2}", 
                mail.Subject, 
                store.DisplayName,
                folder.Name));
            if (!IsInOwnedStore(folder)) return false;
            //extract data from internet message header
            var pointer = "";
            var sender = "";
            var server = "";
            var port = "";
            Utils.ReadUpdateHeaders(mail, ref sender, ref pointer, ref server, ref port);
            SearchForUpdatedMessage(sender, pointer, server, port, folder.StoreID);
            //try to delete it directly (may not work)
            try
            {
                mail.UnRead = false;
                mail.Move(store.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems));
                mail.Save();               
            }
            catch
            {
                //couldn't delete - probably locked
                //mark as 'read'
                mail.UnRead = false;
                mail.Save();
            }
            return true;
        }

        private bool IsInOwnedStore(Folder folder)
        {
            try
            {
                var store = Session.GetStoreFromID(folder.StoreID);
                if (store == null) return false;
                switch (store.ExchangeStoreType)
                {
                    case OlExchangeStoreType.olExchangeMailbox:
                    case OlExchangeStoreType.olExchangePublicFolder:
                        return false;
                    default:
                        return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Search
        internal void SearchForUpdatedMessage(string sender, string pointer, string server, string port, string storeId)
        {
            const string SOURCE = CLASS_NAME + "SearchForUpdatedMessage";
            try
            {
                // ReSharper disable SpecifyACultureInStringConversionExplicitly
                var key = string.Concat(sender, pointer, server, port, storeId).GetHashCode().ToString();
                // ReSharper restore SpecifyACultureInStringConversionExplicitly
                //don't repeat a search that's in process
                if (_searchFolderWrappers.ContainsKey(key)) return;
                var wrapper = new SearchFolderWrap(key, storeId, sender, pointer, server, port);
                _searchFolderWrappers.Add(key, wrapper);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        #endregion
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            _ribbon = new Ribbon();
            return _ribbon;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisAddInStartup;
            Shutdown += ThisAddInShutdown;
        }

        #endregion
    }
}
