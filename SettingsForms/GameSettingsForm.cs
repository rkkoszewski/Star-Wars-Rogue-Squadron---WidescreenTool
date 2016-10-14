using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RogueSquadronUI
{
    public partial class GameSettings : Form
    {
        static string _GamesExecutable = "Rogue Squadron.EXE";
        static string _GamesExecutableBackup = "Rogue Squadron.exe.bak";
        public int ResolutionX = 1280;
        public int ResolutionY = 720;
        public float aspectRatio = 1.77777f;

        byte[] resSequance = { 0x80, 0x02, 0x00, 0x00, 0xE0, 0x01, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x20, 0x03, 0x00, 0x00, 0x58, 0x02, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x04 };
        int sequenceoffset = 24;
        byte[] fovSequence = { 0x07, 0x79, 0x00, 0xC7, 0x05, 0xE8, 0x07, 0x79, 0x00, 0x00, 0x00, 0x00, 0x40, 0x88, 0x0D, 0x30, 0x08, 0x79, 0x00, 0x88, 0x1D, 0x18, 0x08, 0x79, 0x00, 0x88, 0x1D, 0x31 };
        int fovSequenceoffset = 9;

        int adress = 0;
        int adress2 = 0;
        int aspectAddress = 0;
        byte[] data;

        public GameSettings(mainform parent)
        {
            InitializeComponent();
            data = GetBytesFromAFile(_GamesExecutable);
            adress = findSequence(data, resSequance, new int[] { sequenceoffset, sequenceoffset + 4 });
            if(adress != -1)
                adress2 = adress + 4;
            aspectAddress = findSequence(data, fovSequence, fovSequenceoffset);

            if (adress == -1 || adress2 == -1 || aspectAddress == -1)
            {
                MessageBox.Show("Nothing found in the file. Sorry.");
                Close();
            }
            else
            {
                ResolutionX = BitConverter.ToInt32(data, adress);
                ResolutionY = BitConverter.ToInt32(data, adress2);
                T_ResolutionX.Text = ResolutionX.ToString();
                T_ResolutionY.Text = ResolutionY.ToString();
                aspectRatio = ResolutionX*1.0f / ResolutionY;

                Debug.WriteLine("ResX: " + ResolutionX.ToString() + "(" + adress.ToString("X4") + "), ResY: " + ResolutionY.ToString() + "(" + adress2.ToString("X4") + ").");
                Debug.WriteLine("Aspect ratio: " + (BitConverter.ToSingle(data, aspectAddress)).ToString() + "(" + aspectAddress.ToString("X4") + ").");
            }
        }

        private void T_ResolutionX_TextChanged(object sender, EventArgs e)
        {
            var res = 1280;
            if(int.TryParse(T_ResolutionX.Text, out res))
            {
                ResolutionX = res;
            }
        }

        private void T_ResolutionY_TextChanged(object sender, EventArgs e)
        {
            var res = 720;
            if (int.TryParse(T_ResolutionY.Text, out res))
            {
                ResolutionY = res;
            }
        }


        private void B_SaveAndClose_Click(object sender, EventArgs e)
        {
            aspectRatio = ResolutionX*1.0f / ResolutionY;
            overrideBytesInArray(data, BitConverter.GetBytes(ResolutionX), adress);
            overrideBytesInArray(data, BitConverter.GetBytes(ResolutionY), adress2);
            overrideBytesInArray(data, BitConverter.GetBytes(aspectRatio), aspectAddress);

            if (!File.Exists(@_GamesExecutableBackup))
            {
                File.Copy(@_GamesExecutable, @_GamesExecutableBackup);
            }

            bool success = true;
            success = WriteBytesToAFile(_GamesExecutable, data);

            if (!success)
                MessageBox.Show("There was an error writting to a file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MessageBox.Show("Successfully made the changes!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region FindAdress
        private int findSequence(byte[] source, byte[] _sequence, int offset)
        {
            for (int adress = 0; adress < source.Length; adress++)
            {
                if (compareByteArrays(_sequence, source, adress, offset))
                {
                    return adress + offset;
                }
            }
            return -1;
        }

        private bool compareByteArrays(byte[] sequenceArray, byte[] dataArray, int lookFrom, int dataOffset)
        {
            if (dataArray.Length - lookFrom > sequenceArray.Length)
            {
                for (int i = 0; i < sequenceArray.Length; i++)
                {
                    if (i == dataOffset)
                        i = i + 4;
                    else if (sequenceArray[i] != dataArray[lookFrom + i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private int findSequence(byte[] source, byte[] _sequence, int[] offsets)
        {
            //I originally wrote it to find one value, but look - now I need 2
            for (int adress = 0; adress < source.Length; adress++)
            {
                if (compareByteArrays(_sequence, source, adress, offsets))
                {
                    return adress + offsets[0];
                }
            }
            return -1;
        }

        private bool compareByteArrays(byte[] sequenceArray, byte[] dataArray, int lookFrom, int[] dataOffset)
        {
            if (dataArray.Length - lookFrom > sequenceArray.Length)
            {
                for (int i = 0; i < sequenceArray.Length; i++)
                {
                    for(int j=0; j<dataOffset.Length; j++)
                    {
                        if (i == dataOffset[j])
                            i = i + 4;
                    }

                    if (sequenceArray[i] != dataArray[lookFrom + i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private void overrideBytesInArray(byte[] source, byte[] value, int address)
        {
            if(address+value.Length < source.Length)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    source[address + i] = value[i];
                }
            }
            else
            {
                throw new Exception("Value lenght + address is out of byte array!");
            }
        }
        #endregion

        #region Load_Save
        private byte[] GetBytesFromAFile(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(@filename);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private bool WriteBytesToAFile(string filename, byte[] usedData)
        {
            try
            {
                File.WriteAllBytes(@filename, usedData);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }
        #endregion
    }
}
