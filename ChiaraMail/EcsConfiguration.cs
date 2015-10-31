
using System;
using Newtonsoft.Json;

namespace ChiaraMail
{
    [Serializable]
    [JsonObjectAttribute(MemberSerialization.Fields)]
    internal class EcsConfiguration
    {
        internal int Key;
        internal string Description;
        internal string Server;
        internal string Password;
        internal string Port;
        internal bool DefaultOn;
        internal bool Encrypt;
        internal bool NoPlaceholder;
        internal bool AllowForwarding;
    }
}
