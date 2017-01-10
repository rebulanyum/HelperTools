using System;

namespace HelperTools
{
    public struct TypeName
    {
        public TypeName(string name, AssemblyName assembly, params TypeName[] genericTypeArguments)
        {
            Name = name;
            Assembly = assembly;
            GenericTypeArguments = genericTypeArguments ?? new TypeName[0];
        }
        public string Name { get; }

        public AssemblyName Assembly { get; }

        public TypeName[] GenericTypeArguments { get; }
    }
}
