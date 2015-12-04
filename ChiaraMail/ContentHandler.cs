using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Threading;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;

namespace ChiaraMail
{
    internal class ContentHandler
    {
        private const string CLASS_NAME = "ContentHandler.";
        private static ManualResetEvent _callbackDone;

        #region Shared methods
        
        internal static void PostContent(string smtpAddress, EcsConfiguration configuration, string encodedContent, 
            string recips, ref string pointer, out string error)
        {
            const string SOURCE = CLASS_NAME + "PostContent";
            try
            {
                //1 MB is the max for RECEIVE CONTENT, so split into chunks         
                var chunks = SegmentContent(encodedContent);
                //assemble the post
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}%20{2}",
                    "RECEIVE%20CONTENT", HttpUtility.UrlEncode(recips), chunks[0]);
                var postData = Encoding.UTF8.GetBytes(post);
                                
                var request = CreateRequest(configuration.Server, configuration.Port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE,"HttpWebRequest is null");
                    error = "Internal error";
                    return;
                }
                request.Timeout = 30000;
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE,"HaveResponse == false");
                    error = "Internal error";
                    request.Abort();
                    return;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response == null");
                    error = "Internal error";
                    request.Abort();
                    return;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    error = "Internal error";
                    request.Abort();
                    return;
                }
                var reader = new StreamReader(stream);
                var responseText = reader.ReadToEnd();
                ParsePostResponse(responseText, ref pointer, out error);
                if (chunks.Count <= 1 || string.IsNullOrEmpty(pointer)) return;
                for(var j = 1; j< chunks.Count; j++)
                {
                    Logger.Info(SOURCE,string.Format("sending segment #{0} of {1} segments",
                                                     j + 1, chunks.Count));
                    ReceiveSegment(smtpAddress,configuration,chunks[j],pointer,out error);
                    if (error == "success") continue;
                    Logger.Error(SOURCE,string.Format("exiting on error at chunk #{0}: {1}",
                                                      j + 1, error));
                    break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        internal static void UpdateContent(string smtpAddress, EcsConfiguration configuration, string encodedContent,
            string id, out string error)
        {
            const string SOURCE = CLASS_NAME + "UpdateContent";
            try
            {
                //1 MB is the max for RECEIVE CONTENT, so split into chunks         
                var chunks = SegmentContent(encodedContent);
                //assemble the post
                string post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}%20{2}",
                    "UPDATE%20CONTENT", id, chunks[0]);
                var postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequest(configuration.Server, configuration.Port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "HttpWebRequest is null");
                    error = "Internal error";
                    return;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "HaveResponse == false");
                    request.Abort();
                    error = "Internal error";
                    return;                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response == null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var reader = new StreamReader(stream);
                var responseText = reader.ReadToEnd();
                error = ParseUpdateResponse(responseText);
                if (chunks.Count <= 1 || !string.IsNullOrEmpty(error)) return;
                for (var j = 1; j < chunks.Count; j++)
                {
                    Logger.Info(SOURCE, string.Format("sending segment #{0} of {1} segments",
                                                     j + 1, chunks.Count));
                    ReceiveSegment(smtpAddress, configuration, chunks[j], id, out error);
                    if (error == "success") continue;
                    Logger.Error(SOURCE, string.Format("exiting on error at chunk #{0}: {1}",
                                                       j + 1, error));
                    break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        internal static void FetchContent(string smtpAddress,EcsConfiguration configuration, string senderAddress, string id, 
            string server, string port, bool returnRaw, out string content, out string error)
        {
            const string SOURCE = CLASS_NAME + "FetchContent";
            content = "";
            try
            {
                DateTime dtStartDownloadTime = DateTime.Now; 
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1} {2}",
                    "FETCH%20CONTENT",
                    senderAddress,
                    id);
                byte[] postData = Encoding.UTF8.GetBytes(post);
                if (string.IsNullOrEmpty(server)) server = configuration.Server;
                if (string.IsNullOrEmpty(port)) port = configuration.Port;
                HttpWebRequest request = CreateRequest(server, port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE,"failed to create HttpWebRequest object");
                    error = "Internal error";
                    return;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE,"server did not respond");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE,"response is null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var reader = new StreamReader(stream);
                string responseText = reader.ReadToEnd();
                ParseFetchResponse(responseText, returnRaw, ref content, out error);
                DateTime dtEndDownloadTime = DateTime.Now;

                //if DownloadSpeed is 0 then it means we are going to measure Download speed and then storing for future use.
                if (Properties.Settings.Default.DownloadSpeed == 0)
                {
                    double sizeInKb = content.Length / 1024;
                    TimeSpan ts = dtEndDownloadTime.Subtract(dtStartDownloadTime);
                    double speed = sizeInKb / ts.TotalSeconds;

                    Properties.Settings.Default.DownloadSpeed = speed;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        internal static void FetchSegment(string smtpAddress, EcsConfiguration configuration, string senderAddress, string id,
            string server, string port, bool returnRaw, string filePointer, out string content, out string error)
        {
            const string SOURCE = CLASS_NAME + "FetchSegment";
            content = "";
            try
            {
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1} {2} {3}",
                    "FETCH%20SEGMENT",
                    senderAddress,
                    id,
                    filePointer);
                byte[] postData = Encoding.UTF8.GetBytes(post);
                if (string.IsNullOrEmpty(server)) server = configuration.Server;
                if (string.IsNullOrEmpty(port)) port = configuration.Port;
                HttpWebRequest request = CreateRequest(server, port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "failed to create HttpWebRequest object");
                    error = "Internal error";
                    return;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "server did not respond");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response is null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var reader = new StreamReader(stream);
                string responseText = reader.ReadToEnd();

                ParseFetchSegmentResponse(responseText, returnRaw, ref content, out error);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        internal static void ReceiveSegment(string smtpAddress, EcsConfiguration configuration, string segment,
            string id, out string error)
        {
            const string SOURCE = CLASS_NAME + "ReceiveSegment";
            try
            {
                //assemble the post
                string post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}%20{2}",
                    "RECEIVE%20SEGMENT", id, segment);
                var postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequest(configuration.Server, configuration.Port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "HttpWebRequest is null");
                    error = "Internal error";
                    return;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "HaveResponse == false");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response == null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var reader = new StreamReader(stream);
                var responseText = reader.ReadToEnd();
                error = ParseSegmentResponse(responseText);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        internal static void DeleteContent(string smtpAddress, EcsConfiguration configuration, 
            string id, out string error, bool deletePointer)
        {
            const string SOURCE = CLASS_NAME + "DeleteContent";
            try
            {
                string post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}",
                    deletePointer ? "DELETE%20CONTENT" : "DELETE%20DATA", id);
                byte[] postData = Encoding.UTF8.GetBytes(post);
                HttpWebRequest request = CreateRequest(configuration.Server, configuration.Port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "failed to create HttpWebRequest object");
                    error = "Internal error";
                    return;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "server did not respond");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response is null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    request.Abort();
                    error = "Internal error";
                    return;
                }
                var reader = new StreamReader(stream);
                var responseText = reader.ReadToEnd();
                error = ParseDeleteResponse(responseText);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        internal static ThisAddIn.RegistrationState CheckRegistered(string smtpAddress, EcsConfiguration configuration, string senderAddresses, out string error)
        {
            const string SOURCE = CLASS_NAME + "CheckRegistered";
            try
            {
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}",
                    "USER%20REGISTERED", senderAddresses);
                var postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequest(configuration.Server, configuration.Port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "failed to create HttpWebRequest object");
                    error = "unable to connect";
                    return ThisAddIn.RegistrationState.Unknown;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "server did not respond");
                    request.Abort();
                    error = "no response";
                    return ThisAddIn.RegistrationState.Unknown;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response is null");
                    error = "no response";
                    return ThisAddIn.RegistrationState.Unknown;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "response stream is null");
                    error = "no response";
                    return ThisAddIn.RegistrationState.Unknown;
                }
                var reader = new StreamReader(stream);
                string responseText = reader.ReadToEnd();
                /* -16 SELECT error in user_registered(), e: 
                *  -10 Invalid number of parameters
                *  -2 Login not complete
                *  7 User registered = 
                */
                if (responseText.StartsWith("7 "))
                {
                    var value = responseText.Replace("7 User registered =", "").Replace(",","").Trim();
                    bool answer;
                    if (bool.TryParse(value, out answer))
                    {
                        error = "";
                        return answer
                                   ? ThisAddIn.RegistrationState.Registered
                                   : ThisAddIn.RegistrationState.NotRegistered;
                    }
                    error = value;
                    return ThisAddIn.RegistrationState.ServerError;
                }
                Logger.Warning(SOURCE, responseText);
                string code;
                EvalResponse(responseText, out code, out error);
                return code == "-1"
                    ? ThisAddIn.RegistrationState.BadCredentials 
                    : ThisAddIn.RegistrationState.ServerError;
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "internal error";
                return ThisAddIn.RegistrationState.Unknown;
            }
        }

        internal static string RegisterUser(string emailAddr, string password, string displayName, string protocol,
                                            string host, string port, string loginName, string targetServer, string targetPort)
        {
            const string SOURCE = CLASS_NAME + "RegisterUser";
            try
            {
                var uri = new Uri(string.Format("https://{0}:{1}/RegisterUser",
                    targetServer, targetPort));
                //var uri = new Uri("https://www.chiaramail.com/RegisterUser");
                var request = WebRequest.Create(uri) as HttpWebRequest;
                if (request == null)
                {
                    //log an error 
                    Logger.Error(SOURCE, "failed to create Register request");
                    return string.Empty;
                }
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                string post = string.Format(
                    "email_addr={0}&password={1}&username={2}&city=ECSMail user&zipcode=00000&country={3}&mail.IMAP.host={4}&mail.IMAP.user={5}&mail.IMAP.port={6}&mail.store.protocol={7}",
                    HttpUtility.UrlEncode(emailAddr),
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(password)),
                    HttpUtility.UrlEncode(displayName),
                    "US",
                    host,
                    loginName,
                    port,
                    protocol);
                request.ContentLength = post.Length;
                byte[] postData = Encoding.UTF8.GetBytes(post);
                request.UserAgent = "outlook";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var requestStream = request.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (!request.HaveResponse || response == null)
                    {
                        Logger.Error(SOURCE, "no response");
                        request.Abort();
                        return "Error: no response";
                    }
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream == null)
                        {
                            Logger.Error(SOURCE, "ResponseStream is null");
                            request.Abort();
                            return "Error: no response";
                        }
                        using (var reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(SOURCE, "Exception when sending registration request to server: " + e);
            }
            return string.Empty;
        }

        //internal static bool RegisterUser(string emailAddr, string userName, string address)
        //{
        //    string source = _className + "RegisterUser";
        //    try
        //    {
        //        var uri = new Uri("https://www.chiaramail.com/Register");
        //        var request = WebRequest.Create(uri) as HttpWebRequest;
        //        if (request == null)
        //        {
        //            //log an error 
        //            Logger.Error(source, "failed to create Register request");
        //            return false;
        //        }
        //        request.Method = "POST";
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        string post = string.Format(
        //            "email_addr={0}&addr={1}&username={2}&city=ECSMail user&zipcode=00000&country=United States",
        //            HttpUtility.UrlEncode(emailAddr),
        //            HttpUtility.UrlEncode(address), 
        //            userName);
        //        request.ContentLength = post.Length;
        //        byte[] postData = Encoding.UTF8.GetBytes(post);
        //        request.UserAgent = "outlook";
        //        var requestStream = request.GetRequestStream();
        //        requestStream.Write(postData, 0, postData.Length);
        //        requestStream.Close();
        //        var response = request.GetResponse();
        //        response.Close();
        //        //we don't care about the response
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(source, "Exception when sending registration request to server: " + e);
        //    }
        //    return false;
        //}
   
        internal static bool AddRecipients(string smtpAddress, EcsConfiguration configuration, string senderAddress, string id,
            string server, string port, string newRecips, out string error)
        {
            const string SOURCE = CLASS_NAME + "AddRecipients";
            try
            {
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1} {2} {3}",
                    "ADD%20RECIPIENTS",
                    senderAddress,
                    id,
                    newRecips);
                byte[] postData = Encoding.UTF8.GetBytes(post);
                if (string.IsNullOrEmpty(server)) server = configuration.Server;
                if (string.IsNullOrEmpty(port)) port = configuration.Port;
                HttpWebRequest request = CreateRequest(server, port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "failed to create HttpWebRequest object");
                    error = "Internal error";
                    return false;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "server did not respond");
                    request.Abort();
                    error = "Internal error";
                    return false;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response is null");
                    request.Abort();
                    error = "Internal error";
                    return false;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    request.Abort();
                    error = "Internal error";
                    return false;
                }
                var reader = new StreamReader(stream);
                string responseText = reader.ReadToEnd();
                error = ParseAddRecipsResponse(responseText);
                return string.IsNullOrEmpty(error);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
                return false;
            }
        }

        internal static bool RemoveRecipient(string smtpAddress, EcsConfiguration configuration, string senderAddress, string id,
            string server, string port, out string error)
        {
            const string SOURCE = CLASS_NAME + "RemoveRecipient";
            try
            {
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1} {2}",
                    "REMOVE%20RECIPIENT",
                    senderAddress,
                    id);
                byte[] postData = Encoding.UTF8.GetBytes(post);
                if (string.IsNullOrEmpty(server)) server = configuration.Server;
                if (string.IsNullOrEmpty(port)) port = configuration.Port;
                HttpWebRequest request = CreateRequest(server, port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "failed to create HttpWebRequest object");
                    error = "Internal error";
                    return false;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "server did not respond");
                    request.Abort();
                    error = "Internal error";
                    return false;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response is null");
                    request.Abort();
                    error = "Internal error";
                    return false;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "ResponseStream == null");
                    request.Abort();
                    error = "Internal error";
                    return false;
                }
                var reader = new StreamReader(stream);
                string responseText = reader.ReadToEnd();
                error = ParseRemoveRecipResponse(responseText);
                return string.IsNullOrEmpty(error);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
                return false;
            }
        }

        internal static ThisAddIn.LicenseState CheckLicensed(string smtpAddress, EcsConfiguration configuration, string serverAddress)
        {
            const string SOURCE = CLASS_NAME + "CheckLicensed";
            try
            {
                string post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}",
                    "SERVER%20LICENSED", serverAddress);
                byte[] postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequest(configuration.Server, configuration.Port, postData);
                if (request == null)
                {
                    Logger.Error(SOURCE, "failed to create HttpWebRequest object");
                    return ThisAddIn.LicenseState.Unknown;
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    Logger.Error(SOURCE, "server did not respond");
                    request.Abort();
                    return ThisAddIn.LicenseState.Unknown;
                }
                if (response == null)
                {
                    Logger.Error(SOURCE, "response is null");
                    return ThisAddIn.LicenseState.Unknown;
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    Logger.Error(SOURCE, "response stream is null");
                    return ThisAddIn.LicenseState.Unknown;
                }
                var reader = new StreamReader(stream);
                var responseText = reader.ReadToEnd();
                if (string.IsNullOrEmpty(responseText)) return 0;
                /* -27 Missing or expired content sever license: 
                *  -10 Invalid number of parameters
                *  -2 Login not complete
                *  10 Server Licensed 
                */
                var value = responseText.Split(new [] {' '}, 2);
                switch (value[0])
                {
                    case "-27":
                        return ThisAddIn.LicenseState.NotLicensed;
                    case "10":
                        return ThisAddIn.LicenseState.Licensed;
                    default:
                        return ThisAddIn.LicenseState.Unknown;
                }                               
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                return ThisAddIn.LicenseState.Unknown;
            }
        }

        #region Async Methods
        
        internal static void PostContent(string smtpAddress, EcsConfiguration configuration, string encodedContent,
            string recips, ref BackgroundWorker bw, ref DoWorkEventArgs e,
            ref string pointer, out string error)
        {
            const string SOURCE = CLASS_NAME + "PostContent";
            try
            {
                //1 MB is the max for RECEIVE CONTENT, so split into chunks         
                var chunks = SegmentContent(encodedContent);
                DateTime dtStartUploadTime = DateTime.Now;
                //assemble the post
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}%20{2}",
                    "RECEIVE%20CONTENT", HttpUtility.UrlEncode(recips), HttpUtility.UrlEncode(chunks[0]));
                var postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequestAsync(configuration.Server, configuration.Port, postData,
                    ref bw, ref e);
                if (request == null && !e.Cancel && !bw.CancellationPending)
                {
                    Logger.Error(SOURCE, "failed to create WebRequest");
                    error = "Internal error";
                    return;
                }
                GetResponseAsync(request, "ReceiveContent", ref bw, ref e, out pointer, out error);
                DateTime dtEndUploadTime = DateTime.Now;

                //if UploadSpeed is 0 then it means we are going to measure Upload speed and then storing for future use.
                if (Properties.Settings.Default.UploadSpeed == 0)
                {
                    double sizeInKb = (chunks[0].Length * 8) / 1024;
                    TimeSpan ts = dtEndUploadTime.Subtract(dtStartUploadTime);
                    double speed = sizeInKb / ts.TotalSeconds;

                    Properties.Settings.Default.UploadSpeed = speed;
                    Properties.Settings.Default.Save();
                }

                if(chunks.Count <= 1 || string.IsNullOrEmpty(pointer)) return;
                for (var j = 1; j < chunks.Count; j++)
                {
                    if (e.Cancel || bw.CancellationPending) return;                   
                    Logger.Info(SOURCE, string.Format("sending segment #{0} of {1} segments",
                                                     j + 1, chunks.Count));
                    ReceiveSegment(smtpAddress, configuration, chunks[j], pointer, ref bw, ref e, out error);
                    if (error == "success") continue;
                    Logger.Error(SOURCE, string.Format("exiting on error at chunk #{0}: {1}",
                                                      j + 1, error));
                    break;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        internal static void UpdateContent(string smtpAddress, EcsConfiguration configuration, string encodedContent,
            string id, ref BackgroundWorker bw, ref DoWorkEventArgs e, out string error)
        {
            const string SOURCE = CLASS_NAME + "UpdateContent";
            try
            {
                //1 MB is the max for RECEIVE CONTENT, so split into chunks         
                var chunks = SegmentContent(encodedContent);
                //assemble the post
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                    string.Format("{0}&parms={1}%20{2}",
                    "UPDATE%20CONTENT", id, chunks[0]);
                var postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequestAsync(configuration.Server, configuration.Port, postData,
                    ref bw, ref e);
                if (request == null && !e.Cancel && !bw.CancellationPending)
                {
                    Logger.Error(SOURCE, "failed to create WebRequest");
                    error = "Internal error";
                    return;
                }
                string pointer;
                GetResponseAsync(request, "UpdateContent", ref bw, ref e, out pointer, out error);
                if (chunks.Count <= 1 || !string.IsNullOrEmpty(error)) return;
                for (var j = 1; j < chunks.Count; j++)
                {
                    Logger.Info(SOURCE, string.Format("sending segment #{0} of {1} segments",
                                                     j + 1, chunks.Count));
                    ReceiveSegment(smtpAddress, configuration, chunks[j], id, ref bw, ref e, out error);
                    if (error == "success") continue;
                    Logger.Error(SOURCE, string.Format("exiting on error at chunk #{0}: {1}",
                                                       j + 1, error));
                    break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        private static void ReceiveSegment(string smtpAddress, EcsConfiguration configuration, string segment,
            string id, ref BackgroundWorker bw, ref DoWorkEventArgs e, out string error)
        {
            const string SOURCE = CLASS_NAME + "ReceiveSegment";
            try
            {
                //assemble the post
                var post = AssembleLoginParams(smtpAddress, configuration.Password) +
                           string.Format("{0}&parms={1} {2}",
                                         "RECEIVE%20SEGMENT", id, HttpUtility.UrlEncode(segment));

                var postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequestAsync(configuration.Server, configuration.Port, postData,
                                                 ref bw, ref e);
                if (request == null && !e.Cancel && !bw.CancellationPending)
                {
                    Logger.Error(SOURCE, "failed to create WebRequest");
                    error = "Internal error";
                    return;
                }
                string pointer;
                GetResponseAsync(request, "ReceiveSegment", ref bw, ref e, out pointer, out error);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        #endregion

        internal static string EncodeContent(string content, string encryptKey,
            string encryptKey2)
        {
            const string SOURCE = CLASS_NAME + "EncodeContent";
            try
            {
                //get UTF8 bytes
                byte[] buf = Encoding.UTF8.GetBytes(content);
                //encrypted?
                if (!string.IsNullOrEmpty(encryptKey))
                {
                    buf = Encoding.UTF8.GetBytes(
                        Cryptography.EncryptAES(content, encryptKey));
                }
                else if (!string.IsNullOrEmpty(encryptKey2))
                {
                    buf = AES_JS.EnCryptCBC(buf, encryptKey2);
                }
                if (buf == null) return "";
                //convert to base64 
                string base64 = Convert.ToBase64String(buf);
                //UrlEncode (we are posting as application/x-www-form-urlencoded)
                return HttpUtility.UrlEncode(base64);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return "";
        }

        internal static string EncodeAttachBytes(byte[] buf, string encryptKey, string encryptKey2)
        {
            const string SOURCE = CLASS_NAME + "EncodeAttachBytes";
            try
            {
                //optional encryption                               
                if (!string.IsNullOrEmpty(encryptKey))
                {
                    //compress first
                    Compress(ref buf);
                    //then encrypt
                    buf = Cryptography.EncryptAES(buf, encryptKey);
                    //"UTF8" encode to match TB2.0 client
                    buf = Utf8Encode(buf);
                }
                else if (!string.IsNullOrEmpty(encryptKey2))
                {
                    //encrypt
                    buf = AES_JS.EnCryptCBC(buf, encryptKey2);
                    //then compress
                    //Compress(ref buf);
                }

                //convert to base64
                return Convert.ToBase64String(buf);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return "";
        }

        internal static void SaveAttachment(string content, string encryptKey,
            string encryptKey2, string userAgent, string path)
        {
            const string SOURCE = CLASS_NAME + "SaveAttachment";
            try
            {
                string strUrlDecoded = content;

            WithoutUrlDecode:
                byte[] buf = Convert.FromBase64String(strUrlDecoded);
                //optional encryption
                if (!string.IsNullOrEmpty(encryptKey))
                {
                    //decode as "UTF8"
                    buf = Utf8Decode(buf);
                    buf = Cryptography.DecryptAES(buf, encryptKey);
                    //and decompress
                    Decompress(ref buf);
                }
                else if (!string.IsNullOrEmpty(encryptKey2))
                { 
                    //if user-agent field have value then decrypt with CBC mode or decrypt with ECB mode (earlier solution)
                    if (!string.IsNullOrEmpty(userAgent))
                    {
                        //no UTF8 decoding
                        //just decrypt
                        try
                        {
                            buf = AES_JS.DecryptCBC(buf, encryptKey2);
                        }
                        catch (Exception ex)
                        {
                            if (ex.ToString().Contains("Length of the data to decrypt is invalid"))
                            {
                                strUrlDecoded = content;
                                goto WithoutUrlDecode;
                            }
                        }
                    }
                    else
                    {
                        //no UTF8 decoding
                        //just decrypt
                        buf = AES_JS.Decrypt(buf, encryptKey2);
                    }
                }

                if (Utils.IsFileImage(path))
                {
                    using (Image image = Image.FromStream(new MemoryStream(buf)))
                    {
                        image.Save(path, ImageFormat.Png);
                    }
                }
                else
                {
                    File.WriteAllBytes(path, buf);
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal static byte[] GetAttachBytes(string content,
            string encryptKey, string encryptKey2)
        {
            const string SOURCE = CLASS_NAME + "GetAttachBytes";
            try
            {
                byte[] buf = Convert.FromBase64String(content);

                //optional decryption
                if (!string.IsNullOrEmpty(encryptKey))
                {
                    //decode as "UTF8" first    
                    buf = Utf8Decode(buf);
                    //decrypt bytes
                    buf = Cryptography.DecryptAES(buf, encryptKey);
                    //and decompress
                    Decompress(ref buf);
                }
                else if (!string.IsNullOrEmpty(encryptKey2))
                {
                    //no UTF8 decoding
                    //decompress
                    //Decompress(ref buf);
                    //then decrypt
                    buf = AES_JS.Decrypt(buf, encryptKey2);
                }

                return buf;

            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return null;
        }
        
        #endregion

        #region Helper methods

        private static HttpWebRequest CreateRequest(string server, string port, byte[] postData)
        {
            const string SOURCE = CLASS_NAME + "CreateRequest";
            HttpWebRequest request = null;
            try
            {
                if (!server.Contains("://")) server = "https://" + server;
                var route = Properties.Settings.Default.Route;
                if (route != "DynamicContentServer/ContentServer")
                {
                    Logger.Verbose(SOURCE, "submitting request to " + route);
                }
                var uri = new Uri(string.Format("{0}:{1}/{2}",
                    server, port, route));
                request = WebRequest.Create(uri) as HttpWebRequest;
                if (request == null) return null;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                //request.KeepAlive;
                //request.Proxy;
                //request.SendChunked = true;
                request.UserAgent = "outlook";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var requestStream = request.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE,string.Format(
                    "WebRequest.Create failed using '{0}:{1}', ContentLength {2}: {3}",
                    server,port,postData.Length,ex.Message));
            }
            return request;
        }

        private static HttpWebRequest CreateRequestAsync(string server, string port, byte[] postData,
            ref BackgroundWorker bw, ref DoWorkEventArgs e)
        {
            const string SOURCE = CLASS_NAME + "CreateRequestAsync";
            HttpWebRequest request = null;
            try
            {
                if (!server.Contains("://")) server = "https://" + server;
                var route = Properties.Settings.Default.Route;
                if (route != "DynamicContentServer/ContentServer")
                {
                    Logger.Verbose(SOURCE,"submitting request to " + route);
                }
                var uri = new Uri(string.Format("{0}:{1}/{2}",
                    server, port, route));
                request = WebRequest.Create(uri) as HttpWebRequest;
                if (request == null) return null;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;
                request.AllowWriteStreamBuffering = false;
                var state = new RequestState
                {
                    Request = request,
                    Data = postData
                };
                //request.KeepAlive;
                //request.Proxy;
                request.UserAgent = "outlook";
                //use manual reset event to signal when callback is complete
                _callbackDone = new ManualResetEvent(false);
                //initiate callback for the request stream
                var ar = request.BeginGetRequestStream(
                    RequestStreamCallback, state);
                //this will signal when the callback fires, but before the data gets written to the stream
                //poll every 50 ms
                var cycles = 0;
                const int WAIT = 50;
                ar.AsyncWaitHandle.WaitOne(WAIT, true);
                while (ar.IsCompleted != true)
                {
                    cycles++;
                    if (bw.CancellationPending)
                    {
                        request.Abort();
                        e.Cancel = true;
                        Logger.Info(SOURCE,
                            "aborting call for RequestStream in response to CancellationPending flag");
                        break;
                    }
                    if (cycles >= 600)
                    {
                        //enforce timeout of 30 seconds (600 * 50ms)
                        request.Abort();
                        Logger.Info(SOURCE, string.Format(
                            "aborting call for RequestStream after {0} sec timeout",
                            cycles * WAIT / 1000));
                        break;
                    }
                    //wait for another 50 ms
                    ar.AsyncWaitHandle.WaitOne(WAIT, true);
                }
                //wait for the callback to signal completion
                _callbackDone.WaitOne();
                _callbackDone.Close();
                _callbackDone = null;
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, string.Format(
                    "WebRequest.Create failed using '{0}:{1}', ContentLength {2}: {3}",
                    server, port, postData.Length, ex.Message));
            }
            return request;
        }

        private static void GetResponseAsync(HttpWebRequest request, string method,
            ref BackgroundWorker bw, ref DoWorkEventArgs e, out string pointer, out string error)
        {
            const string SOURCE = CLASS_NAME + "GetResponseAsync";
            pointer = "";
            error = "";
            try
            {
                //make async call to get response 
                //then we can abort if Cancel is called on the background worker
                var state = new RequestState
                {
                    Request = request,
                    Method = method
                };
                _callbackDone = new ManualResetEvent(false);
                IAsyncResult ar = request.BeginGetResponse(
                    ResponseCallback, state);
                //poll every 250 ms
                const int WAIT = 250;
                int cycles = 0;
                ar.AsyncWaitHandle.WaitOne(WAIT, true);
                while (ar.IsCompleted != true)
                {
                    cycles += 1;
                    //check for manual/application Cancel
                    if (bw.CancellationPending)
                    {
                        request.Abort();
                        e.Cancel = true;
                        Logger.Info(SOURCE, string.Format(
                            "aborting request for {0} in response to CancellationPending flag",
                            method));
                        return;
                    }
                    if (cycles >= 120)
                    {
                        //enforce timeout of 30 seconds (120 * 250ms)
                        request.Abort();
                        Logger.Info(SOURCE, string.Format(
                            "request for {0} timed out after {1} sec",
                            method, (cycles * WAIT / 1000)));
                        error = "request timed out";
                        return;
                    }
                    //wait for another 250 ms
                    ar.AsyncWaitHandle.WaitOne(WAIT, true);
                }
                _callbackDone.WaitOne();
                _callbackDone.Close();
                _callbackDone = null;
                using (var response = state.Response)
                {
                    if (response != null)
                    {
                        //read the response stream
                        var stream = response.GetResponseStream();
                        if (stream != null)
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                var responseText = reader.ReadToEnd();
                                Logger.Verbose(SOURCE, string.Format("response for {0} = {1}",
                                                                     method, responseText));
                                switch (method)
                                {
                                    case "UpdateContent":
                                        error = ParseUpdateResponse(responseText);
                                        break;                                    
                                    case "ReceiveSegment":
                                        error = ParseSegmentResponse(responseText);
                                        break;
                                    default:
                                        ParsePostResponse(responseText, ref pointer, out error);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //treat this as an error
                        Logger.Warning(SOURCE, string.Format(
                            "no response for {0}", method));
                        error = "No response";
                    }
                }
            }
            catch (WebException wex)
            {
                //exception will be raised if the server didn't return 200 - OK   
                //try to retrieve more information about the network error  
                if (wex.Response != null)
                {
                    try
                    {
                        var errResponse = (HttpWebResponse)wex.Response;
                        error = string.Format("code:{0}; desc:{1}",
                            errResponse.StatusCode,
                            errResponse.StatusDescription);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(SOURCE, ex.ToString());
                    }
                }
                else
                {
                    error = wex.ToString();
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                Logger.Error(SOURCE, ex.ToString());
            }
        }
      
        #region Callbacks
        
        private static void RequestStreamCallback(IAsyncResult ar)
        {
            const string SOURCE = CLASS_NAME + "RequestStreamCallback";
            try
            {
                //Logger.Verbose(Source, "returned");
                var state = (RequestState)ar.AsyncState;
                using (var postStream = state.Request.EndGetRequestStream(ar))
                {
                    postStream.Write(state.Data, 0, state.Data.Length);
                }
            }
            catch (WebException wex)
            {
                //WebException will be thrown if request is aborted
                if (wex.Message.Contains("request was aborted"))
                {
                    //ignore
                }
                else
                {
                    Logger.Error(SOURCE, wex.ToString());
                }
            }
            catch (Exception ex)
            {

                Logger.Info(SOURCE, ex.ToString());
            }
            finally
            {
                //signal completion
                if (_callbackDone != null) _callbackDone.Set();
            }
        }

        /// <summary>
        /// Callback for GetResponse
        /// </summary>
        /// <param name="ar"></param>
        private static void ResponseCallback(IAsyncResult ar)
        {
            const string SOURCE = CLASS_NAME + "ResponseCallback";
            try
            {
                var state = (RequestState)ar.AsyncState;
                //Logger.Verbose(Source, "returned for " + state.method);
                if (state.Request.HaveResponse)
                {
                    state.Response = (HttpWebResponse)state.Request.EndGetResponse(ar);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("request was aborted"))
                {
                    //ignore - that means the timeout expired and we aborted the request
                }
                else
                {
                    Logger.Info(SOURCE, ex.ToString());
                }
            }
            finally
            {
                //signal completion
                if (_callbackDone != null) _callbackDone.Set();
            }
        }

        #endregion

        private static byte[] Utf8Decode(byte[] input)
        {
            const string SOURCE = CLASS_NAME + "utf8Decode";
            byte[] result = null;
            int i = 0;
            try
            {
                var list = new List<byte>();
                while (i < input.Length)
                {
                    byte c = input[i];
                    if (c < 128)
                    {
                        list.Add(c);
                        i++;
                    }
                    else
                    {
                        byte c2;
                        if ((c > 191) && (c < 224))
                        {
                            c2 = input[i + 1];
                            list.Add((byte)(((c & 31) << 6) | (c2 & 63)));
                            i += 2;
                        }
                        else
                        {
                            c2 = input[i + 1];
                            byte c3 = input[i + 2];
                            list.Add((byte)(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63)));
                            i += 3;
                        }
                    }
                }
                result = list.ToArray();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return result;
        }

        private static byte[] Utf8Encode(IEnumerable<byte> input)
        {
            const string SOURCE = CLASS_NAME + "utf8Encode";
            byte[] result = null;
            try
            {
                var list = new List<byte>();
                foreach (byte c in input)
                {
                    if (c < 128)
                    {
                        list.Add(c);
                    }
                    else if ((c > 127) && (c < 255))//2048))
                    {
                        list.Add((byte)((c >> 6) | 192));
                        list.Add((byte)((c & 63) | 128));
                    }
                    else
                    {
                        list.Add((byte)((c >> 12) | 224));
                        list.Add((byte)(((c >> 6) & 63) | 128));
                        list.Add((byte)((c & 63) | 128));
                    }

                }
                result = list.ToArray();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return result;
        }
        
        private static string AssembleLoginParams(string email, string password)
        {
            var passwordBase64 =
                HttpUtility.UrlEncode(
                Convert.ToBase64String(Encoding.UTF8.GetBytes(password)));
            ////assemble the parameters
            return string.Format("email_addr={0}&passwd={1}&cmd=",
                HttpUtility.UrlEncode(email), passwordBase64);
        }

        private static void Compress(ref byte[] input)
        {
            const string SOURCE = CLASS_NAME + "Compress";
            //compress input array
            try
            {
                var mem = new MemoryStream();
                using(var deflate = new DeflateStream(mem,CompressionMode.Compress))
                {
                    deflate.Write(input, 0, input.Length);
                }
                //write compressed bytes back to original buffer
                input = mem.ToArray();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private static void Decompress(ref byte[] input)
        {
            const string SOURCE = CLASS_NAME + "Decompress";
            try
            {
                var mem = new MemoryStream(input);
                using (var inflate = new DeflateStream(mem, CompressionMode.Decompress))
                {
                    const int BUFFERSIZE = 100;
                    var buffer = new byte[BUFFERSIZE];
                    int offset = 0, read, size = 0;
                    do
                    {
                        // If the buffer doesn’t offer enough space left create a new array
                        // with the double size. Copy the current buffer content to that array
                        // and use that as new buffer.
                        if (buffer.Length < size + BUFFERSIZE)
                        {
                            var tmp = new byte[buffer.Length * 2];
                            Array.Copy(buffer, tmp, buffer.Length);
                            buffer = tmp;
                        }

                        // Read the net chunk of data.
                        read = inflate.Read(buffer, offset, BUFFERSIZE);

                        // Increment offset and read size.
                        offset += BUFFERSIZE;
                        size += read;
                    } while (read == BUFFERSIZE); // Terminate if we read less then the buffer size.

                    // Copy only that amount of data to the result that has actually been read!
                    input = new byte[size];
                    Array.Copy(buffer, input, size);
                }                
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private static void ParsePostResponse(string responseText, ref string pointer,
            out string error)
        {
            const string SOURCE = CLASS_NAME + "ParsePostResponse";
            try
            {
                if(string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    error = "Server did not respond";
                    return;
                }
                string code;
                EvalResponse(responseText, out code, out error);
                if (code.Equals("2")) //Content saved, key = 
                {
                    pointer = error.Substring(error.IndexOf('=') + 1).Trim();
                    error = "success";
                }
                else
                {
                    /*
                        -17 Unable to determine user data limit”
                        -14 User data limit exceeded: "
                        -13 Error fetching content file length, e: "
                        -12 Missing recipient list"
                        -10 Invalid number of parameters"
                         -5 Error writing to content or index file, e: "
                         -4 Error opening content file, e: "
                         -3 Error opening content index file, e: "	
                         -2 Login not complete"
                         -1 Invalid username/password
                     */
                    Double result;
                    if (!Double.TryParse(code, out result))
                    {
                        error = "Unknown error";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        private static string ParseUpdateResponse(string responseText)
        {
            const string SOURCE = CLASS_NAME + "ParseUpdateResponse";
            try
            {
                if (string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    return "Server did not respond";
                }
                string code;
                string error;
                EvalResponse(responseText, out code, out error);
                if (code.Equals("4")) // Content updated "
                {
                    return "success";
                }
                /*
                    -17 Unable to determine user data limit”
                    -15 Not a number, key: "
                    -14 User data limit exceeded: "
                    -13 Error fetching content file length, e: "
                    -10 Invalid number of parameters"
                        -7 Error updating content or index file, e: "
                        -4 Error opening content file, e: "
                        -3 Error opening content index file, e: "	
                        -2 Login not complete"
                        -1 Invalid username/password
                    */
                Double result;
                return Double.TryParse(code, out result) 
                    ? error
                    : "Unknown error";
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                return "Internal error";
            }
        }

        private static string ParseSegmentResponse(string responseText)
        {
            const string SOURCE = CLASS_NAME + "ParseSegmentResponse";
            try
            {
                if (string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    return "Server did not respond";
                }
                string code;
                string error;
                EvalResponse(responseText, out code, out error);
                if (code.Equals("12")) // Segment saved"
                {
                    return "success";
                }
                /*
                    -17 Unable to determine user data limit”
                    -15 Not a number, key: "
                    -14 User data limit exceeded: "
                    -13 Error fetching content file length, e: "
                    -10 Invalid number of parameters"
                        -7 Error updating content or index file, e: "
                        -4 Error opening content file, e: "
                        -3 Error opening content index file, e: "	
                        -2 Login not complete"
                        -1 Invalid username/password
                    */
                Double result;
                return Double.TryParse(code, out result)
                    ? error
                    : "Unknown error";
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                return "Internal error";
            }
        }

        private static void ParseFetchSegmentResponse(string responseText, bool returnRaw,
            ref string content, out string error)
        {
            const string SOURCE = CLASS_NAME + "ParseFetchSegmentResponse";
            try
            {
                if (string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    error = "Server did not respond";
                    return;
                }
                string code;
                EvalFetchSegmentResponse(responseText, out code, out error);
                //handle error codes
                if (code.Equals("13")) //Content segment fetched
                {
                }
                else
                {
                    /*
                        "-29 Content file missing, possibly deleted "  
                        “-26 Misleading e-mail address” 
                        "-15 Not a number, key: " 
                        "-11 Not a recipient, e-mail address: " 
                        "-6 Error reading content file" 
                        "-4 Error opening content file" 
                        "-3 Error deleting file "  
                        "-2 Login not complete" 
                        "13 Content segment fetched, file pointer=<pointer>, total content size=<length>, segment = " 
                     */
                    double result;
                    if (!Double.TryParse(code, out result))
                    {
                        //missing code
                        error = "Unknown error";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        private static void ParseFetchResponse(string responseText, bool returnRaw,
            ref string content, out string error)
        {
            const string SOURCE = CLASS_NAME + "ParseFetchResponse";
            try
            {
                if (string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    error = "Server did not respond";
                    return;
                }
                string code;
                EvalResponse(responseText, out code, out error);
                //handle error codes
                if (code.Equals("3")) //Content fetched, content = 
                {
                    string raw = error.Substring(
                        error.IndexOf("=", StringComparison.InvariantCultureIgnoreCase) + 1).Trim();
                    //guard against truncation
                    if (raw.Length % 2 > 0) raw += "=";
                    content = returnRaw 
                        ? raw 
                        : Encoding.UTF8.GetString(Convert.FromBase64String(raw));
                    error = "success";
                }
                else
                {
                    /*
                        -15 Not a number, key: "
                        -11 Not a recipient, e-mail address: "
                         -6 Error reading content file, e: "
                         -4 Error opening content file, e: "
                         -3 Error opening content index file, e: "
                         -2 Login not complete"
                         -1 Invalid username/password
                     */
                    double result;
                    if(!Double.TryParse(code, out result))
                    {
                        //missing code
                        error = "Unknown error";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                error = "Internal error";
            }
        }

        private static string ParseDeleteResponse(string responseText)
        {
            const string SOURCE = CLASS_NAME + "ParseDeleteResponse";
            try
            {
                if (string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    return "Server did not respond";
                }
                string code;
                string error;
                EvalResponse(responseText, out code, out error);
                //handle error codes
                if(code.Equals("5")) // Content deleted
                {
                    return "success";
                }
                /*  "-13" Error fetching content file length, e: "
                    "-10" Invalid number of parameters "
                        "-5" Error writing to content or index file, e: "
                        "-4" Error opening content file, e: "
                        "-3" Error opening content index file, e: "
                        "-2" Login not complete"
                        "-1" Incorrect e-mail address/password"
                    */                        
                double result;
                return Double.TryParse(code,out result)
                    ? error
                    :"Unknown error";
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                return "Internal error";
            }
        }

        private static string ParseAddRecipsResponse(string responseText)
        {
            const string SOURCE = CLASS_NAME + "ParseAddRecipsResponse";
            try
            {
                if (string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    return "Server did not respond";
                }
                string code;
                string error;
                EvalResponse(responseText, out code, out error);
                //handle error codes
                if (code.Equals("8")) // Recip added or exists
                {
                    return "";
                }
                /*     "-10" Invalid number of parameters "
                        "-4" Error opening content file, e: "
                        "-2" Login not complete"
                        "-1" Incorrect e-mail address/password"
                    */
                double result;
                return double.TryParse(code, out result)
                    ? error
                    : "Unknown error";
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                return "Internal error";
            }
        }

        private static string ParseRemoveRecipResponse(string responseText)
        {
            const string SOURCE = CLASS_NAME + "ParseRemoveRecipResponse";
            try
            {
                if (string.IsNullOrEmpty(responseText))
                {
                    Logger.Warning(SOURCE, "missing responseText");
                    return "Server did not respond";
                }
                string code;
                string error;
                EvalResponse(responseText, out code, out error);
                //handle error codes
                if (code.Equals("11")) // Recip removed
                {
                    return "";
                }
                /*     "-10" Invalid number of parameters "
                        "-4" Error opening content file, e: "
                        "-2" Login not complete"
                        "-1" Incorrect e-mail address/password"
                    */
                double result;
                return double.TryParse(code, out result)
                    ? error
                    : "Unknown error";
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                return "Internal error";
            }
        }

        /// <summary>
        /// to get response data as Storage data in bytes from server for passed account
        /// </summary>
        /// <param name="smtpAddress">email address</param>
        /// <param name="Password">password</param>
        /// <param name="server">server name</param>
        /// <param name="port">port number</param>
        /// <returns></returns>
        public static string GetDataResponse(string smtpAddress, string Password, string server, string port)
        {
            const string SOURCE = CLASS_NAME + "GetData";
            try
            {
                string post = AssembleLoginParams(smtpAddress, Password) +
                    string.Format("{0}", "GET%20DATA");
                byte[] postData = Encoding.UTF8.GetBytes(post);
                var request = CreateRequest(server, port, postData);
                if (request == null)
                {
                }
                var response = request.GetResponse() as HttpWebResponse;
                if (!request.HaveResponse)
                {
                    request.Abort();
                }
                if (response == null)
                {
                }
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                }
                var reader = new StreamReader(stream);
                string responseText = reader.ReadToEnd();
                
                return responseText;
            }
            catch (Exception e)
            {
                Logger.Error(SOURCE, e.ToString());
            }
            return string.Empty;
        }

        private static void EvalResponse(string responseText, out string code, out string error)
        {
            var delim = responseText.IndexOf(' ');
            code = responseText.Substring(0, delim);
            if (!string.IsNullOrEmpty(code)) code = code.Trim();
            error = responseText.Substring(delim);
            if (!string.IsNullOrEmpty(error)) error = error.Trim();
        }

        private static void EvalFetchSegmentResponse(string responseText, out string code, out string error)
        {
            var delim = responseText.IndexOf(' ');
            code = responseText.Substring(0, delim);
            if (!string.IsNullOrEmpty(code)) code = code.Trim();
            delim = responseText.IndexOf(AppConstants.TotalContentSize) + AppConstants.TotalContentSize.Length;
            error = responseText.Substring(delim);
            delim = error.IndexOf(',');
            error = error.Substring(0, delim);
            if (!string.IsNullOrEmpty(error)) error = error.Trim();
        }

        private static List<string> SegmentContent(string encoded)
        {
            var segments = new List<string>();
            var chunkSize = Convert.ToInt32(Math.Pow(1024, 2));
            var start = 0;
            while(start < encoded.Length)
            {
                var length = chunkSize;
                if (start + length > encoded.Length)
                {
                    length = encoded.Length - start;
                }
                var chunk = encoded.Substring(start, length);
                segments.Add(chunk);
                start += chunkSize;
            }           
            return segments;
        } 

        #endregion
    }
    
    public class RequestState
    {
        public HttpWebRequest Request;
        public HttpWebResponse Response;
        public Byte[] Data;
        public string Method;
    }
}
