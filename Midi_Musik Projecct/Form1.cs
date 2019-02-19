using System;
using System.IO;
using System.Windows.Forms;
using Midi_Musik_Projekt.Data;

namespace Midi_Musik_Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Btn_openData.Enabled = false;
                string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                MidiDataInterface.ReadByteMidi(openFileDialog1.FileName, filename);
                Btn_openData.Enabled = true;
            }
        }
    }
}
