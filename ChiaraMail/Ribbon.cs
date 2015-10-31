using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ChiaraMail.FormRegions;
using ChiaraMail.Properties;
using Redemption;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Drawing;

namespace ChiaraMail
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI _ribbon;

        //public Ribbon()
        //{
        //}

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonId)
        {
            switch (ribbonId)
            {
                case "Microsoft.Outlook.Explorer":
                    return ThisAddIn.AppVersion < 15 
                        ? Resources.ExplorerRibbon
                        : Resources.ExplorerRibbon2013;
                case "Microsoft.Outlook.Mail.Compose":
                    return ThisAddIn.AppVersion < 14 
                        ? Resources.InspectorRibbonCompose 
                        : Resources.InspectorRibbonCompose2010;
                case "Microsoft.Outlook.Mail.Read":
                    return ThisAddIn.AppVersion < 14 
                        ? Resources.InspectorRibbonRead 
                        : Resources.InspectorRibbonRead2010;
                default:
                    return string.Empty;
            }
            
        }

        #endregion

        #region Ribbon Callbacks

        public void RibbonLoad(Office.IRibbonUI ribbonUI)
        {
            _ribbon = ribbonUI;
        }

        public Bitmap GetImage(Office.IRibbonControl control)
        {
            switch (control.Id)
            {
                case "toggleDynamic":
                case "toggleDynamicEx":
                case "toggleAllowForwarding":
                case "buttonChiaraMailMenu":
                case "buttonChiaraMailAccount":
                    return Resources.ChiaraMailIcon;
                case "buttonEditContent":
                    return Resources.edit_content;
                case "buttonDeleteContent":
                    var img = Resources.delete_content;
                    img.MakeTransparent();
                    return img;
                case "spaceAvailable":
                case "spaceAvailableEx":
                    Account acct = ThisAddIn.FindMatchingAccount(Globals.ThisAddIn.ActiveAccount.SMTPAddress);
                    string strSpaceUsedAndDiskQuota = acct.Storage;

                    if (!string.IsNullOrEmpty(strSpaceUsedAndDiskQuota))
                    {
                        string[] duParms = strSpaceUsedAndDiskQuota.Split(' ');

                        if (duParms.Length >= 2)
                        {
                            long spaceUsed = Convert.ToInt64(duParms[0]);
                            long diskQuota = Convert.ToInt64(duParms[1]);
                            long lngSpaceAvailable = diskQuota - spaceUsed;
                            double lngPercentageUsed = (Convert.ToDouble(spaceUsed) / Convert.ToDouble(diskQuota));

                            var imgSpaceAvailable = Utils.GetImage(lngSpaceAvailable, lngPercentageUsed);
                            imgSpaceAvailable.MakeTransparent();
                            return imgSpaceAvailable;
                        }
                    }

                    return null;
                default:
                    return null;
            }
        }

        public string GetLabel(Office.IRibbonControl control)
        {
            Logger.Info("GetLabel", control.Id);
            switch (control.Id)
            {
                case "spaceAvailable":
                case "spaceAvailableEx":
                    return Resources.label_available_storage;
                case "toggleDynamic":
                case "toggleDynamicEx":
                    return Resources.label_dynamic_content;
                case "toggleEncrypted":
                case "toggleEncryptedEx":
                    return Resources.label_encrypted;
                case "toggleNoPlaceholder":
                case "toggleNoPlaceholderEx":
                    return Resources.label_no_placeholder;
                case "toggleAllowForwarding":
                case "toggleAllowForwardingEx":
                    return Resources.label_allow_forwarding;
                case "buttonChiaraMailAccount":
                    return Resources.label_account_settings;
                case "buttonChiaraMailMenu":
                    return Resources.label_config_button;
                case "buttonInviteECS":
                    return Resources.screentip_invite;
                case "taskChiaraMailHelp":
                    return Resources.label_help_backstage;
                case "taskChiaraMailSupport":
                    return Resources.label_support;
                case"groupChiaraMailHelp":
                    return Resources.label_help_group;
                case "buttonEditContent":
                    return Resources.label_edit_content;
                case "buttonDeleteContent":
                    return Resources.label_delete_content;
                case "groupChiaraAccount":
                    return Resources.label_config_group;
                case "groupChiaraMail":
                    return Resources.label_group_name;
                case "groupChiaraMailRead":
                    return Resources.ecs;
            }
            return "";
        }

        public string GetDescription(Office.IRibbonControl control)
        {
            switch (control.Id)
            {
                case "spaceAvailable":
                case "spaceAvailableEx":
                    return Resources.description_available_storage;
                case "toggleDynamic":
                case "toggleDynamicEx":
                    return  Resources.description_dynamic_content;
                case "toggleEncrypted":
                case "toggleEncryptedEx":
                    return  Resources.description_encrypted;
                case "toggleAllowForwarding":
                case "toggleAllowForwardingEx":
                    return Resources.description_allow_forwarding;
                case "toggleNoPlaceholder":
                case "toggleNoPlaceholderEx":
                    return Resources.description_no_placeholder;
                case "buttonEditContent":
                    return  Resources.description_edit_content;
                case "buttonDeleteContent":
                    return Resources.description_delete_content;
                case "buttonChiaraMailAccount":
                case "groupChiaraAccount":
                    return  Resources.tooltip_config_button;
                case "taskChiaraMailHelp":
                    return  Resources.description_help;
                case "taskChiaraMailSupport":
                    return Resources.description_support;
            }
            return "";
        }

        public string GetScreentip(Office.IRibbonControl control)
        {          
            switch (control.Id)
            {
                case "spaceAvailable":
                case "spaceAvailableEx":
                    return Resources.description_available_storage;
                case "toggleDynamic":
                case "toggleDynamicEx":
                    return  Resources.screentip_dynamic_content;
                case "toggleEncrypted":
                case "toggleEncryptedEx":
                    return  Resources.screentip_encrypted;
                case "toggleNoPlaceholder":
                case "toggleNoPlaceholderEx":
                    return Resources.description_no_placeholder;
                case "toggleAllowForwarding":
                case "toggleAllowForwardingEx":
                    return Resources.description_allow_forwarding;
                case "buttonEditContent":
                    return  Resources.screentip_edit_content;
                case "buttonDeleteContent":
                    return Resources.description_delete_content;
                case "buttonChiaraMailAccount":
                case "buttonChiaraMailMenu":
                    return  Resources.tooltip_config_button;
                case "buttonInviteECS":
                    return Resources.screentip_invite;
            }
            return "";
        }
        
        public bool GetPressed(Office.IRibbonControl control)
        {
            var state = false;
            if (control.Id.EndsWith("Ex"))
            {
                var exWrapper = Globals.ThisAddIn.GetActiveExplWrap();
                if (exWrapper == null)
                {
                    Logger.Info("GetPressed", "missing ActiveExplWrap");
                    return false;
                }
                Logger.Info("GetPressed", "found ActiveExplWrap");
                switch (control.Id)
                {
                    case "toggleDynamicEx":
                        state = exWrapper.Dynamic;
                        break;
                    case "toggleEncryptedEx":
                        state = exWrapper.Dynamic && exWrapper.Encrypted;
                        break;
                    case "toggleNoPlaceholderEx":
                        state = exWrapper.Dynamic && exWrapper.NoPlaceholder;
                        break;
                    case "toggleAllowForwardingEx":
                        state = exWrapper.Dynamic && exWrapper.AllowForwarding;
                        break;
                }
                return state;
            }
            var insp = (Outlook.Inspector)control.Context;
            if (insp == null)
            {
                Logger.Info("GetPressed","context != inspector");
                return false;
            }
            try
            {               
                var wrapper = Globals.ThisAddIn.GetInspWrap(insp);
                if (wrapper == null) return false;
                switch (control.Id)
                {
                    case "toggleDynamic":
                        state = wrapper.Dynamic;
                        break;
                    case "toggleEncrypted":
                    case "toggleEncryptedEx":
                        state = wrapper.Dynamic && wrapper.Encrypted;
                        break;
                    case "toggleNoPlaceholder":
                    case "toggleNoPlaceholderEx":
                        state = wrapper.Dynamic && wrapper.NoPlaceholder;
                        break;
                    case "toggleAllowForwarding":
                    case "toggleAllowForwardingEx":
                        state = wrapper.Dynamic && wrapper.AllowForwarding;
                        break;
                }
            }
            finally
            {
                Marshal.ReleaseComObject(insp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            return state;
        }

        public bool GetEnabled(Office.IRibbonControl control)
        {
            if (control.Id.EndsWith("Ex"))
            {
                var exWrapper = Globals.ThisAddIn.GetActiveExplWrap();
                if (exWrapper == null)
                {
                    Logger.Info("GetEnabled", "missing ActiveExplWrap");
                    return false;
                }
                Logger.Info("GetEnabled", "found ActiveExplWrap");
                if (control.Id == "toggleDynamicEx")
                {
                    var mailItem = exWrapper.MailItem;
                    if (mailItem == null)
                    {
                        Logger.Info("GetEnabled", "no MailItem");
                        return false;
                    }
                    var olAccount = mailItem.SendUsingAccount;
                    if (olAccount == null)
                    {
                        Logger.Info("GetEnabled","SendUsingAccount == null");
                        return false;
                    }
                    var smtpAddress = olAccount.SmtpAddress;
                    Logger.Info("GetEnabled","sendUsingAddress: " + smtpAddress);
                    //disable for spoofed address
                    var displayName = olAccount.DisplayName;
                    return !Utils.IsSpoofed(displayName, smtpAddress)
                           && ThisAddIn.AccountHasEnabledConfiguration(smtpAddress);
                }
                // "toggleEncryptedEx":
                //  "toggleNoPlaceholderEx":
                return exWrapper.Dynamic;                
            }
            
            var insp = (Outlook.Inspector)control.Context;
            if (insp == null)
            {
                Logger.Info("GetEnabled", "context != inspector");
                return false;
            }
            
            Outlook.MailItem item = null;
            try
            {
                item = insp.CurrentItem;
                switch (control.Id)
                {
                    case "toggleDynamic":
                        //evaluate the SendUsingAccount to determine if ECS is configured
                        if (item == null) return false;
                        //this could be null in 2K7
                        Outlook.Account olAccount;
                        try
                        {
                            olAccount = item.SendUsingAccount;
                        }
                        catch
                        {
                            olAccount = null;
                        }
                        var smtpAddress = (olAccount != null)
                            ? olAccount.SmtpAddress
                            : Globals.ThisAddIn.ActiveAccount.SMTPAddress;
                        //disable for spoofed address
                        var displayName = (olAccount != null)
                                              ? olAccount.DisplayName
                                              : Globals.ThisAddIn.ActiveAccount.UserName;
                        return !Utils.IsSpoofed(displayName, smtpAddress) 
                            && ThisAddIn.AccountHasEnabledConfiguration(smtpAddress);
                    case "toggleEncrypted":                    
                    case "toggleNoPlaceholder":
                        var wrapper = Globals.ThisAddIn.GetInspWrap(insp);
                        return wrapper != null && wrapper.Dynamic;
                    case "toggleAllowForwarding":
                        if (item == null) return false;
                        
                        var wrapper1 = Globals.ThisAddIn.GetInspWrap(insp);

                        if (wrapper1 != null && wrapper1.Dynamic == false)
                        {
                            return false;
                        }

                        //While compose new, we will always enable “Allow forwarding” button
                        if ((item.CreationTime > DateTime.Now && item.Recipients.Count == 0) || 
                            item.CreationTime <= DateTime.Now)
                        {
                            Win32.AllowForwarding(true);
                            return true;
                        }

                        //While reply/reply all/forward, we will always disable “Allow forwarding” button
                        if (item.CreationTime > DateTime.Now)
                        {
                            Win32.AllowForwarding(ThisAddIn.IsMailAllowForwarding);
                            return false;
                        }

                        return false;
                    case "buttonEditContent":
                    case "buttonDeleteContent":
                        if (item == null) return false;
                        //get the value from the matching formregion
                        if (!item.Sent) return false;
                        var safItem = RedemptionLoader.new_SafeMailItem();
                        safItem.Item = item;
                        var recordKey = Utils.GetRecordKey(safItem);
                        return (from region in Globals.FormRegions 
                                where region.GetType() == typeof (DynamicInspector) 
                                select (DynamicInspector) region into target 
                                where target.RecordKey.Equals(recordKey) 
                                select target.Editable).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GetEnabled", ex.ToString());
            }
            finally
            {
                Marshal.ReleaseComObject(insp);
                if (item != null) Marshal.ReleaseComObject(item);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            return false;
        }

        public void OnPress(Office.IRibbonControl control, bool isPressed)
        {
            if (control.Id.EndsWith("Ex"))
            {
                var exWrapper = Globals.ThisAddIn.GetActiveExplWrap();
                if (exWrapper == null)
                {
                    Logger.Info("OnPress", "missing ActiveExplWrap");
                    return;
                }
                Logger.Info("OnPress", "found ActiveExplWrap");
                switch (control.Id)                    
                {
                    case "toggleDynamicEx":
                        exWrapper.Dynamic = isPressed;
                        if (!isPressed && exWrapper.Encrypted)
                        {
                            //clear Encrypted
                            exWrapper.Encrypted = false;

                        }
                        //enabled/disabled status of Encrypted and NoPlaceholder are governed by Dynamic
                        _ribbon.InvalidateControl("toggleEncryptedEx");
                        _ribbon.InvalidateControl("toggleNoPlaceholderEx");
                        break;
                    case "toggleEncryptedEx":
                        if (exWrapper.Dynamic)
                        {
                            exWrapper.Encrypted = isPressed;
                        }
                        else
                        {
                            //disallow the press
                            _ribbon.InvalidateControl(control.Id);
                        }
                        break;
                    case "toggleNoPlaceholderEx":
                        if (exWrapper.Dynamic)
                        {
                            exWrapper.NoPlaceholder = isPressed;
                        }
                        else
                        {
                            //disallow the press
                            _ribbon.InvalidateControl(control.Id);
                        }
                        break;
                    case "toggleAllowForwarding":
                        if (exWrapper.Dynamic)
                        {
                            exWrapper.AllowForwarding = isPressed;
                        }
                        else
                        {
                            //disallow the press
                            _ribbon.InvalidateControl(control.Id);
                        }
                        break;
                }
                return;
            }
            var insp = (Outlook.Inspector)control.Context;
            if (insp == null) return;
            try
            {                                
                var wrapper = Globals.ThisAddIn.GetInspWrap(insp);
                if (wrapper == null) return;
                switch (control.Id)
                {
                    case "toggleDynamic":
                        wrapper.Dynamic = isPressed;
                        if (!isPressed && wrapper.Encrypted)
                        {
                            //clear Encrypted
                            wrapper.Encrypted = false;                           
                        }
                        //enabled/disabled status of Encrypted is governed by Dynamic
                        _ribbon.InvalidateControl("toggleEncrypted");
                        _ribbon.InvalidateControl("toggleNoPlaceholder");
                        _ribbon.InvalidateControl("toggleAllowForwarding");
                        break;
                    case "toggleEncrypted":
                        if (wrapper.Dynamic)
                        {
                            wrapper.Encrypted = isPressed;
                        }
                        else
                        {
                            //disallow the press
                            _ribbon.InvalidateControl(control.Id);
                        }
                        break;
                    case "toggleNoPlaceholder":
                        if (wrapper.Dynamic)
                        {
                            wrapper.NoPlaceholder = isPressed;
                        }
                        else
                        {
                            //disallow the press
                            _ribbon.InvalidateControl(control.Id);
                        }
                        break;
                    case "toggleAllowForwarding":
                        if (wrapper.Dynamic)
                        {
                            wrapper.AllowForwarding = isPressed;
                        }
                        else
                        {
                            //disallow the press
                            _ribbon.InvalidateControl(control.Id);
                        }
                        break;
                }
            }
            finally
            {
                Marshal.ReleaseComObject(insp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        public void OnClick(Office.IRibbonControl control)
        {
            Outlook.Inspector insp = null; 
            try
            {
                switch (control.Id)
                {
                    case "buttonChiaraMailAccount":
                        Globals.ThisAddIn.ShowConfig();
                        break;
                    case "buttonInviteECS":
                        Globals.ThisAddIn.CreateInvite();
                        break;
                    case "taskChiaraMailHelp":
                        Utils.OpenHelp();
                        break;
                    case "taskChiaraMailSupport":
                        Globals.ThisAddIn.RequestSupport();
                        break;
                    case "buttonEditContent":
                        insp = (Outlook.Inspector)control.Context;
                        if (insp == null) return;
                        //tell the region to save the change
                        DynamicInspector region = Globals.FormRegions[insp].DynamicInspector;
                        if (region == null) return;
                        try
                        {
                            Utils.SetCursor(Cursors.WaitCursor.Handle);
                            region.SaveChanges();
                        }
                        finally
                        {
                            Utils.SetCursor(Cursors.Default.Handle);
                        }
                        break;
                    case "buttonDeleteContent":
                        insp = (Outlook.Inspector)control.Context;
                        if (insp == null) return;
                        //tell the region to save the change
                        region = Globals.FormRegions[insp].DynamicInspector;
                        if (region == null) return;
                        try
                        {
                            Utils.SetCursor(Cursors.WaitCursor.Handle);
                            region.DeleteContent();
                        }
                        finally
                        {
                            Utils.SetCursor(Cursors.Default.Handle);
                        }
                        break;
                }
            }
            finally
            {
                if (insp != null)
                {
                    Marshal.ReleaseComObject(insp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
        }

        public void ResetInspButtons()
        {
            if(_ribbon == null) return;
            _ribbon.InvalidateControl("spaceAvailable");
            _ribbon.InvalidateControl("spaceAvailableEx");
            _ribbon.InvalidateControl("toggleDynamic");
            _ribbon.InvalidateControl("toggleDynamicEx");
            _ribbon.InvalidateControl("toggleEncrypted");
            _ribbon.InvalidateControl("toggleNoPlaceholder");
            _ribbon.InvalidateControl("toggleDynamicEx");
            _ribbon.InvalidateControl("toggleEncryptedEx");
            _ribbon.InvalidateControl("toggleNoPlaceholderEx");
            _ribbon.InvalidateControl("toggleAllowForwarding");
        }
        #endregion

    }
}
