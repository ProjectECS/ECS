
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChiaraMail
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class Account
    {
        internal string SMTPAddress = "";
        internal string UserName = "";
        internal Dictionary<int, EcsConfiguration> Configurations;
        internal int DefaultConfiguration;
        internal string Password = "";
        internal string Host = "";
        internal string Port = "";
        internal string Protocol = "";
        internal string LoginName = "";
    }
}
