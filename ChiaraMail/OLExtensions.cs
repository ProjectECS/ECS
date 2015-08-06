using Microsoft.Office.Interop.Outlook;

namespace ChiaraMail
{
    internal static class OLExtensions
    {
        public static string GetPrimarySMTP(this Recipient recip)
        {
            var ae = recip.AddressEntry;
            return ae != null
                ? ae.GetPrimarySMTP()
                : null;
        }

        public static string GetPrimarySMTP(this AddressEntry ae)
        {
            if (ae.AddressEntryUserType == OlAddressEntryUserType.olExchangeUserAddressEntry)
            {
                var exUser = ae.GetExchangeUser();
                return exUser.PrimarySmtpAddress;
            }
            switch (ae.Type)
            {
                case "SMTP":
                    return ae.Address;
                case "EX":
                    //X500 address but not EX address entry - force resolution
                    var session = ae.Session;
                    var recip = session.CreateRecipient(ae.Address);
                    recip.Resolve();
                    var ae2 = recip.AddressEntry;
                    if (ae2.AddressEntryUserType != OlAddressEntryUserType.olExchangeUserAddressEntry)
                        return null;
                    var exUser = ae2.GetExchangeUser();
                    return exUser.PrimarySmtpAddress;
                default:
                    return null;
            }
        }

        public static string InternetAccountName(this MailItem mail)
        {
            var pa = mail.PropertyAccessor;
            string value = "";
            try
            {
                //prop may not have value
                value = pa.GetProperty(ThisAddIn.DASL_INTERNET_ACCOUNT_NAME);
                if (!string.IsNullOrEmpty(value)) return value;
            }
            catch
            {
                value = "";
            }            
            try
            {
                //prop may not exist
                value = pa.GetProperty(ThisAddIn.DASL_DELIVERED_TO);
                return value;  
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
