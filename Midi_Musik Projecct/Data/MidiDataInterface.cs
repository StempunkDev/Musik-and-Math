using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Midi_Musik_Projecct.Data
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
            List<Note> Accord1 = new List<Note>();
            List<Note> Accord2 = new List<Note>();
            List<Note> AccordBuffer = new List<Note>();
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
                        if (NB_1 != null && !Accord2.Contains(NB_1)) Accord2.Add(NB_1);
                        if (N.Time == time)
                        {
                            Accord2.Add(N);
                        }
                        else if(time != N.Time)
                        {
                            // Buffering
                            addBuffer(Accord2, AccordBuffer);
                            AccordBuffer = CalculateBuffer(AccordBuffer,N);
                            foreach (Note no in AccordBuffer) { if (!Accord2.Contains(no)) Accord2.Add(no); }

                            if (Accord2.First().Time < N.Time && Accord1.Count > 0)
                            {

                                result.Add(NoteConverter.CNote(Accord1, Accord2));
                            }
                            //Shifting
                            Accord1.Clear();
                            foreach (Note no in Accord2) Accord1.Add(no);
                            Accord2.Clear();
                            NB_1 = N;
                            time = N.Time;

                        }
                     }
                    if (NB_1 != null && !Accord2.Contains(NB_1)) Accord2.Add(NB_1);
                    result.Add(NoteConverter.CNote(Accord1, Accord2));
                    ValidateAnalysisPath(AnalyseFolder);
                    
                    string json = JsonConvert.SerializeObject(result);
                    File.WriteAllText(AnalysePath + name + ".json", json);

                }
                else
                {
                    MessageBox.Show(text: "Die ausgewählte Datei ist keine Midi Datei", caption: "Fehler bei der Dateneinlesung",icon:MessageBoxIcon.Error,buttons: MessageBoxButtons.OK);
                }
                br.Close();
                br.Dispose();
                fs.Close();
                
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
