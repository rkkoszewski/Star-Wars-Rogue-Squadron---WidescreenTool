using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace RogueSquadronUI
{
    public partial class mainform : Form
    {
        GameSettings _GraphicsSettings;
        static string _GamesExecutable = "Rogue Squadron.EXE";
        static string _dllHack = "widescreenfix.dll";

        public mainform()
        {
            InitializeComponent();
            if(!File.Exists(_GamesExecutable))
            {
                MessageBox.Show("No game executable found. Pleace place the file in the folder with the game.");
                Close();
            }
            else if(!File.Exists(_dllHack))
            {
                MessageBox.Show("DLL hack was not found. Pleace place it in the folder with the game and try again.");
                Close();
            }
            else
            {
                _GraphicsSettings = new GameSettings(this);
            }
        }

        private void mainform_Load(object sender, EventArgs e)
        {
            _GraphicsSettings = new GameSettings(this);
        }

        private void B_StartGame_Click(object sender, EventArgs e)
        {
            Process gameProcess = new Process();
            gameProcess.StartInfo.FileName = _GamesExecutable;
            gameProcess.EnableRaisingEvents = true;
            this.WindowState = FormWindowState.Minimized;

            gameProcess.Start();
            doInjection();
            gameProcess.WaitForExit();
            this.Close();


            try
            {                

            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred!: " + ex.Message);
                return;
            }
        }

        #region InjectionStuff
        private void doInjection()
        {
            Process[] processes = Process.GetProcesses();
            Process[] myProcess = null;
            int baseAddress = 0;
            while (baseAddress == 0)
            {
                for(int i=0; i<processes.Length; i++)
                {
                    if (processes[i].ProcessName.ToLower() == "rogue squadron")
                    {
                        myProcess = new Process[] { processes[i] };
                        baseAddress = myProcess[0].MainModule.BaseAddress.ToInt32();
                        Debug.WriteLine("Found process!");
                    }
                }

                Debug.WriteLine("Nothing. Waiting.");
                Thread.Sleep(1000);
            }

            Thread.Sleep(10000);
            DllInjectionResult result = DllInjector.GetInstance.Inject(myProcess, "widescreenfix.dll");
            if(result == DllInjectionResult.Success)
            {
                Debug.WriteLine("Injected!");
            }
            else if(result == DllInjectionResult.InjectionFailed)
            {
                Debug.WriteLine("Failed!");
            }
        }
        #endregion

        private void B_DisplaySettings_Click(object sender, EventArgs e)
        {
            _GraphicsSettings.SetDesktopLocation(this.DesktopLocation.X + 10, this.DesktopLocation.Y + 10);
            _GraphicsSettings.ShowDialog();
        }

        private void B_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
