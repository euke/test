using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keeBoard_CS_01
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //QUELLE: http://www.whitebyte.info/programming/how-to-get-main-window-handle-of-the-last-active-window
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        
        IntPtr lastWindowHandle;
        IntPtr myWindowHandle = GetWindow(Process.GetCurrentProcess().MainWindowHandle, (uint)GetWindow_Cmd.GW_HWNDNEXT); //GW_HWNDNEXT

        private void Form1_Click(object sender, EventArgs e)
        {
            SetForegroundWindow(LastWinHandle());
        }

        IntPtr LastWinHandle()
        {
            IntPtr lastWindowHandle = GetWindow(Process.GetCurrentProcess().MainWindowHandle, (uint)GetWindow_Cmd.GW_HWNDNEXT);
            while (true)
            {
                IntPtr temp = GetParent(lastWindowHandle);
                if (temp.Equals(IntPtr.Zero)) break;
                lastWindowHandle = temp;
            }
            
            //SetForegroundWindow(lastWindowHandle);
            return lastWindowHandle;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //SetForegroundWindow(LastWinHandle());
            SetFocus(LastWinHandle());
            //System.Threading.Thread.Sleep(10);
            SendKeys.Send("5");
            //System.Threading.Thread.Sleep(100);
            //SetForegroundWindow(myWindowHandle);
            //SendKeys.SendWait("3");
            //SetForegroundWindow(LastWinHandle());
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetForegroundWindow(LastWinHandle());
        
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            SetForegroundWindow(myWindowHandle);

        }



    }
}
