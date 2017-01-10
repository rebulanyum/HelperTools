using System;
using System.Linq;

namespace HelperTools
{
    public static class AssemblyHelper
    {
        private const char NameSeperator = ',';
        private const char ValueSeperator = '=';
        private const string Version = "Version";
        private const string Culture = "Culture";
        private const string PublicKeyToken = "PublicKeyToken";
        public static AssemblyName Parse(string assembly)
        {
            string name = null, version = null, culture = null, publicKeyToken = null;

            string[] assemblyNameProps = assembly.Split(NameSeperator);
            foreach (string assemblyNameProp in assemblyNameProps)
            {
                if (assemblyNameProp.Contains(ValueSeperator))
                {
                    string[] eq = assemblyNameProp.Split(ValueSeperator);
                    switch (eq[0])
                    {
                        case Version:
                            version = eq[1];
                            break;
                        case Culture:
                            culture = eq[1];
                            break;
                        case PublicKeyToken:
                            publicKeyToken = eq[1];
                            break;
                    }
                }
                else if (assemblyNameProp != string.Empty)
                {
                    name = assemblyNameProp;
                }
            }

            return new AssemblyName(name, version, culture, publicKeyToken);
        }
    }
}
