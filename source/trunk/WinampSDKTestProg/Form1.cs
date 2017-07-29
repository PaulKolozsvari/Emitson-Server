using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinAmpSDK;

namespace TestProg
{
    public partial class Form1 : Form
    {
        private Winamp wa;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Winamp wa = new Winamp();
            wa = new Winamp(true);

            //TODO : Put valid mp3 filename here
            string filename = @"E:\New Music 7\B.U.G. Mafia feat. Adriana - Viata Noastra www.flor.cc.mp3";

            //wa.AppendToPlayList(filename);
            //Process.Start(wa.WA_Path, "/ADD \"" + filename + '"');
            //  THIS IS A HACK THAT WILL WORK ONLY IF "Alllow multiple instances" is NOT SELECTED IN WinAmp
 
            wa.AppendToPlayList(filename);

            //int length = Win32.SendMessage(wa.Handle, (int)WA_IPC.WM_WA_IPC, 0, (int)WA_IPC.IPC_GETLISTLENGTH);
            //wa.SendToWinamp(WA_MsgTypes.WM_USER, length-1, (int)WA_IPC.IPC_SETPLAYLISTPOS);
            wa.SendToWinamp(WA_MsgTypes.WM_COMMAND, (int)WinAmpSDK.WM_COMMAND_MSGS.WINAMP_BUTTON2, 0);
            

            //wa.ToggleShuffle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.ShowDialog(this);
            wa.AppendToPlayList(ofd1.FileName);
        }
    }
}