using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;

namespace ChiaraMail
{
    internal class UserRegistration
    {
        private readonly DataTable _table;
        internal UserRegistration()
        {
            //load serialized data if it exists
            var path = FilePath;
            if (File.Exists(path))
            {
                try
                {
                    var ds = new DataSet();
                    ds.ReadXml(path);
                    if(ds.Tables.Count>0)
                    {
                        _table = ds.Tables["UserRegistration"];
                        if (_table.PrimaryKey.Length == 0)
                        {
                            _table.PrimaryKey = new[]
                                                    {
                                                        _table.Columns["Email"],
                                                        _table.Columns["Account"]
                                                    };
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {       
                    Logger.Error("UserRegistration",ex.ToString());
                }
            }          
            
            //init empty table           
            _table = new DataTable("UserRegistration");
            var clm1 = _table.Columns.Add("Email", typeof (string));
            var clm2 = _table.Columns.Add("Account", typeof(string));
            _table.PrimaryKey = new[] {clm1,clm2};
            _table.Columns.Add("Registered", typeof (bool));
            _table.Columns.Add("LastCheck", typeof (DateTime));
        }

        //public bool CheckUser(Account account, string email)
        //{
        //    var changed = false;
        //    var row = _table.Rows.Find(new object[]{email, account.smtpAddress});
        //    if (row != null && Convert.ToDateTime(row["LastCheck"]) >= DateTime.Now.AddHours(-8)) return false;
        //    string registered = ContentHandler.CheckRegistered(account, email);
        //    if (registered != null)
        //    {
        //        if (row != null)
        //        {
        //            changed = row.Field<bool>("Registered") != registered;
        //            row["Registered"] = registered;
        //            row["LastCheck"] = DateTime.Now;
        //        }
        //        else
        //        {
        //            _table.Rows.Add(new object[] {email, account.smtpAddress, registered, DateTime.Now});
        //            changed = true;
        //        }
        //        _table.AcceptChanges();
        //        Save();
        //    }
        //    return changed;
        //}

        public string GetRegistrationFilter(string accountAddress)
        {
            var rows = _table.Select("[Registered]=True");
            if (rows.Length == 0) return "";
            var list = rows.Select(row => 
                string.Format("\"http://schemas.microsoft.com/mapi/proptag/0x0C1F001E\" LIKE '{0}'",
                row.Field<string>("Email"))).ToList();
            return string.Join(" OR ", list);
        }

        private void Save()
        {
            //serialize
            _table.DataSet.WriteXml(FilePath);
        }

        private string FilePath
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                    "ChiaraMail", "Registration.xml");
            }
        }
    }
}
