using System;

namespace HelperTools
{
    public struct AssemblyName
    {
        public AssemblyName(string name, string version, string culture, string publicKeyToken)
        {
            Name = name;
            Version = version;
            Culture = culture;
            PublicKeyToken = publicKeyToken;
        }
        public string Name { get; }

        public string Version { get; }

        public string Culture { get; }

        public string PublicKeyToken { get; }
    }
}
