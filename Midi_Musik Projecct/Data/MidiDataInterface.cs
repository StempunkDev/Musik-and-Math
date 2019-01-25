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
            byte[] reversed;
            byte[] fbyte;
            try
            {
                fbyte = br.ReadBytes(4);
                s = bytetoASCII(fbyte);
                Console.WriteLine(s);
                fbyte = br.ReadBytes(4);
                reversed = fbyte.Reverse().ToArray();
                s = BitConverter.ToInt32(reversed, 0).ToString();
                Console.WriteLine(s);
                



                br.Close();
                br.Dispose();
                fs.Close();
                return fbyte;
            }
            finally
            {
                br.Close();
                fs.Close();
            }
        }

        private static string bytetoASCII(byte[] b)
        {
            return ASCIIEncoding.ASCII.GetString(b);

        }
    }
}
