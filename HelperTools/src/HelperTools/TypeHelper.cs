using System;

namespace HelperTools
{
    public static class TypeHelper
    {
        private const char GenericTypeOpenChar = '[';
        private const char GenericTypeCloseChar = ']';
        private const char NameSeperator = ',';
        private const char GenericTypeArgsCountSeperator = '`';

        public static TypeName Parse(string type)
        {
            type = type.Replace(" ", "");

            if (type[0] == GenericTypeOpenChar && type[type.Length - 1] == GenericTypeCloseChar)
            {
                type = type.Trim(GenericTypeOpenChar, GenericTypeCloseChar);
            }

            string typeName;
            AssemblyName assemblyName;
            TypeName[] genericTypeArgs;
            
            int genericTypeOpenCharIndex = type.IndexOf(GenericTypeOpenChar);
            if (genericTypeOpenCharIndex > -1)
            {
                int genericTypeArgsCountSeperatorCharIndex = type.IndexOf(GenericTypeArgsCountSeperator, 0, genericTypeOpenCharIndex);

                typeName = type.Substring(0, genericTypeArgsCountSeperatorCharIndex);

                int genericTypeArgsCount = int.Parse(type.Substring(genericTypeArgsCountSeperatorCharIndex + 1, genericTypeOpenCharIndex - genericTypeArgsCountSeperatorCharIndex - 1));

                int genericTypeCloseCharIndex = FindGenericTypeCloseIndex(genericTypeOpenCharIndex, type);

                string genericTypeArgsStr = type.Substring(genericTypeOpenCharIndex + 1, genericTypeCloseCharIndex - (genericTypeOpenCharIndex + 1));

                genericTypeArgs = new TypeName[genericTypeArgsCount];

                int genericTypeArgIndex = 0, genericTypeArgOpenCharIndex = 0, genericTypeArgCloseCharIndex;
                while (genericTypeArgIndex < genericTypeArgsCount)
                {
                    genericTypeArgCloseCharIndex = FindGenericTypeCloseIndex(genericTypeArgOpenCharIndex, genericTypeArgsStr);
                    string genericTypeArgStr = genericTypeArgsStr.Substring(genericTypeArgOpenCharIndex, genericTypeArgCloseCharIndex - genericTypeArgOpenCharIndex + 1);
                    genericTypeArgs[genericTypeArgIndex] = Parse(genericTypeArgStr);

                    ++genericTypeArgIndex;
                    genericTypeArgOpenCharIndex = genericTypeArgCloseCharIndex + 2;
                }

                string assemblyNameStr = type.Substring(genericTypeCloseCharIndex + 1, type.Length - (genericTypeCloseCharIndex + 1)).Trim(NameSeperator);
                assemblyName = AssemblyHelper.Parse(assemblyNameStr);
            }
            else
            {
                int typeNameSeperatorCharIndex = type.IndexOf(NameSeperator);
                typeName = type.Substring(0, typeNameSeperatorCharIndex);

                string assemblyNameStr = type.Substring(typeNameSeperatorCharIndex + 1, type.Length - (typeNameSeperatorCharIndex + 1)).Trim(NameSeperator);
                assemblyName = AssemblyHelper.Parse(assemblyNameStr);

                genericTypeArgs = new TypeName[0];
            }

            return new TypeName(typeName, assemblyName, genericTypeArgs);
        }

        private static int FindGenericTypeCloseIndex(int genericTypeOpenCharIndex, string type)
        {
            for (int index = genericTypeOpenCharIndex, level = 0; index < type.Length; index++)
            {
                char ch = type[index];
                if (ch == GenericTypeOpenChar)
                {
                    ++level;
                }
                else if (ch == GenericTypeCloseChar)
                {
                    --level;
                    if (level == 0)
                    {
                        return index;
                    }
                }
            }

            return 0;
        }
    }
}
