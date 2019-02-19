using Melanchall.DryWetMidi.Smf.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Midi_Musik_Projekt.Data;

namespace Midi_Musik_Projekt
{
    class NoteConverter
    {
        private static Dictionary<string, float> Conv = new Dictionary<string, float>();
        public static string filepath = Config.Data.NoteTablePath;

        static NoteConverter()
        {
            if (!ValidateStorage(filepath)) return;
            string json = File.ReadAllText(Config.Data.NoteTablePath);
            Conv = JsonConvert.DeserializeObject<Dictionary<string, float>>(json);
        }

        public static float CNote(List<Note> NL1, List<Note> NL2)
        {
            float Akk1 = 0;
            float Akk2 = 0;
            foreach (Note N in NL1) Akk1 += Conv[N.ToString()];
            foreach (Note N in NL2) Akk2 += Conv[N.ToString()];
            return Akk2 / Akk1;
        }


        private static bool ValidateStorage(string file)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, "");
                return false;
            }
            return true;
        }
        
    }
}
