using Newtonsoft.Json;
using System.IO;

namespace Midi_Musik_Projekt
{
    class Config
    {
        private const string configfile = "config.json";
        public static Templ Data;
        static Config()
        {
            if (!File.Exists(configfile))
            {
                Data = new Templ() {NoteTablePath = "NoteTable.json", AnalysePath = "Analyse" };
                string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
                File.WriteAllText(configfile, json);
            }
            else
            {
                string json = File.ReadAllText(configfile);
                Data = JsonConvert.DeserializeObject<Templ>(json);
            }

        }

        public static void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(Data);
            File.WriteAllText(configfile, json);
        }







        public struct Templ
        {
            public string NoteTablePath;
            public string AnalysePath;
        }

    }
}
