using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyEditorLib
{
    public static class FuncType
    {
        public static object GetType(string str , Type t)
        {
            foreach (var item in Enum.GetValues(t))
            {
                if (item.ToString().ToLower() == str.ToLower())
                    return item;
            }
            return null;
        }
    }
     
}
