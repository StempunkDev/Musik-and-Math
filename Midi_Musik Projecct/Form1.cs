using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Midi_Musik_Projecct.Data;

namespace Midi_Musik_Projecct
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
