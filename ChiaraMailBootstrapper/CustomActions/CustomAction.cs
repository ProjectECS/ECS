using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Deployment.WindowsInstaller;
using System.Windows.Forms;

namespace ChiaraMail.CustomActions
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult EvalAllUsers(Session session)
        {
            try
            {
                var installAllUsers = session["INSTALLALLUSERS"];
                if (installAllUsers == "1")
                {
                    session.Log("EvalAllusers: INSTALLALLUSERS = '1', setting ALLUSERS = '1'");
                    session["ALLUSERS"] = "1";
                }                
            }
            catch (Exception ex)
            {
                session.Log("EvalAllUsers error: {0}", ex);
            }
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult FindEcsProductCode(Session session)
        {
            const string ECS = "{310393F9-CA6F-42B8-A6C7-9FB283815208}";
            const string ECS64 = "{BD3F6AEF-94AA-40E8-87BD-2C76525E21AC}";
            try
            {
                var ecs32 = ProductInstallation.GetRelatedProducts(ECS).ToList();
                if (ecs32.Count > 0)
                {
                    session.Log("found {0} installs for ECS", ecs32.Count);
                    //session["ECSINSTALLED"] = ecs32[0].ProductCode;
                    ThreadPool.QueueUserWorkItem(UninstallEcs, ecs32[0].ProductCode);
                    return ActionResult.Success;
                    //foreach (var productInstallation in ecs32)
                    //{
                    //    AddUpgradeRecord(productInstallation,ECS,session);
                    //}
                }
                var ecs64 = ProductInstallation.GetRelatedProducts(ECS64).ToList();
                if (ecs64.Count > 0)
                {
                    session.Log("found {0} installs for ECS64", ecs64.Count);
                    //session["ECSINSTALLED"] = ecs64[0].ProductCode;
                    ThreadPool.QueueUserWorkItem(UninstallEcs, ecs32[0].ProductCode);
                    //foreach (var productInstallation in ecs32)
                    //{
                    //    AddUpgradeRecord(productInstallation, ECS, session);
                    //}
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                session.Log("FindEcsProductCode error: {0}", ex);
                return ActionResult.Success;
            }
        }

        private static void UninstallEcs(object arg)
        {
            var pi = new ProcessStartInfo
                {
                    Arguments = string.Format("/x {0}",arg),
                    FileName = "msiexec.exe"
                };
            Process.Start(pi);
        }

        [CustomAction]
        public static ActionResult PromptToCloseOutlook(Session session)
        {
            session.Log("Detecting running instances of Microsoft Outlook...");
            
            if (null != Process.GetProcessesByName("outlook").FirstOrDefault())
            {
                session.Log("Microsoft Outlook is running.");

                var record = new Record
                {
                    FormatString = "Please exit Microsoft Outlook before continuing\n" + 
                    "or click Retry to close it automatically."
                };

                var result = session.Message(
                    InstallMessage.Error | (InstallMessage)MessageBoxIcon.Error |
                    (InstallMessage)MessageBoxButtons.AbortRetryIgnore, record);

                if (result == MessageResult.Abort)
                {
                    session.Log("User chose to abort the installer.");
                    return ActionResult.Failure;
                }
                if (result == MessageResult.Ignore)
                {
                    session.Log("User chose to ignore.");
                    record.FormatString = "This application will not be available until you restart Outlook.";
                    session.Message(
                    InstallMessage.Error | (InstallMessage)MessageBoxIcon.Exclamation |
                    (InstallMessage)MessageBoxButtons.OK, record);
                    return ActionResult.Success;
                }
                //check to see if it's still running
                var outlook = Process.GetProcessesByName("outlook").FirstOrDefault();
                if (outlook == null)
                {
                    session.Log("User closed Outlook");
                    return ActionResult.Success;
                }
                session.Log("User clicked Retry but Outlook is still running, attempting to kill it.");
                try
                {
                    outlook.Kill();
                }
                catch
                {
                    session.Log("Failed to kill Outlook, raising alert and returning failure.");
                    record = new Record
                    {
                        FormatString = "Outlook is still running. Open the Task Manager, end any open Outlook processes, and try this install again."
                    };
                    session.Message(
                        InstallMessage.Error | (InstallMessage)MessageBoxIcon.Error, record);
                    return ActionResult.Failure;
                }
            }
            return ActionResult.Success;
        }

        //private static void AddUpgradeRecord(ProductInstallation product, string upgradeCode, Session session)
        //{
        //    var upgradeView = session.Database.OpenView("SELECT * FROM Upgrade");
        //    upgradeView.Execute();

        //    var record = session.Database.CreateRecord(7);
        //    record.SetString("UpgradeCode",upgradeCode);
        //    record.SetString("VersionMin",product.ProductVersion.ToString());
        //    record.SetString("VersionMax", product.ProductVersion.ToString());
        //    record.SetNullableInteger("Language", null);
        //    record.SetInteger("Attributes",256);
        //    record.SetString("Remove","");
        //    record.SetString("ActionProperty",product.ProductCode);
        //    upgradeView.Modify(ViewModifyMode.InsertTemporary, record);
        //    upgradeView.Close();
        //}
    }
}
