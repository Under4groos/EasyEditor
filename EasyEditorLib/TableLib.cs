using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEditorLib
{
    public static class TableLib
    {
        public static string[] Add(this string[] table,  string str)
        {
            List<string> list = new List<string>();
            list.AddRange(table);
            list.Add(str);
            return list.ToArray();
        }
    }
}
