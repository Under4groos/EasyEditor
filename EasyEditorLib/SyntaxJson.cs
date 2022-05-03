using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EasyEditorLib
{
    [Serializable]
    public struct Syntax
    {
        public string Foreground { get; set; }
        public string Background { get; set; } 
        public string DriverOperation { get; set; }
        public bool IsRegex { get; set; }
        public string Keywords { get; set; }
    }

    public class SyntaxJson
    {
        public Dictionary<string, Syntax> syntax = new Dictionary<string, Syntax>();
        public string languages;
        public Action<bool> EventOpen;
        public SyntaxJson()
        {

        }
        public void CreateAll()
        {
             
        }
        public void Save()
        {
            if (languages == null)
                return;
            string json = JsonConvert.SerializeObject(syntax , Formatting.Indented);

            File.WriteAllText($"Syntax/{languages.ToString()}.txt", json);
        }
        public void Open()
        {
            if (languages == null)
            {
                if(EventOpen != null)
                    this.EventOpen(false);
                return;
            }
                
            string name_ = $"Syntax/{languages.ToString()}.txt";
            if (!File.Exists(name_))
            {
                if (EventOpen != null)
                    this.EventOpen(false);
                return;
            }
                
            try
            {
                string str_ = File.ReadAllText(name_);
                syntax = JsonConvert.DeserializeObject<Dictionary<string, Syntax>>(str_);

                if (EventOpen != null)
                    this.EventOpen(true);
            }
            catch (Exception err)
            {
                if (EventOpen != null)
                    this.EventOpen(false);
                 
            } 
        }
        public void Clear()
        {
            syntax.Clear();
        }
        public void add(string color , Syntax Keywords)
        {
            syntax.Add(color , Keywords);
        }
    }
}
