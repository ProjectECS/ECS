using System;
using System.Linq;
using Redemption;

namespace ChiaraMail.Wrappers
{
    internal class StoreWrap
    {
        private const string CLASS_NAME = "StoreWrap.";
        private RDOSearchFolder _searchFolder;
        private string StoreId { get; set; }
        private RDOStore Store { get; set; }
        public StoreWrap(string storeId)
        {
            StoreId = storeId;
             var session = RedemptionLoader.new_RDOSession();
            session.MAPIOBJECT = Globals.ThisAddIn.Session.MAPIOBJECT;
            Store = session.GetStoreFromID(StoreId);
            //Store.OnNewMail += StoreOnNewMail;
            //create a search folder to check for update messages
            LaunchInitialSearch();
        }

        //private void StoreOnNewMail(string entryId)
        //{
        //    const string SOURCE = CLASS_NAME + "StoreOnNewMail";
        //    RDOMail mail = null;
        //    try
        //    {
        //        Logger.Verbose(SOURCE,"fired");
        //        return;
        //        mail = Store.GetMessageFromID(entryId);
        //        if (mail == null) return;
        //        //intercept Message Updated
        //        if (IsUpdateMessage(mail))
        //        {
        //            ThreadPool.QueueUserWorkItem(DeleteNewMail, entryId);
        //            return;
        //        }
        //        //check for our headers
        //        if (mail.MessageClass == Resources.message_class_CM
        //            || !Utils.HasChiaraHeader(mail))
        //            return;
        //        Logger.Info(SOURCE, string.Format(
        //            "updating message class on {0} in {1}",
        //            mail.Subject, Store.Name));
        //        mail.MessageClass = Resources.message_class_CM;
        //        mail.Save();
        //    }
        //    catch
        //    {
        //        mail = null;
        //    }
        //    finally
        //    {
        //        if (mail != null) Utils.ReleaseObject(mail);
        //    }           
        //}

        private bool IsUpdateMessage(RDOMail mail)
        {
            const string SOURCE = CLASS_NAME + "IsUpdateMessage";
            if (!mail.Subject.Equals(ThisAddIn.UPDATE_SUBJECT,
                StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!mail.UnRead) return false;
            //ignore if more than 1 day old
            if (mail.SentOn.AddDays(1) < DateTime.Now) return false;
            Logger.Verbose(SOURCE, string.Format(
                "evaluating {0} in {1}", mail.Subject, Store.Name));
            //extract data from internet message header
            var pointer = "";
            var sender = "";
            var server = "";
            var port = "";
            Utils.ReadUpdateHeaders(mail, ref sender, ref pointer, ref server, ref port);
            Logger.Verbose(SOURCE, string.Format(
                "found {0} for {1} from {2} in {3}", pointer, mail.Subject, sender, Store.Name));
            return true;
        }

        private void DeleteNewMail(object arg)
        {
            if (!(arg is string)) return;
            RDOMail mail = null;
            try
            {
                mail = Store.GetMessageFromID(Convert.ToString(arg));
                mail.Delete();
            }
            catch (Exception ex)
            {
                Logger.Warning("DeleteNewMail",ex.Message);
            }
            finally
            {
                Utils.ReleaseObject(mail);
            }
        }
        
        private void LaunchInitialSearch()
        {
            const string SOURCE = CLASS_NAME + "LaunchInitialSearch";
            RDOFolder searchRoot = null;
            RDOFolder ipmRoot = null;
            try
            {
                Logger.Verbose("InitSearchFolder", "initiating search in " + Store.Name);
                //get the SearchFolders collection
                searchRoot = Store.SearchRootFolder;

                var folders = searchRoot.Folders;
                //look for existing folder
                const string NAME = "ECS Message Update";
                _searchFolder = (RDOSearchFolder)folders[NAME];
                if (_searchFolder != null)
                {
                    Logger.Verbose(SOURCE, "found existing search folder for " + Store.Name);
                }
                else
                {
                    //create the search folder                     
                    Logger.Verbose(SOURCE, "creating new search folder for " + Store.Name);
                    _searchFolder = folders.AddSearchFolder(NAME);
                }
                if (_searchFolder == null)
                {
                    Logger.Error(SOURCE, "unable to get or create search folder " + Store.Name);
                    return;
                }
                //make sure it's initialized correctly
                ipmRoot = Store.IPMRootFolder;
                //set the criteria
                var criteria = _searchFolder.SearchCriteria;
                var rstrAnd = (RestrictionAnd)criteria.SetKind(RestrictionKind.RES_AND);
                //subject
                var rstrSubject = (RestrictionContent)rstrAnd.Add(RestrictionKind.RES_CONTENT);
                rstrSubject.lpProp = ThisAddIn.UPDATE_SUBJECT;
                rstrSubject.ulPropTag = (int)MAPITags.PR_SUBJECT;
                rstrSubject.ulFuzzyLevel = ContentFuzzyLevel.FL_IGNORECASE;
                //unread
                var rstrUnread = (RestrictionBitmask) rstrAnd.Add(RestrictionKind.RES_BITMASK);
                rstrUnread.relBMR = BitmaskBMR.BMR_EQZ;
                rstrUnread.ulMask = 1; //MSGFLAG_READ
                rstrUnread.ulPropTag = (int) MAPITags.PR_MESSAGE_FLAGS;
                //set the search container as the mailbox
                _searchFolder.SearchContainers.Add(ipmRoot);
                _searchFolder.IsRecursiveSearch = true;
                //set up a handler
                _searchFolder.OnSearchComplete +=
                    OnSearchComplete;
                _searchFolder.Start();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                Utils.ReleaseObject(searchRoot);
                Utils.ReleaseObject(ipmRoot);
            }
        }

        private void OnSearchComplete()
        {
            const string SOURCE = CLASS_NAME + "OnSearchComplete";
            try
            {
                Logger.Info(SOURCE, "fired in " + Store.Name);
                //kill it
                _searchFolder.Stop();                
                var items = _searchFolder.Items;
                Logger.Info(SOURCE, string.Format(
                    "search returned {0} items",
                    items.Count));
                if (items.Count < 1) return;
                var ids = (from RDOMail item in items 
                           where IsUpdateMessage(item) 
                           select item.EntryID).ToList();
                //delete matching items
                if (ids.Count == 0) return;
                foreach (var item in ids.
                    Select(id => Store.GetMessageFromID(id)))
                {
                    item.Delete();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                //release this listener
                _searchFolder.OnSearchComplete -= OnSearchComplete;
            }
        }
    }
}
