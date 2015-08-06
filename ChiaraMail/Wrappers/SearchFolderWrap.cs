using System;
using System.Text.RegularExpressions;
using ChiaraMail.Properties;
using Redemption;

namespace ChiaraMail.Wrappers
{
    internal class SearchFolderWrap
    {
        internal string Pointer { get; private set; }
        internal string Sender { get; private set; }
        internal string Server { get; private set; }
        internal string Port { get; private set; }
        internal string Key { get; private set; }
        internal string StoreId { get; private set; }
        private const string CLASS_NAME = "SearchFolderWrap.";
        private RDOSearchFolder _searchFolder;

        public SearchFolderWrap(string key, string storeId, string sender, string pointer, 
            string server, string port)
        {
            Pointer = pointer;
            Sender = sender;
            Server = server;
            Port = port;
            StoreId = storeId;
            Key = key;
            InitSearchFolder();
        }
               
        private void InitSearchFolder()
        {
            const string SOURCE = CLASS_NAME + "InitSearchFolder";
            RDOStore store = null;
            RDOFolder searchRoot = null;
            RDOFolder ipmRoot = null;
            try
            {
                //don't repeat a search that's in process
                var rdoSession = RedemptionLoader.new_RDOSession();
                rdoSession.MAPIOBJECT = Globals.ThisAddIn.Session.MAPIOBJECT;
                store = rdoSession.GetStoreFromID(StoreId);
                Logger.Verbose("InitSearchFolder", "initiating search in " + store.Name);
                //get the SearchFolders collection
                searchRoot = store.SearchRootFolder;
                                
                var folders = searchRoot.Folders;
                //look for existing folder
                _searchFolder = (RDOSearchFolder) folders[Key] ;
                if (_searchFolder != null)
                {
                    Logger.Verbose(SOURCE, "found existing search folder for " + Key);
                }
                else
                {
                    //create the search folder                     
                    Logger.Verbose(SOURCE, "creating new search folder for " + Key);
                    _searchFolder = folders.AddSearchFolder(Key);
                }
                if (_searchFolder == null)
                {
                    Logger.Error(SOURCE, "unable to get or create search folder " + Key);
                    return;
                }
                //make sure it's initialized correctly
                ipmRoot = store.IPMRootFolder;
                //set the criteria
                var criteria = _searchFolder.SearchCriteria;
                var rstrAnd = (RestrictionAnd)criteria.SetKind(RestrictionKind.RES_AND);
                //pointer - could be at start, middle or end of string
                var rstrPointer = (RestrictionContent)rstrAnd.Add(RestrictionKind.RES_CONTENT);
                rstrPointer.lpProp = Pointer;
                var tag = _searchFolder.GetIDsFromNames(ThisAddIn.PS_INTERNET_HEADERS, Resources.content_header)
                    | ThisAddIn.PT_STRING8;
                rstrPointer.ulPropTag = tag;
                rstrPointer.ulFuzzyLevel =
                    ContentFuzzyLevel.FL_SUBSTRING | ContentFuzzyLevel.FL_IGNORECASE;
                //sender
                var rstrSender = (RestrictionContent)rstrAnd.Add(RestrictionKind.RES_CONTENT);
                rstrSender.lpProp = Sender;
                rstrSender.ulPropTag = (int)MAPITags.PR_SENDER_EMAIL_ADDRESS;
                rstrSender.ulFuzzyLevel = ContentFuzzyLevel.FL_IGNORECASE;
                //server
                var rstrServer = (RestrictionContent)rstrAnd.Add(RestrictionKind.RES_CONTENT);
                rstrServer.lpProp = Server;
                tag = _searchFolder.GetIDsFromNames(ThisAddIn.PS_INTERNET_HEADERS, Resources.server_header)
                    | ThisAddIn.PT_STRING8;
                rstrServer.ulPropTag = tag;
                rstrServer.ulFuzzyLevel = ContentFuzzyLevel.FL_IGNORECASE;
                //port
                var rstrPort = (RestrictionContent)rstrAnd.Add(RestrictionKind.RES_CONTENT);
                rstrPort.lpProp = Port;
                tag = _searchFolder.GetIDsFromNames(ThisAddIn.PS_INTERNET_HEADERS, Resources.port_header)
                    | ThisAddIn.PT_STRING8;
                rstrPort.ulPropTag = tag;
                rstrPort.ulFuzzyLevel = ContentFuzzyLevel.FL_IGNORECASE;

                //set the search container as the mailbox
                _searchFolder.SearchContainers.Add(ipmRoot);
                _searchFolder.IsRecursiveSearch = true;
                Logger.Verbose(SOURCE, "adding handler for UpdatedMessageSearchComplete");
                //set up a handler
                _searchFolder.OnSearchComplete +=
                    UpdatedMessageSearchComplete;
                _searchFolder.Start();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                //Utils.ReleaseObject(stores);
                Utils.ReleaseObject(store);
                Utils.ReleaseObject(searchRoot);
                Utils.ReleaseObject(ipmRoot);
            }
        }

        private void UpdatedMessageSearchComplete()
        {
            const string SOURCE = CLASS_NAME + "UpdatedMessageSearchComplete";
            try
            {
                Logger.Info(SOURCE, "fired");
                //release this handler
                _searchFolder.OnSearchComplete -=
                    UpdatedMessageSearchComplete;
                //stop the search
                _searchFolder.Stop();
                //if search returned anything read data from newest message that we can decrypt
                var items = _searchFolder.Items;
                Logger.Info(SOURCE, string.Format(
                    "search {0} returned {1} items",
                    Key, items.Count));
                if (items.Count > 0)
                {
                    foreach (RDOMail item in items)
                    {
                        //pointer could be at start, middle or end of string - check for it as a word
                        var tag = item.GetIDsFromNames(ThisAddIn.PS_INTERNET_HEADERS,
                                                       Resources.content_header) | ThisAddIn.PT_STRING8;
                        var header = Convert.ToString(item.Fields[tag]);
                        if (!Regex.IsMatch(header,
                                           string.Format(@"\b{0}\b", Pointer),
                                           RegexOptions.IgnoreCase)) continue;
                        item.UnRead = true;
                        item.Save();
                    }
                }
                //delete the folder regardless
                var parent = _searchFolder.Parent;
                var folders = parent.Folders;
                for (var i = 1; i <= folders.Count; i++)
                {
                    if (folders[i].Name != _searchFolder.Name) continue;
                    folders.Remove(i);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                
                Globals.ThisAddIn.ReleaseSearchFolderWrap(Key);
            }
        }

        //private void OnMessageClassSearchComplete()
        //{
        //    var source = CLASS_NAME + "OnMessageClassSearchComplete";
        //    try
        //    {
        //        Logger.Info(source, "fired");
        //        //release this handler
        //        //_searchFolder.OnSearchComplete -=
        //        //    OnMessageClassSearchComplete;
        //        ////stop the search
        //        //_searchFolder.Stop();
        //        //if search returned anything handle each message
        //        var items = _searchFolder.Items;
        //        Logger.Info(source, string.Format(
        //            "search {0} returned {1} items",
        //            Key, items.Count));
        //        if (items.Count <= 0) return;
        //        foreach (RDOMail item in items)
        //        {
        //            item.MessageClass = Resources.message_class_CM;
        //            item.Save();
        //        }
        //        //leave search folder in place
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(source, ex.ToString());
        //    }
        //    //finally
        //    //{
        //    //    Globals.ThisAddIn.ReleaseSearchFolderWrap(Key);
        //    //}
        //}
    }
}
