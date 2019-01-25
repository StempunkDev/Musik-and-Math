using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Midi_Musik_Projecct.Data
{
    public static class MidiDataInterface
    {
        public static byte[] ReadByteMidi(string path)
        {
            FileStream fs = File.OpenRead(path);
            BinaryReader br = new BinaryReader(fs);
            String s;
            try
            {
                byte[] bytes = new byte[fs.Length];
                byte[] fbyte = br.ReadBytes(4);
                s = System.Text.ASCIIEncoding.ASCII.GetString(fbyte);
                Console.WriteLine(s);




                br.Close();
                fs.Close();
                return fbyte;
            }
            finally
            {
                br.Close();
                fs.Close();
            }
        }
    }
}
