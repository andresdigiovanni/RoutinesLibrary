using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutinesLibrary.Data
{
    public class EnumHelper
    {
        public static Enum GetEnumValue(Type enumType, string name, bool ignoreCase = false)
        {
            int index;
            if (ignoreCase)
            {
                index = Array.FindIndex(
                    Enum.GetNames(enumType),
                    s => string.Compare(s, name, StringComparison.OrdinalIgnoreCase) == 0);
            }
            else
            {
                index = Array.IndexOf(Enum.GetNames(enumType), name);
            }

            if (index < 0)
            {
                throw new ArgumentException("\"" + name + "\" is not a value in " + enumType, "name");
            }

            return (Enum)Enum.GetValues(enumType).GetValue(index);
        }
    }
}
