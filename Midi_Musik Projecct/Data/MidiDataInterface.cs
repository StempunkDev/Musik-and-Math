using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Midi_Musik_Projekt.Data
{
    public static class MidiDataInterface
    {
        public static void ReadByteMidi(string path, string name)
        {
            // Variablen Definition
            FileStream fs = File.OpenRead(path);
            BinaryReader br = new BinaryReader(fs);
            String s;
            long time = 0;
            Note NB_1 = null;
            int NCount = 0;
            List<float> result = new List<float>();
            List<Note> Akkord1 = new List<Note>();
            List<Note> Akkord2 = new List<Note>();
            List<Note> AkkordBuffer = new List<Note>();
            string AnalyseFolder = Config.Data.AnalysePath;
            string AnalysePath = AnalyseFolder + @"\";
            // Try Block für die Vorbeugung von Fehlern.
            try
            {
                // Test nach dem Richtigen Dateitypen anhand der ersten 4 Bytes.
                s = bytetoASCII(br.ReadBytes(4));
                if(s == "MThd")
                {
                    var midifile = MidiFile.Read(path);
                    IEnumerable<Chord> chords = midifile.GetChords();
                    List<Note> notes = new List<Note>();
                    foreach(Chord c in chords)
                    {
                        foreach (Note N in c.Notes) notes.Add(N);
                    }
                    //Analysing
                    time = notes.First().Time;
                    NCount = notes.Count();
                    foreach (Note N in notes)
                    {
                        if (NB_1 != null && !Akkord2.Contains(NB_1)) Akkord2.Add(NB_1);
                        if (N.Time == time)
                        {
                            Akkord2.Add(N);
                        }
                        else if(time != N.Time)
                        {
                            // Buffering
                            addBuffer(Akkord2, AkkordBuffer);
                            AkkordBuffer = CalculateBuffer(AkkordBuffer,N);
                            foreach (Note no in AkkordBuffer) { if (!Akkord2.Contains(no)) Akkord2.Add(no); }

                            if (Akkord2.First().Time < N.Time && Akkord1.Count > 0)
                            {

                                result.Add(NoteConverter.CNote(Akkord1, Akkord2));
                            }
                            //Shifting
                            Akkord1.Clear();
                            foreach (Note no in Akkord2) Akkord1.Add(no);
                            Akkord2.Clear();
                            NB_1 = N;
                            time = N.Time;

                        }
                     }
                    if (NB_1 != null && !Akkord2.Contains(NB_1)) Akkord2.Add(NB_1);
                    result.Add(NoteConverter.CNote(Akkord1, Akkord2));
                    ValidateAnalysisPath(AnalyseFolder);
                    
                    string json = JsonConvert.SerializeObject(result,Formatting.Indented);
                    File.WriteAllText(AnalysePath + name + ".json", json);

                }
                else
                {
                    MessageBox.Show(text: "Die ausgewählte Datei ist keine Midi Datei", caption: "Fehler bei der Dateneinlesung",icon:MessageBoxIcon.Error,buttons: MessageBoxButtons.OK);
                }

                
            }
            finally
            {
                br.Close();
                fs.Close();
            }
        }

        private static void ValidateAnalysisPath(string FolderPath)
        {
            if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
        }

        private static List<Note> CalculateBuffer(List<Note> list, Note N)
        {
            long length;
            long T1;
            long T2 = N.Time;
            long M_Res;
            List<Note> result = new List<Note>();
            foreach(Note no in list)
            {
                T1 = no.Time;
                length = no.Length;
                M_Res = (T1 + length) - T2;
                if (M_Res >= 0) result.Add(no);
            }
            return result;
        }

        private static long FindSmallest(List<Note> list)
        {
            long L_small = 100000;
            foreach (Note no in list)
            {
                if (no.Length < L_small)
                    L_small = no.Length;
            }
            return L_small;
        }

        private static void addBuffer(List<Note> list, List<Note> buffer)
        {
            long L_small = FindSmallest(list);
            foreach(Note no in list)
            {
                if (no.Length != L_small) buffer.Add(no);
            }
        }

        private static string bytetoASCII(byte[] b)
        {
            return ASCIIEncoding.ASCII.GetString(b);

        }
    }

}
